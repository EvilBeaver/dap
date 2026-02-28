// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Base;

/// <summary>
/// A debug adapter initiated event.
/// </summary>
public class Event : ProtocolMessage
{
    public override string Type => "event";

    /// <summary>
    /// Type of event.
    /// </summary>
    [JsonPropertyName("event")]
    public string EventType { get; set; } = default!;

    /// <summary>
    /// Event-specific information.
    /// </summary>
    [JsonPropertyName("body")]
    public object? Body { get; set; }
}

/// <summary>
/// Typed event with specific body.
/// </summary>
public class Event<TBody> : Event
{
    [JsonPropertyName("body")]
    public new TBody? Body { get; set; }
}
