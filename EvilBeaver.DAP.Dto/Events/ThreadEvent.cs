using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ThreadEvent : Event<ThreadEventBody>
{
    public override string EventType => "thread";
}

public class ThreadEventBody
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;

    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }
}

public static class ThreadReason
{
    public const string Started = "started";
    public const string Exited = "exited";
}
