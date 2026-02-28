using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Dto.Events;

public class ProcessEvent : Event<ProcessEventBody>
{
    public ProcessEvent() => EventType = "process";
}

public class ProcessEventBody
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("systemProcessId")]
    public int? SystemProcessId { get; set; }

    [JsonPropertyName("isLocalProcess")]
    public bool? IsLocalProcess { get; set; }

    [JsonPropertyName("startMethod")]
    public string? StartMethod { get; set; }

    [JsonPropertyName("pointerSize")]
    public int? PointerSize { get; set; }
}

public static class ProcessStartMethod
{
    public const string Launch = "launch";
    public const string Attach = "attach";
    public const string AttachForSuspendedLaunch = "attachForSuspendedLaunch";
}
