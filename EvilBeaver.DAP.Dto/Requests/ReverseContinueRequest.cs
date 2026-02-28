using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class ReverseContinueRequest : Request<ReverseContinueArguments>
{
    public ReverseContinueRequest() => Command = "reverseContinue";
}

public class ReverseContinueArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }
}

public class ReverseContinueResponse : Response
{
}
