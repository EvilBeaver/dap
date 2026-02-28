using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetExpressionRequest : Request<SetExpressionArguments>
{
    public SetExpressionRequest() => Command = "setExpression";
}

public class SetExpressionArguments
{
    [JsonPropertyName("expression")]
    public string Expression { get; set; } = default!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("frameId")]
    public int? FrameId { get; set; }

    [JsonPropertyName("format")]
    public ValueFormat? Format { get; set; }
}

public class SetExpressionResponse : Response<SetExpressionResponseBody>
{
}

public class SetExpressionResponseBody
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = default!;

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("presentationHint")]
    public VariablePresentationHint? PresentationHint { get; set; }

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
