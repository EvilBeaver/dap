// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Requests;

public class CancelRequest : Request<CancelArguments>
{
    public CancelRequest() => Command = "cancel";
}

public class CancelArguments
{
    [JsonPropertyName("requestId")]
    public int? RequestId { get; set; }

    [JsonPropertyName("progressId")]
    public string? ProgressId { get; set; }
}

public class CancelResponse : Response
{
}
