// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


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
