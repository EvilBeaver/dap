// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

/// <summary>
/// A structured message object. Used to return errors from requests.
/// </summary>
public class Message
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; } = "";

    [JsonPropertyName("variables")]
    public Dictionary<string, string>? Variables { get; set; }

    [JsonPropertyName("sendTelemetry")]
    public bool? SendTelemetry { get; set; }

    [JsonPropertyName("showUser")]
    public bool? ShowUser { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("urlLabel")]
    public string? UrlLabel { get; set; }
}
