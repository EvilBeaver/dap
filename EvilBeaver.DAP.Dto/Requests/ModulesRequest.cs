using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ModulesRequest : Request<ModulesArguments>
{
    public ModulesRequest() => Command = "modules";
}

public class ModulesArguments
{
    [JsonPropertyName("startModule")]
    public int? StartModule { get; set; }

    [JsonPropertyName("moduleCount")]
    public int? ModuleCount { get; set; }
}

public class ModulesResponse : Response<ModulesResponseBody>
{
}

public class ModulesResponseBody
{
    [JsonPropertyName("modules")]
    public Module[] Modules { get; set; } = default!;

    [JsonPropertyName("totalModules")]
    public int? TotalModules { get; set; }
}
