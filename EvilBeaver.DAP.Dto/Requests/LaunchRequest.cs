// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class LaunchRequest : Request<LaunchRequestArguments>
{
    public LaunchRequest() => Command = "launch";
}

public class LaunchRequestArguments
{
    [JsonPropertyName("noDebug")]
    public bool? NoDebug { get; set; }

    [JsonPropertyName("__restart")]
    public object? Restart { get; set; }
    
    // Additional attributes are implementation specific.
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }
}

public class LaunchResponse : Response
{
}
