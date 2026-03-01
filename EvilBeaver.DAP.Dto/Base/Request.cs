// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.


namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// A client or debug adapter initiated request.
/// </summary>
public class Request : ProtocolMessage
{
    public override string Type => "request";

    /// <summary>
    /// The command to execute.
    /// </summary>
    [JsonPropertyName("command")]
    public string Command { get; set; } = default!;

    /// <summary>
    /// Object containing arguments for the command.
    /// </summary>
    [JsonPropertyName("arguments")]
    public object? Arguments { get; set; }
}

/// <summary>
/// Typed request with specific arguments.
/// </summary>
public class Request<TArgs> : Request
{
    [JsonPropertyName("arguments")]
    public new TArgs? Arguments { get; set; }
}
