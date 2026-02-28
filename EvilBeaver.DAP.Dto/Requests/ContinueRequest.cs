using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class ContinueRequest : Request<ContinueArguments>
{
    public ContinueRequest() => Command = "continue";
}

public class ContinueArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }
}

public class ContinueResponse : Response<ContinueResponseBody>
{
}

public class ContinueResponseBody
{
    [JsonPropertyName("allThreadsContinued")]
    public bool? AllThreadsContinued { get; set; }
}
