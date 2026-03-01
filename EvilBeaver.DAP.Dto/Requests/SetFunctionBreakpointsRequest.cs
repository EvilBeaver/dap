// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetFunctionBreakpointsRequest : Request<SetFunctionBreakpointsArguments>
{
    public SetFunctionBreakpointsRequest() => Command = "setFunctionBreakpoints";
}

public class SetFunctionBreakpointsArguments
{
    [JsonPropertyName("breakpoints")]
    public FunctionBreakpoint[] Breakpoints { get; set; } = default!;
}

public class SetFunctionBreakpointsResponse : Response<SetFunctionBreakpointsResponseBody>
{
}

public class SetFunctionBreakpointsResponseBody
{
    [JsonPropertyName("breakpoints")]
    public Breakpoint[] Breakpoints { get; set; } = default!;
}
