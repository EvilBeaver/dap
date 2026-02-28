using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class DataBreakpoint
{
    [JsonPropertyName("dataId")]
    public string DataId { get; set; } = default!;

    [JsonPropertyName("accessType")]
    public string? AccessType { get; set; }

    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("hitCondition")]
    public string? HitCondition { get; set; }
}

public static class DataBreakpointAccessType
{
    public const string Read = "read";
    public const string Write = "write";
    public const string ReadWrite = "readWrite";
}
