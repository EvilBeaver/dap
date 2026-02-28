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
