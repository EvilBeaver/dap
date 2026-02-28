using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SetExceptionBreakpointsRequest : Request<SetExceptionBreakpointsArguments>
{
    public SetExceptionBreakpointsRequest() => Command = "setExceptionBreakpoints";
}

public class SetExceptionBreakpointsArguments
{
    [JsonPropertyName("filters")]
    public string[] Filters { get; set; } = default!;

    [JsonPropertyName("filterOptions")]
    public ExceptionFilterOptions[]? FilterOptions { get; set; }

    [JsonPropertyName("exceptionOptions")]
    public ExceptionOptions[]? ExceptionOptions { get; set; }
}

public class SetExceptionBreakpointsResponse : Response<SetExceptionBreakpointsResponseBody>
{
}

public class SetExceptionBreakpointsResponseBody
{
    [JsonPropertyName("breakpoints")]
    public Breakpoint[]? Breakpoints { get; set; }
}
