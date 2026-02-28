using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class InitializedEvent : Event
{
    public InitializedEvent() => EventType = "initialized";
}
