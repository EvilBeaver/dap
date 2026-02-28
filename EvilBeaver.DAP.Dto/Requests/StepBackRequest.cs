using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class StepBackRequest : Request<StepBackArguments>
{
    public StepBackRequest() => Command = "stepBack";
}

public class StepBackArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }

    [JsonPropertyName("granularity")]
    public string? Granularity { get; set; }
}

public class StepBackResponse : Response
{
}
