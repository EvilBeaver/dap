// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class BreakpointLocationsRequest : Request<BreakpointLocationsArguments>
{
    public BreakpointLocationsRequest() => Command = "breakpointLocations";
}

public class BreakpointLocationsArguments
{
    [JsonPropertyName("source")]
    public Source Source { get; set; } = default!;

    [JsonPropertyName("line")]
    public int Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }

    [JsonPropertyName("endLine")]
    public int? EndLine { get; set; }

    [JsonPropertyName("endColumn")]
    public int? EndColumn { get; set; }
}

public class BreakpointLocationsResponse : Response<BreakpointLocationsResponseBody>
{
}

public class BreakpointLocationsResponseBody
{
    [JsonPropertyName("breakpoints")]
    public BreakpointLocation[] Breakpoints { get; set; } = default!;
}
