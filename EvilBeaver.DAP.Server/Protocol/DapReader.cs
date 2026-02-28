using System.Text;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Serialization;

namespace EvilBeaver.DAP.Server.Protocol;

public class DapReader
{
    private readonly Stream _input;
    private const string ContentLengthHeader = "Content-Length: ";
    private static readonly byte[] HeaderSeparator = "\r\n\r\n"u8.ToArray();

    public DapReader(Stream input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
    }

    public async Task<ProtocolMessage?> ReadMessageAsync(CancellationToken ct = default)
    {
        var contentLength = await ReadContentLengthAsync(ct);
        if (contentLength == -1)
        {
            return null;
        }

        var bodyBuffer = new byte[contentLength];
        var totalRead = 0;
        while (totalRead < contentLength)
        {
            var read = await _input.ReadAsync(bodyBuffer.AsMemory(totalRead, contentLength - totalRead), ct);
            if (read == 0)
            {
                throw new EndOfStreamException("Unexpected end of stream while reading message body.");
            }
            totalRead += read;
        }

        return DapSerializer.Deserialize(bodyBuffer);
    }

    private async Task<int> ReadContentLengthAsync(CancellationToken ct)
    {
        var headerBuilder = new StringBuilder();
        var buffer = new byte[1];
        var separatorMatchIndex = 0;

        while (true)
        {
            var read = await _input.ReadAsync(buffer.AsMemory(), ct);
            if (read == 0)
            {
                return -1;
            }

            var b = buffer[0];
            headerBuilder.Append((char)b);

            if (b == HeaderSeparator[separatorMatchIndex])
            {
                separatorMatchIndex++;
                if (separatorMatchIndex == HeaderSeparator.Length)
                {
                    break;
                }
            }
            else
            {
                // Reset separator match index but check if current byte starts the separator again
                separatorMatchIndex = b == HeaderSeparator[0] ? 1 : 0;
            }
        }

        var headers = headerBuilder.ToString();
        var lines = headers.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            if (line.StartsWith(ContentLengthHeader, StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(line.Substring(ContentLengthHeader.Length).Trim(), out var length))
                {
                    return length;
                }
            }
        }

        throw new InvalidDataException("Content-Length header not found or invalid.");
    }
}
