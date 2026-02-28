// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class TerminateThreadsRequest : Request<TerminateThreadsArguments>
{
    public TerminateThreadsRequest() => Command = "terminateThreads";
}

public class TerminateThreadsArguments
{
    [JsonPropertyName("threadIds")]
    public int[]? ThreadIds { get; set; }
}

public class TerminateThreadsResponse : Response
{
}
