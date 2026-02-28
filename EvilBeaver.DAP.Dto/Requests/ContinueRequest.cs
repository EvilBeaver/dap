// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class ContinueRequest : Request<ContinueArguments>
{
    public ContinueRequest() => Command = "continue";
}

public class ContinueArguments
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("singleThread")]
    public bool? SingleThread { get; set; }
}

public class ContinueResponse : Response<ContinueResponseBody>
{
}

public class ContinueResponseBody
{
    [JsonPropertyName("allThreadsContinued")]
    public bool? AllThreadsContinued { get; set; }
}
