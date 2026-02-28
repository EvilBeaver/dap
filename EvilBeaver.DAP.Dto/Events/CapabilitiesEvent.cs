using System.Text.Json.Serialization;
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
