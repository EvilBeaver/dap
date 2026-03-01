// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Serialization;

namespace EvilBeaver.DAP.Server.Protocol;

public class DapWriter
{
    private readonly Stream _output;
    private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

    public DapWriter(Stream output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public async Task WriteMessageAsync(ProtocolMessage message, CancellationToken ct = default)
    {
        var json = DapSerializer.Serialize(message);
        var bodyBytes = Utf8NoBom.GetBytes(json);
        
        var header = $"Content-Length: {bodyBytes.Length}\r\n\r\n";
        var headerBytes = Utf8NoBom.GetBytes(header);

#if NET8_0_OR_GREATER
        await _output.WriteAsync(headerBytes, ct);
        await _output.WriteAsync(bodyBytes, ct);
#else
        await _output.WriteAsync(headerBytes, 0, headerBytes.Length, ct);
        await _output.WriteAsync(bodyBytes, 0, bodyBytes.Length, ct);
#endif
        await _output.FlushAsync(ct);
    }
}
