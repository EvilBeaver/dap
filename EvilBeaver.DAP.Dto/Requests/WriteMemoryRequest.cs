using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class WriteMemoryRequest : Request<WriteMemoryArguments>
{
    public WriteMemoryRequest() => Command = "writeMemory";
}

public class WriteMemoryArguments
{
    [JsonPropertyName("memoryReference")]
    public string MemoryReference { get; set; } = default!;

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("allowPartial")]
    public bool? AllowPartial { get; set; }

    [JsonPropertyName("data")]
    public string Data { get; set; } = default!;
}

public class WriteMemoryResponse : Response<WriteMemoryResponseBody>
{
}

public class WriteMemoryResponseBody
{
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("bytesWritten")]
    public int? BytesWritten { get; set; }
}
