// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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

    // Additional attributes are implementation specific.
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalData { get; set; }
}

public class AttachResponse : Response
{
}
