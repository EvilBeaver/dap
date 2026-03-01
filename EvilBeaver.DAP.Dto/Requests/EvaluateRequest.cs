// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class EvaluateRequest : Request<EvaluateArguments>
{
    public EvaluateRequest() => Command = "evaluate";
}

public class EvaluateArguments
{
    [JsonPropertyName("expression")]
    public string Expression { get; set; } = default!;

    [JsonPropertyName("frameId")]
    public int? FrameId { get; set; }

    [JsonPropertyName("line")]
    public int? Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }

    [JsonPropertyName("source")]
    public Source? Source { get; set; }

    [JsonPropertyName("context")]
    public string? Context { get; set; }

    [JsonPropertyName("format")]
    public ValueFormat? Format { get; set; }
}

public static class EvaluateContext
{
    public const string Watch = "watch";
    public const string Repl = "repl";
    public const string Hover = "hover";
    public const string Clipboard = "clipboard";
    public const string Variables = "variables";
}

public class EvaluateResponse : Response<EvaluateResponseBody>
{
}

public class EvaluateResponseBody
{
    [JsonPropertyName("result")]
    public string Result { get; set; } = default!;

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("presentationHint")]
    public VariablePresentationHint? PresentationHint { get; set; }

    [JsonPropertyName("variablesReference")]
    public int VariablesReference { get; set; }

    [JsonPropertyName("namedVariables")]
    public int? NamedVariables { get; set; }

    [JsonPropertyName("indexedVariables")]
    public int? IndexedVariables { get; set; }

    [JsonPropertyName("memoryReference")]
    public string? MemoryReference { get; set; }

    [JsonPropertyName("valueLocationReference")]
    public int? ValueLocationReference { get; set; }
}
