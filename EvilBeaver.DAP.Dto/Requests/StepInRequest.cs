// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class StepInRequest : Request<StepInArguments>
{
    public StepInRequest() => Command = "stepIn";
}

public class StepInArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }

    [JsonPropertyName("targetId")]
    public int? TargetId { get; set; }

    [JsonPropertyName("granularity")]
    public string? Granularity { get; set; }
}

public class StepInResponse : Response
{
}
