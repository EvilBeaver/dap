using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class LaunchRequest : Request<LaunchRequestArguments>
{
    public LaunchRequest() => Command = "launch";
}

public class LaunchRequestArguments
{
    [JsonPropertyName("noDebug")]
    public bool? NoDebug { get; set; }

    [JsonPropertyName("__restart")]
    public object? Restart { get; set; }
    
    // Additional attributes are implementation specific.
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }
}

public class LaunchResponse : Response
{
}
