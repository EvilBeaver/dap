// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class InstructionBreakpoint
{
    [JsonPropertyName("instructionReference")]
    public string InstructionReference { get; set; } = default!;

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("hitCondition")]
    public string? HitCondition { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}
