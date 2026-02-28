using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Events;

public class BreakpointEvent : Event<BreakpointEventBody>
{
    public override string EventType => "breakpoint";
}

public class BreakpointEventBody
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;

    [JsonPropertyName("breakpoint")]
    public Breakpoint Breakpoint { get; set; } = default!;
}

public static class BreakpointReasonEvent
{
    public const string Changed = "changed";
    public const string New = "new";
    public const string Removed = "removed";
}
