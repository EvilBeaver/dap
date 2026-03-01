// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class Module
{
    [JsonPropertyName("id")]
    public object Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("isOptimized")]
    public bool? IsOptimized { get; set; }

    [JsonPropertyName("isUserCode")]
    public bool? IsUserCode { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("symbolStatus")]
    public string? SymbolStatus { get; set; }

    [JsonPropertyName("symbolFilePath")]
    public string? SymbolFilePath { get; set; }

    [JsonPropertyName("dateTimeStamp")]
    public string? DateTimeStamp { get; set; }

    [JsonPropertyName("addressRange")]
    public string? AddressRange { get; set; }
}
