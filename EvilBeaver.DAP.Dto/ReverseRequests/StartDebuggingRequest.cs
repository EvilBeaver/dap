using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.ReverseRequests;

public class StartDebuggingRequest : Request<StartDebuggingRequestArguments>
{
    public StartDebuggingRequest() => Command = "startDebugging";
}

public class StartDebuggingRequestArguments
{
    [JsonPropertyName("configuration")]
    public Dictionary<string, object> Configuration { get; set; } = default!;

    [JsonPropertyName("request")]
    public string RequestType { get; set; } = default!;
}

public static class StartDebuggingRequestType
{
    public const string Launch = "launch";
    public const string Attach = "attach";
}

public class StartDebuggingResponse : Response
{
}
