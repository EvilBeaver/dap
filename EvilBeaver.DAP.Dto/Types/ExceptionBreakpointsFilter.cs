// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionBreakpointsFilter
{
    [JsonPropertyName("filter")]
    public string Filter { get; set; } = default!;

    [JsonPropertyName("label")]
    public string Label { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("default")]
    public bool? Default { get; set; }

    [JsonPropertyName("supportsCondition")]
    public bool? SupportsCondition { get; set; }

    [JsonPropertyName("conditionDescription")]
    public string? ConditionDescription { get; set; }
}
