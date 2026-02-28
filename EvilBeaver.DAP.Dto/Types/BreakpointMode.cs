using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class BreakpointMode
{
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = default!;

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("appliesTo")]
    public string[] AppliesTo { get; set; } = default!;
}

public static class BreakpointModeApplicability
{
    public const string Source = "source";
    public const string Exception = "exception";
    public const string Data = "data";
    public const string Instruction = "instruction";
}
