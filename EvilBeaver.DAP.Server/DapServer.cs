// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Server.Protocol;
using EvilBeaver.DAP.Server.Transport;
using Microsoft.Extensions.Logging;

namespace EvilBeaver.DAP.Server;

/// <summary>
/// Runs message loop that listens to specified input and output streams
/// And calls methods of debug adapter.
/// </summary>
public class DapServer : IClientChannel
{
    private readonly ITransport _transport;
    private readonly IDebugAdapter _adapter;
    private readonly ILoggerFactory? _loggerFactory;
    private readonly DapReader _reader;
    private readonly DapWriter _writer;

    /// <summary>
    /// Creates server for DAP messages processing
    /// </summary>
    /// <param name="transport">Transport for client communication</param>
    /// <param name="adapter">Debug adapter implementation</param>
    /// <param name="loggerFactory">Factory for create loggers for server infrastructure</param>
    public DapServer(ITransport transport, IDebugAdapter adapter, ILoggerFactory? loggerFactory = null)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        _loggerFactory = loggerFactory;
        
        _reader = new DapReader(_transport.Input);
        _writer = new DapWriter(_transport.Output);
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        await _adapter.OnServerStartAsync(this, ct);
        var loop = new MessageLoop(_reader, _writer, _adapter);
        await loop.RunAsync(ct);
    }

    public Task SendEventAsync(Event @event, CancellationToken ct = default)
    {
        return _writer.WriteMessageAsync(@event, ct);
    }

    public Task SendResponseAsync(Response response, CancellationToken ct = default)
    {
        return _writer.WriteMessageAsync(response, ct);
    }
}
