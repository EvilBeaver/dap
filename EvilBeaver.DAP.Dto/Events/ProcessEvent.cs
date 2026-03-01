// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ProcessEvent : Event<ProcessEventBody>
{
    public ProcessEvent() => EventType = "process";
}

public class ProcessEventBody
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("systemProcessId")]
    public int? SystemProcessId { get; set; }

    [JsonPropertyName("isLocalProcess")]
    public bool? IsLocalProcess { get; set; }

    [JsonPropertyName("startMethod")]
    public string? StartMethod { get; set; }

    [JsonPropertyName("pointerSize")]
    public int? PointerSize { get; set; }
}

public static class ProcessStartMethod
{
    public const string Launch = "launch";
    public const string Attach = "attach";
    public const string AttachForSuspendedLaunch = "attachForSuspendedLaunch";
}
