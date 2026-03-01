// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ExitedEvent : Event<ExitedEventBody>
{
    public ExitedEvent() => EventType = "exited";
}

public class ExitedEventBody
{
    [JsonPropertyName("exitCode")]
    public int ExitCode { get; set; }
}
