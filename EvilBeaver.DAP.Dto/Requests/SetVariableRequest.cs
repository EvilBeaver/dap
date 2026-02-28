using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetVariableRequest : Request<SetVariableArguments>
{
    public SetVariableRequest() => Command = "setVariable";
}

public class SetVariableArguments
{
    [JsonPropertyName("variablesReference")]
    public int VariablesReference { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("format")]
    public ValueFormat? Format { get; set; }
}

public class SetVariableResponse : Response<SetVariableResponseBody>
{
}

public class SetVariableResponseBody
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("variablesReference")]
    public int? VariablesReference { get; set; }

    [JsonPropertyName("namedVariables")]
    public int? NamedVariables { get; set; }

    [JsonPropertyName("indexedVariables")]
    public int? IndexedVariables { get; set; }

    [JsonPropertyName("memoryReference")]
    public string? MemoryReference { get; set; }

    [JsonPropertyName("valueLocationReference")]
    public int? ValueLocationReference { get; set; }
}
