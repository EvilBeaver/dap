// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class ReverseContinueRequest : Request<ReverseContinueArguments>
{
    public ReverseContinueRequest() => Command = "reverseContinue";
}

public class ReverseContinueArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }
}

public class ReverseContinueResponse : Response
{
}
