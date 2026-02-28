// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

public class ColumnDescriptor
{
    [JsonPropertyName("attributeName")]
    public string AttributeName { get; set; } = default!;

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("width")]
    public int? Width { get; set; }
}

public static class ColumnDescriptorType
{
    public const string String = "string";
    public const string Number = "number";
    public const string Boolean = "boolean";
    public const string UnixTimestampUTC = "unixTimestampUTC";
}
