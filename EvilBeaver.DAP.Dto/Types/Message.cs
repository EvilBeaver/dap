using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

/// <summary>
/// A structured message object. Used to return errors from requests.
/// </summary>
public class Message
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; } = default!;

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
