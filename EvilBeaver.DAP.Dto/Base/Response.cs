// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// Response for a request.
/// </summary>
public class Response : ProtocolMessage
{
    public override string Type => "response";

    /// <summary>
    /// Sequence number of the corresponding request.
    /// </summary>
    [JsonPropertyName("request_seq")]
    public int RequestSeq { get; set; }

    /// <summary>
    /// Outcome of the request.
    /// If true, the request was successful and the `body` attribute may contain
    /// the result of the request.
    /// If the value is false, the attribute `message` contains the error in short
    /// form and the `body` may contain additional information (see
    /// `ErrorResponse.body.error`).
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// The command requested.
    /// </summary>
    [JsonPropertyName("command")]
    public string Command { get; set; } = default!;

    /// <summary>
    /// Contains the raw error in short form if `success` is false.
    /// This raw error might be interpreted by the client and is not shown in the
    /// UI.
    /// Some predefined values exist.
    /// Values:
    /// 'cancelled': the request was cancelled.
    /// 'notStopped': the request may be retried once the adapter is in a 'stopped'
    /// state.
    /// etc.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Contains request result if success is true and error details if success is
    /// false.
    /// </summary>
    [JsonPropertyName("body")]
    public object? Body { get; set; }
}

/// <summary>
/// Typed response with specific body.
/// </summary>
public class Response<TBody> : Response
{
    [JsonPropertyName("body")]
    public new TBody? Body { get; set; }
}
