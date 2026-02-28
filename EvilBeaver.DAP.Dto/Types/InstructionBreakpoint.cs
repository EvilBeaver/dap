using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class InstructionBreakpoint
{
    [JsonPropertyName("instructionReference")]
    public string InstructionReference { get; set; } = default!;

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("hitCondition")]
    public string? HitCondition { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}
