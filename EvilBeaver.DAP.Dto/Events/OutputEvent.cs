using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Events;

public class OutputEvent : Event<OutputEventBody>
{
    public OutputEvent() => EventType = "output";
}

public class OutputEventBody
{
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("output")]
    public string Output { get; set; } = default!;

    [JsonPropertyName("group")]
    public string? Group { get; set; }

    [JsonPropertyName("variablesReference")]
    public int? VariablesReference { get; set; }

    [JsonPropertyName("source")]
    public Source? Source { get; set; }

    [JsonPropertyName("line")]
    public int? Line { get; set; }

    [JsonPropertyName("column")]
    public int? Column { get; set; }

    [JsonPropertyName("data")]
    public object? Data { get; set; }

    [JsonPropertyName("locationReference")]
    public int? LocationReference { get; set; }
}

public static class OutputCategory
{
    public const string Console = "console";
    public const string Important = "important";
    public const string Stdout = "stdout";
    public const string Stderr = "stderr";
    public const string Telemetry = "telemetry";
}

public static class OutputGroup
{
    public const string Start = "start";
    public const string StartCollapsed = "startCollapsed";
    public const string End = "end";
}
