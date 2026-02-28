using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class TerminateThreadsRequest : Request<TerminateThreadsArguments>
{
    public TerminateThreadsRequest() => Command = "terminateThreads";
}

public class TerminateThreadsArguments
{
    [JsonPropertyName("threadIds")]
    public int[]? ThreadIds { get; set; }
}

public class TerminateThreadsResponse : Response
{
}
