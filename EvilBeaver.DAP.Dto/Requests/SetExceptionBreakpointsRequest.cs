// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
