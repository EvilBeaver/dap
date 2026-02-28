using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionPathSegment
{
    [JsonPropertyName("negate")]
    public bool? Negate { get; set; }

    [JsonPropertyName("names")]
    public string[] Names { get; set; } = default!;
}
