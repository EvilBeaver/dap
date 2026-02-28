using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class DisassembledInstruction
{
    [JsonPropertyName("address")]
    public string Address { get; set; } = default!;

    [JsonPropertyName("instructionBytes")]
    public string? InstructionBytes { get; set; }

    [JsonPropertyName("instruction")]
    public string Instruction { get; set; } = default!;

    [JsonPropertyName("symbol")]
    public string? Symbol { get; set; }

    [JsonPropertyName("location")]
    public Source? Location { get; set; }

    [JsonPropertyName("line")]
    public int? Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }

    [JsonPropertyName("endLine")]
    public int? EndLine { get; set; }

    [JsonPropertyName("endColumn")]
    public int? EndColumn { get; set; }

    [JsonPropertyName("presentationHint")]
    public string? PresentationHint { get; set; }
}

public static class DisassembledInstructionPresentationHint
{
    public const string Normal = "normal";
    public const string Invalid = "invalid";
}
