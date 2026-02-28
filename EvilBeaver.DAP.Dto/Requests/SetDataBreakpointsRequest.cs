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
