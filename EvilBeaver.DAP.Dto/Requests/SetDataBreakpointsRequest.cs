// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetDataBreakpointsRequest : Request<SetDataBreakpointsArguments>
{
    public SetDataBreakpointsRequest() => Command = "setDataBreakpoints";
}

public class SetDataBreakpointsArguments
{
    [JsonPropertyName("breakpoints")]
    public DataBreakpoint[] Breakpoints { get; set; } = default!;
}

public class SetDataBreakpointsResponse : Response<SetDataBreakpointsResponseBody>
{
}

public class SetDataBreakpointsResponseBody
{
    [JsonPropertyName("breakpoints")]
    public Breakpoint[] Breakpoints { get; set; } = default!;
}
