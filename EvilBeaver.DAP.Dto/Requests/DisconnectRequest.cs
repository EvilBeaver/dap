using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class DisconnectRequest : Request<DisconnectArguments>
{
    public DisconnectRequest() => Command = "disconnect";
}

public class DisconnectArguments
{
    [JsonPropertyName("restart")]
    public bool? Restart { get; set; }

    [JsonPropertyName("terminateDebuggee")]
    public bool? TerminateDebuggee { get; set; }

    [JsonPropertyName("suspendDebuggee")]
    public bool? SuspendDebuggee { get; set; }
}

public class DisconnectResponse : Response
{
}
