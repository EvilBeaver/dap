// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ProgressUpdateEvent : Event<ProgressUpdateEventBody>
{
    public ProgressUpdateEvent() => EventType = "progressUpdate";
}

public class ProgressUpdateEventBody
{
    [JsonPropertyName("progressId")]
    public string ProgressId { get; set; } = default!;

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("percentage")]
    public int? Percentage { get; set; }
}
