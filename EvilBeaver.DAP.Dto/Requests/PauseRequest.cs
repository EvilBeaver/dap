using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class PauseRequest : Request<PauseArguments>
{
    public PauseRequest() => Command = "pause";
}

public class PauseArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }
}

public class PauseResponse : Response
{
}
