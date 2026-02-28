using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class RestartRequest : Request<RestartArguments>
{
    public RestartRequest() => Command = "restart";
}

public class RestartArguments
{
    [JsonPropertyName("arguments")]
    public object? Arguments { get; set; } // LaunchRequestArguments | AttachRequestArguments
}

public class RestartResponse : Response
{
}
