using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// On error (whenever `success` is false), the body can provide more details.
/// </summary>
public class ErrorResponse : Response<ErrorResponseBody>
{
}

public class ErrorResponseBody
{
    /// <summary>
    /// A structured error message.
    /// </summary>
    [JsonPropertyName("error")]
    public Message? Error { get; set; }
}
