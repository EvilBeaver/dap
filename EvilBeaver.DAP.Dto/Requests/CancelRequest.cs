using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class CancelRequest : Request<CancelArguments>
{
    public CancelRequest() => Command = "cancel";
}

public class CancelArguments
{
    [JsonPropertyName("requestId")]
    public int? RequestId { get; set; }

    [JsonPropertyName("progressId")]
    public string? ProgressId { get; set; }
}

public class CancelResponse : Response
{
}
