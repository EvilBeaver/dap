// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Events;

public class BreakpointEvent : Event<BreakpointEventBody>
{
    public BreakpointEvent() => EventType = "breakpoint";
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
