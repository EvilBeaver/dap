using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class Variable
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("presentationHint")]
    public VariablePresentationHint? PresentationHint { get; set; }

    [JsonPropertyName("evaluateName")]
    public string? EvaluateName { get; set; }

    [JsonPropertyName("variablesReference")]
    public int VariablesReference { get; set; }

    [JsonPropertyName("namedVariables")]
    public int? NamedVariables { get; set; }

    [JsonPropertyName("indexedVariables")]
    public int? IndexedVariables { get; set; }

    [JsonPropertyName("memoryReference")]
    public string? MemoryReference { get; set; }

    [JsonPropertyName("declarationLocationReference")]
    public int? DeclarationLocationReference { get; set; }

    [JsonPropertyName("valueLocationReference")]
    public int? ValueLocationReference { get; set; }
}
