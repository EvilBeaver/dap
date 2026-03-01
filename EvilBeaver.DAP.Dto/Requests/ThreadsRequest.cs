// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ThreadsRequest : Request
{
    public ThreadsRequest() => Command = "threads";
}

public class ThreadsResponse : Response<ThreadsResponseBody>
{
}

public class ThreadsResponseBody
{
    [JsonPropertyName("threads")]
    public EvilBeaver.DAP.Dto.Types.Thread[] Threads { get; set; } = default!;
}
