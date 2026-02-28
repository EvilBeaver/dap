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
