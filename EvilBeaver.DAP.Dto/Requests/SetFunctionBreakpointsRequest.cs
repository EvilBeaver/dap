using System.Text.Json.Serialization;
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
