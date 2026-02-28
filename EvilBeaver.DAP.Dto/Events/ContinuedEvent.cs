using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ContinuedEvent : Event<ContinuedEventBody>
{
    public ContinuedEvent() => EventType = "continued";
}

public class ContinuedEventBody
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("allThreadsContinued")]
    public bool? AllThreadsContinued { get; set; }
}
