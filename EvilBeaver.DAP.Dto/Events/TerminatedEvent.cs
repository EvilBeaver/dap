using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class TerminatedEvent : Event<TerminatedEventBody>
{
    public TerminatedEvent() => EventType = "terminated";
}

public class TerminatedEventBody
{
    [JsonPropertyName("restart")]
    public object? Restart { get; set; }
}
