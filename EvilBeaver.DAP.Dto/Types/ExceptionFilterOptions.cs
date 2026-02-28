using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionFilterOptions
{
    [JsonPropertyName("filterId")]
    public string FilterId { get; set; } = default!;

    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}
