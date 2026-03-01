// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ContinuedEvent : Event<ContinuedEventBody>
{
    public ContinuedEvent() => EventType = "continued";
}

public class ContinuedEventBody
{
    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }

    [JsonPropertyName("allThreadsContinued")]
    public bool? AllThreadsContinued { get; set; }
}
