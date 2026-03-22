// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

#if !NETSTANDARD2_0
using System.Text.Json;
#endif
#if NETSTANDARD2_0
using Newtonsoft.Json.Linq;
#endif
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class AttachRequest : Request<AttachRequestArguments>
{
    public AttachRequest() => Command = "attach";
}

public class AttachRequestArguments
{
    [JsonPropertyName("__restart")]
    public object? Restart { get; set; }

    /// <summary>
    /// Implementation-specific attributes (JSON overflow). Standard properties are not duplicated here.
    /// On .NET (System.Text.Json) values are <see cref="JsonElement"/>; on .NET Standard 2.0 — <c>JToken</c>.
    /// </summary>
    [JsonExtensionData]
#if NETSTANDARD2_0
    public Dictionary<string, JToken>? AdditionalData { get; set; }
#else
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
#endif
}

public class AttachResponse : Response
{
}
