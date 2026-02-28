// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class StoppedEvent : Event<StoppedEventBody>
{
    public StoppedEvent() => EventType = "stopped";
}

public class StoppedEventBody
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("threadId")]
    public int? ThreadId { get; set; }

    [JsonPropertyName("preserveFocusHint")]
    public bool? PreserveFocusHint { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("allThreadsStopped")]
    public bool? AllThreadsStopped { get; set; }

    [JsonPropertyName("hitBreakpointIds")]
    public int[]? HitBreakpointIds { get; set; }
}

public static class StoppedReason
{
    public const string Step = "step";
    public const string Breakpoint = "breakpoint";
    public const string Exception = "exception";
    public const string Pause = "pause";
    public const string Entry = "entry";
    public const string Goto = "goto";
    public const string FunctionBreakpoint = "function breakpoint";
    public const string DataBreakpoint = "data breakpoint";
    public const string InstructionBreakpoint = "instruction breakpoint";
}
