// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Server.Protocol;
using EvilBeaver.DAP.Server.Transport;
using System.Threading;

namespace EvilBeaver.DAP.Server;

public class DapServer : IClientChannel
{
    private readonly ITransport _transport;
    private readonly IDebugAdapter _adapter;
    private readonly DapReader _reader;
    private readonly DapWriter _writer;
    private int _seq;
    private CancellationTokenSource? _shutdownCts;

    public DapServer(ITransport transport, IDebugAdapter adapter)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        _reader = new DapReader(_transport.Input);
        _writer = new DapWriter(_transport.Output);
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        _shutdownCts = new CancellationTokenSource();
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, _shutdownCts.Token);
        try
        {
            await _adapter.OnServerStartAsync(this, ct);
            var loop = new MessageLoop(_reader, _writer, _adapter, NextSeq);
            await loop.RunAsync(linkedCts.Token);
        }
        finally
        {
            _shutdownCts.Dispose();
            _shutdownCts = null;
        }
    }

    public Task SendEventAsync(Event @event, CancellationToken ct = default)
    {
        @event.Seq = NextSeq();
        return _writer.WriteMessageAsync(@event, ct);
    }

    public void Shutdown()
    {
        _shutdownCts?.Cancel();
    }

    private int NextSeq() => Interlocked.Increment(ref _seq);
}
