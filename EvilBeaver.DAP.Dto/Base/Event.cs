using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// A debug adapter initiated event.
/// </summary>
public class Event : ProtocolMessage
{
    public override string Type => "event";

    /// <summary>
    /// Type of event.
    /// </summary>
    [JsonPropertyName("event")]
    public string EventType { get; set; } = default!;

    /// <summary>
    /// Event-specific information.
    /// </summary>
    [JsonPropertyName("body")]
    public object? Body { get; set; }
}

/// <summary>
/// Typed event with specific body.
/// </summary>
public class Event<TBody> : Event
{
    [JsonPropertyName("body")]
    public new TBody? Body { get; set; }
}
