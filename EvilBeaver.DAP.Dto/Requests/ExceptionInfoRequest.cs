using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ExceptionInfoRequest : Request<ExceptionInfoArguments>
{
    public ExceptionInfoRequest() => Command = "exceptionInfo";
}

public class ExceptionInfoArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }
}

public class ExceptionInfoResponse : Response<ExceptionInfoResponseBody>
{
}

public class ExceptionInfoResponseBody
{
    [JsonPropertyName("exceptionId")]
    public string ExceptionId { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("breakMode")]
    public string BreakMode { get; set; } = default!;

    [JsonPropertyName("details")]
    public ExceptionDetails? Details { get; set; }
}
