// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class StackTraceRequest : Request<StackTraceArguments>
{
    public StackTraceRequest() => Command = "stackTrace";
}

public class StackTraceArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("startFrame")]
    public int? StartFrame { get; set; }

    [JsonPropertyName("levels")]
    public int? Levels { get; set; }

    [JsonPropertyName("format")]
    public StackFrameFormat? Format { get; set; }
}

public class StackTraceResponse : Response<StackTraceResponseBody>
{
}

public class StackTraceResponseBody
{
    [JsonPropertyName("stackFrames")]
    public StackFrame[] StackFrames { get; set; } = default!;

    [JsonPropertyName("totalFrames")]
    public int? TotalFrames { get; set; }
}
