using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionDetails
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("typeName")]
    public string? TypeName { get; set; }

    [JsonPropertyName("fullTypeName")]
    public string? FullTypeName { get; set; }

    [JsonPropertyName("evaluateName")]
    public string? EvaluateName { get; set; }

    [JsonPropertyName("stackTrace")]
    public string? StackTrace { get; set; }

    [JsonPropertyName("innerException")]
    public ExceptionDetails[]? InnerException { get; set; }
}
