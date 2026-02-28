using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ThreadsRequest : Request
{
    public ThreadsRequest() => Command = "threads";
}

public class ThreadsResponse : Response<ThreadsResponseBody>
{
}

public class ThreadsResponseBody
{
    [JsonPropertyName("threads")]
    public Thread[] Threads { get; set; } = default!;
}
