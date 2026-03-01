// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// On error (whenever `success` is false), the body can provide more details.
/// </summary>
public class ErrorResponse : Response<ErrorResponseBody>
{
}

public class ErrorResponseBody
{
    /// <summary>
    /// A structured error message.
    /// </summary>
    [JsonPropertyName("error")]
    public Message? Error { get; set; }
}
