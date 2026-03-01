// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class BreakpointMode
{
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = default!;

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("appliesTo")]
    public string[] AppliesTo { get; set; } = default!;
}

public static class BreakpointModeApplicability
{
    public const string Source = "source";
    public const string Exception = "exception";
    public const string Data = "data";
    public const string Instruction = "instruction";
}
