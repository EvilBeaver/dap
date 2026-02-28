using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class RestartFrameRequest : Request<RestartFrameArguments>
{
    public RestartFrameRequest() => Command = "restartFrame";
}

public class RestartFrameArguments
{
    [JsonPropertyName("frameId")]
    public int FrameId { get; set; }
}

public class RestartFrameResponse : Response
{
}
