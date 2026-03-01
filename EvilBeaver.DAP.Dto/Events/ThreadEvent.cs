// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ThreadEvent : Event<ThreadEventBody>
{
    public ThreadEvent() => EventType = "thread";
}

public class ThreadEventBody
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;

    [JsonPropertyName("threadId")]
    public int ThreadId { get; set; }
}

public static class ThreadReason
{
    public const string Started = "started";
    public const string Exited = "exited";
}
