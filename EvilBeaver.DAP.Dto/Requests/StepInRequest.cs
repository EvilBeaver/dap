using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class StepInRequest : Request<StepInArguments>
{
    public StepInRequest() => Command = "stepIn";
}

public class StepInArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }

    [JsonPropertyName("targetId")]
    public int? TargetId { get; set; }

    [JsonPropertyName("granularity")]
    public string? Granularity { get; set; }
}

public class StepInResponse : Response
{
}
