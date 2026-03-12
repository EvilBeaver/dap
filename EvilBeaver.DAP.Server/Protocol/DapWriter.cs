// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

#if NET8_0_OR_GREATER
using System.Buffers.Text;
#endif
using System.Text;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Serialization;

namespace EvilBeaver.DAP.Server.Protocol;

internal class DapWriter
{
    private readonly Stream _output;
    private readonly SemaphoreSlim _writeLock = new SemaphoreSlim(1, 1);
    private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);

    private static readonly byte[] ContentLengthPrefix = "Content-Length: "u8.ToArray();
    private static readonly byte[] HeaderSeparator = "\r\n\r\n"u8.ToArray();

    public DapWriter(Stream output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public async Task WriteMessageAsync(ProtocolMessage message, CancellationToken ct = default)
    {
        var json = DapSerializer.Serialize(message);
        var bodyBytes = Utf8NoBom.GetBytes(json);

        await _writeLock.WaitAsync(ct);
        try
        {
#if NET8_0_OR_GREATER
            await _output.WriteAsync(ContentLengthPrefix.AsMemory(), ct);
            
            Span<byte> lengthBuffer = stackalloc byte[16];
            if (Utf8Formatter.TryFormat(bodyBytes.Length, lengthBuffer, out int bytesWritten))
            {
                await _output.WriteAsync(lengthBuffer.Slice(0, bytesWritten).ToArray().AsMemory(), ct);
            }
            else
            {
                // Fallback if somehow 16 bytes is not enough
                var lengthBytes = Utf8NoBom.GetBytes(bodyBytes.Length.ToString());
                await _output.WriteAsync(lengthBytes.AsMemory(), ct);
            }

            await _output.WriteAsync(HeaderSeparator.AsMemory(), ct);
            await _output.WriteAsync(bodyBytes.AsMemory(), ct);
#else
            var lengthBytes = Utf8NoBom.GetBytes(bodyBytes.Length.ToString());
            
            await _output.WriteAsync(ContentLengthPrefix, 0, ContentLengthPrefix.Length, ct);
            await _output.WriteAsync(lengthBytes, 0, lengthBytes.Length, ct);
            await _output.WriteAsync(HeaderSeparator, 0, HeaderSeparator.Length, ct);
            await _output.WriteAsync(bodyBytes, 0, bodyBytes.Length, ct);
#endif
            await _output.FlushAsync(ct);
        }
        finally
        {
            _writeLock.Release();
        }
    }
}
