// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
