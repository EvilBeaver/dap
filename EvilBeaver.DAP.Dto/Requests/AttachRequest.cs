using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class AttachRequest : Request<AttachRequestArguments>
{
    public AttachRequest() => Command = "attach";
}

public class AttachRequestArguments
{
    [JsonPropertyName("__restart")]
    public object? Restart { get; set; }

    // Additional attributes are implementation specific.
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }
}

public class AttachResponse : Response
{
}
