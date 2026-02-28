using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class SourceRequest : Request<SourceArguments>
{
    public SourceRequest() => Command = "source";
}

public class SourceArguments
{
    [JsonPropertyName("source")]
    public Source? Source { get; set; }

    [JsonPropertyName("sourceReference")]
    public int SourceReference { get; set; }
}

public class SourceResponse : Response<SourceResponseBody>
{
}

public class SourceResponseBody
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }
}
