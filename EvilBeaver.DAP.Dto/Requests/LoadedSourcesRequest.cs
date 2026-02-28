using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class LoadedSourcesRequest : Request<LoadedSourcesArguments>
{
    public LoadedSourcesRequest() => Command = "loadedSources";
}

public class LoadedSourcesArguments
{
}

public class LoadedSourcesResponse : Response<LoadedSourcesResponseBody>
{
}

public class LoadedSourcesResponseBody
{
    [JsonPropertyName("sources")]
    public Source[] Sources { get; set; } = default!;
}
