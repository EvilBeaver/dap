using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionOptions
{
    [JsonPropertyName("path")]
    public ExceptionPathSegment[]? Path { get; set; }

    [JsonPropertyName("breakMode")]
    public string BreakMode { get; set; } = default!;
}

public static class ExceptionBreakMode
{
    public const string Never = "never";
    public const string Always = "always";
    public const string Unhandled = "unhandled";
    public const string UserUnhandled = "userUnhandled";
}
