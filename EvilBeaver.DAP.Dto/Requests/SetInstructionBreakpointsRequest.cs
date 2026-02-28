// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetInstructionBreakpointsRequest : Request<SetInstructionBreakpointsArguments>
{
    public SetInstructionBreakpointsRequest() => Command = "setInstructionBreakpoints";
}

public class SetInstructionBreakpointsArguments
{
    [JsonPropertyName("breakpoints")]
    public InstructionBreakpoint[] Breakpoints { get; set; } = default!;
}

public class SetInstructionBreakpointsResponse : Response<SetInstructionBreakpointsResponseBody>
{
}

public class SetInstructionBreakpointsResponseBody
{
    [JsonPropertyName("breakpoints")]
    public Breakpoint[] Breakpoints { get; set; } = default!;
}
