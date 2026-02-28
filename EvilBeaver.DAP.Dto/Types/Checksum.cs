using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class Checksum
{
    [JsonPropertyName("algorithm")]
    public string Algorithm { get; set; } = default!;

    [JsonPropertyName("checksum")]
    public string ChecksumValue { get; set; } = default!;
}

public static class ChecksumAlgorithm
{
    public const string MD5 = "MD5";
    public const string SHA1 = "SHA1";
    public const string SHA256 = "SHA256";
    public const string Timestamp = "timestamp";
}
