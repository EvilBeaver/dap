// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class ScopesRequest : Request<ScopesArguments>
{
    public ScopesRequest() => Command = "scopes";
}

public class ScopesArguments
{
    [JsonPropertyName("frameId")]
    public int FrameId { get; set; }
}

public class ScopesResponse : Response<ScopesResponseBody>
{
}

public class ScopesResponseBody
{
    [JsonPropertyName("scopes")]
    public Scope[] Scopes { get; set; } = default!;
}
