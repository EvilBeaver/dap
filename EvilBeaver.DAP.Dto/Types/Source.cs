// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

/// <summary>
/// A descriptor for source code.
/// </summary>
public class Source
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("sourceReference")]
    public int? SourceReference { get; set; }

    [JsonPropertyName("presentationHint")]
    public string? PresentationHint { get; set; }

    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    [JsonPropertyName("sources")]
    public Source[]? Sources { get; set; }

    [JsonPropertyName("adapterData")]
    public object? AdapterData { get; set; }

    [JsonPropertyName("checksums")]
    public Checksum[]? Checksums { get; set; }
}

public static class SourcePresentationHint
{
    public const string Normal = "normal";
    public const string Emphasize = "emphasize";
    public const string Deemphasize = "deemphasize";
}
