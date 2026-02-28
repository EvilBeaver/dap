// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class RestartRequest : Request<RestartArguments>
{
    public RestartRequest() => Command = "restart";
}

public class RestartArguments
{
    [JsonPropertyName("arguments")]
    public object? Arguments { get; set; } // LaunchRequestArguments | AttachRequestArguments
}

public class RestartResponse : Response
{
}
