using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ProgressStartEvent : Event<ProgressStartEventBody>
{
    public override string EventType => "progressStart";
}

public class ProgressStartEventBody
{
    [JsonPropertyName("progressId")]
    public string ProgressId { get; set; } = default!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;

    [JsonPropertyName("requestId")]
    public int? RequestId { get; set; }

    [JsonPropertyName("cancellable")]
    public bool? Cancellable { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("percentage")]
    public int? Percentage { get; set; }
}
