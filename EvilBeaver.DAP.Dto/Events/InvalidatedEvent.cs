using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class InvalidatedEvent : Event<InvalidatedEventBody>
{
    public override string EventType => "invalidated";
}

public class InvalidatedEventBody
{
    [JsonPropertyName("areas")]
    public string[]? Areas { get; set; }

    [JsonPropertyName("threadId")]
    public int? ThreadId { get; set; }

    [JsonPropertyName("stackFrameId")]
    public int? StackFrameId { get; set; }
}
