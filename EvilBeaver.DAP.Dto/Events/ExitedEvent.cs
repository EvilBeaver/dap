using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ExitedEvent : Event<ExitedEventBody>
{
    public override string EventType => "exited";
}

public class ExitedEventBody
{
    [JsonPropertyName("exitCode")]
    public int ExitCode { get; set; }
}
