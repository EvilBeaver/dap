// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionFilterOptions
{
    [JsonPropertyName("filterId")]
    public string FilterId { get; set; } = default!;

    [JsonPropertyName("condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }
}
