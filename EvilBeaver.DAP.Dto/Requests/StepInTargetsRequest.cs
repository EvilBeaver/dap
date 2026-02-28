// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class StepInTargetsRequest : Request<StepInTargetsArguments>
{
    public StepInTargetsRequest() => Command = "stepInTargets";
}

public class StepInTargetsArguments
{
    [JsonPropertyName("frameId")]
    public int FrameId { get; set; }
}

public class StepInTargetsResponse : Response<StepInTargetsResponseBody>
{
}

public class StepInTargetsResponseBody
{
    [JsonPropertyName("targets")]
    public StepInTarget[] Targets { get; set; } = default!;
}
