// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class DisconnectRequest : Request<DisconnectArguments>
{
    public DisconnectRequest() => Command = "disconnect";
}

public class DisconnectArguments
{
    [JsonPropertyName("restart")]
    public bool? Restart { get; set; }

    [JsonPropertyName("terminateDebuggee")]
    public bool? TerminateDebuggee { get; set; }

    [JsonPropertyName("suspendDebuggee")]
    public bool? SuspendDebuggee { get; set; }
}

public class DisconnectResponse : Response
{
}
