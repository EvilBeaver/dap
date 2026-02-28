using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionBreakpointsFilter
{
    [JsonPropertyName("filter")]
    public string Filter { get; set; } = default!;

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("default")]
    public bool? Default { get; set; }

    [JsonPropertyName("supportsCondition")]
    public bool? SupportsCondition { get; set; }

    [JsonPropertyName("conditionDescription")]
    public string? ConditionDescription { get; set; }
}
