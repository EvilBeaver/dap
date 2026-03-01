// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Types;

public class ExceptionPathSegment
{
    [JsonPropertyName("negate")]
    public bool? Negate { get; set; }

    [JsonPropertyName("names")]
    public string[] Names { get; set; } = default!;
}
