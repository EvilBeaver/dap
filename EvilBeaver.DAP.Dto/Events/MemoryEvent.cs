using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class MemoryEvent : Event<MemoryEventBody>
{
    public MemoryEvent() => EventType = "memory";
}

public class MemoryEventBody
{
    [JsonPropertyName("memoryReference")]
    public string MemoryReference { get; set; } = default!;

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}
