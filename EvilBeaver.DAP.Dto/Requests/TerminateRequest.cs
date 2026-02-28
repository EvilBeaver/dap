using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class TerminateRequest : Request<TerminateArguments>
{
    public TerminateRequest() => Command = "terminate";
}

public class TerminateArguments
{
    [JsonPropertyName("restart")]
    public bool? Restart { get; set; }
}

public class TerminateResponse : Response
{
}
