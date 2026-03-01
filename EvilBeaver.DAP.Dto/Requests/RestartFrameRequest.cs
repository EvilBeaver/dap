// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class RestartFrameRequest : Request<RestartFrameArguments>
{
    public RestartFrameRequest() => Command = "restartFrame";
}

public class RestartFrameArguments
{
    [JsonPropertyName("frameId")]
    public int FrameId { get; set; }
}

public class RestartFrameResponse : Response
{
}
