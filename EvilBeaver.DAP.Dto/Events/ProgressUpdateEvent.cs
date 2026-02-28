using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ProgressUpdateEvent : Event<ProgressUpdateEventBody>
{
    public ProgressUpdateEvent() => EventType = "progressUpdate";
}

public class ProgressUpdateEventBody
{
    [JsonPropertyName("progressId")]
    public string ProgressId { get; set; } = default!;

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("percentage")]
    public int? Percentage { get; set; }
}
