using System.Text.Json;
using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Serialization;

public static class DapSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DapMessageConverter() }
    };

    public static string Serialize(ProtocolMessage message)
    {
        return JsonSerializer.Serialize(message, Options);
    }

    public static ProtocolMessage? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<ProtocolMessage>(json, Options);
    }

    public static ProtocolMessage? Deserialize(ReadOnlySpan<byte> utf8)
    {
        return JsonSerializer.Deserialize<ProtocolMessage>(utf8, Options);
    }
}
