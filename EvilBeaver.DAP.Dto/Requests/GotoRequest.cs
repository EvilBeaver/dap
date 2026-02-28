using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class GotoRequest : Request<GotoArguments>
{
    public GotoRequest() => Command = "goto";
}

public class GotoArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("targetId")]
    public int TargetId { get; set; }
}

public class GotoResponse : Response
{
}
