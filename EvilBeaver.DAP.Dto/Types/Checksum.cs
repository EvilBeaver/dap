// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


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
