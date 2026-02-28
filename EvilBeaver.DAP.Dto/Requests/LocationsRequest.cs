using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class LocationsRequest : Request<LocationsArguments>
{
    public LocationsRequest() => Command = "locations";
}

public class LocationsArguments
{
    [JsonPropertyName("locationReference")]
    public int LocationReference { get; set; }
}

public class LocationsResponse : Response<LocationsResponseBody>
{
}

public class LocationsResponseBody
{
    [JsonPropertyName("source")]
    public Source Source { get; set; } = default!;

    [JsonPropertyName("line")]
    public int Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }

    [JsonPropertyName("endLine")]
    public int? EndLine { get; set; }

    [JsonPropertyName("endColumn")]
    public int? EndColumn { get; set; }
}
