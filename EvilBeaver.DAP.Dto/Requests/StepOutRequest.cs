using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class StepOutRequest : Request<StepOutArguments>
{
    public StepOutRequest() => Command = "stepOut";
}

public class StepOutArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }

    [JsonPropertyName("granularity")]
    public string? Granularity { get; set; }
}

public class StepOutResponse : Response
{
}
