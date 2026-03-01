// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Events;

public class CapabilitiesEvent : Event<CapabilitiesEventBody>
{
    public CapabilitiesEvent() => EventType = "capabilities";
}

public class CapabilitiesEventBody
{
    [JsonPropertyName("capabilities")]
    public Capabilities Capabilities { get; set; } = default!;
}
