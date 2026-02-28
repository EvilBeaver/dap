using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Events;

public class ModuleEvent : Event<ModuleEventBody>
{
    public override string EventType => "module";
}

public class ModuleEventBody
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; } = default!;

    [JsonPropertyName("module")]
    public Module Module { get; set; } = default!;
}

public static class ModuleReason
{
    public const string New = "new";
    public const string Changed = "changed";
    public const string Removed = "removed";
}
