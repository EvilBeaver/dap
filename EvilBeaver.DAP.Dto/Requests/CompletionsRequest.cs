using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class CompletionsRequest : Request<CompletionsArguments>
{
    public CompletionsRequest() => Command = "completions";
}

public class CompletionsArguments
{
    [JsonPropertyName("frameId")]
    public int? FrameId { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;

    [JsonPropertyName("column")]
    public int Column { get; set; }

    [JsonPropertyName("line")]
    public int? Line { get; set; }
}

public class CompletionsResponse : Response<CompletionsResponseBody>
{
}

public class CompletionsResponseBody
{
    [JsonPropertyName("targets")]
    public CompletionItem[] Targets { get; set; } = default!;
}
