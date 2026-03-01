// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetBreakpointsRequest : Request<SetBreakpointsArguments>
{
    public SetBreakpointsRequest() => Command = "setBreakpoints";
}

public class SetBreakpointsArguments
{
    [JsonPropertyName("source")]
    public Source Source { get; set; } = default!;

    [JsonPropertyName("breakpoints")]
    public SourceBreakpoint[]? Breakpoints { get; set; }

    [JsonPropertyName("lines")]
    public int[]? Lines { get; set; }

    [JsonPropertyName("sourceModified")]
    public bool? SourceModified { get; set; }
}

public class SetBreakpointsResponse : Response<SetBreakpointsResponseBody>
{
}

public class SetBreakpointsResponseBody
{
    [JsonPropertyName("breakpoints")]
    public Breakpoint[] Breakpoints { get; set; } = default!;
}
