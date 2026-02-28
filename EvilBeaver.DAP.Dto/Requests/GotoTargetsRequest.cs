using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class GotoTargetsRequest : Request<GotoTargetsArguments>
{
    public GotoTargetsRequest() => Command = "gotoTargets";
}

public class GotoTargetsArguments
{
    [JsonPropertyName("source")]
    public Source Source { get; set; } = default!;

    [JsonPropertyName("line")]
    public int Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }
}

public class GotoTargetsResponse : Response<GotoTargetsResponseBody>
{
}

public class GotoTargetsResponseBody
{
    [JsonPropertyName("targets")]
    public GotoTarget[] Targets { get; set; } = default!;
}
