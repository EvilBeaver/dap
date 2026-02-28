using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class BreakpointLocation
{
    [JsonPropertyName("line")]
    public int Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }

    [JsonPropertyName("endLine")]
    public int? EndLine { get; set; }

    [JsonPropertyName("endColumn")]
    public int? EndColumn { get; set; }
}
