using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ProgressEndEvent : Event<ProgressEndEventBody>
{
    public override string EventType => "progressEnd";
}

public class ProgressEndEventBody
{
    [JsonPropertyName("progressId")]
    public string ProgressId { get; set; } = default!;

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
