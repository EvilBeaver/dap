using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ScopesRequest : Request<ScopesArguments>
{
    public ScopesRequest() => Command = "scopes";
}

public class ScopesArguments
{
    [JsonPropertyName("frameId")]
    public int FrameId { get; set; }
}

public class ScopesResponse : Response<ScopesResponseBody>
{
}

public class ScopesResponseBody
{
    [JsonPropertyName("scopes")]
    public Scope[] Scopes { get; set; } = default!;
}
