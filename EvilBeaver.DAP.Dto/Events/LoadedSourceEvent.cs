// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Events;

public class LoadedSourceEvent : Event<LoadedSourceEventBody>
{
    public LoadedSourceEvent() => EventType = "loadedSource";
}

public class LoadedSourceEventBody
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;

    [JsonPropertyName("source")]
    public Source Source { get; set; } = default!;
}

public static class LoadedSourceReason
{
    public const string New = "new";
    public const string Changed = "changed";
    public const string Removed = "removed";
}
