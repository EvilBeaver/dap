using System.Text.Json.Serialization;
using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Dto.Requests;

public class InitializeRequest : Request<InitializeRequestArguments>
{
    public InitializeRequest() => Command = "initialize";
}

public class InitializeRequestArguments
{
    [JsonPropertyName("clientID")]
    public string? ClientId { get; set; }

    [JsonPropertyName("clientName")]
    public string? ClientName { get; set; }

    [JsonPropertyName("adapterID")]
    public string AdapterId { get; set; } = default!;

    [JsonPropertyName("locale")]
    public string? Locale { get; set; }

    [JsonPropertyName("linesStartAt1")]
    public bool? LinesStartAt1 { get; set; }

    [JsonPropertyName("columnsStartAt1")]
    public bool? ColumnsStartAt1 { get; set; }

    [JsonPropertyName("pathFormat")]
    public string? PathFormat { get; set; }

    [JsonPropertyName("supportsVariableType")]
    public bool? SupportsVariableType { get; set; }

    [JsonPropertyName("supportsVariablePaging")]
    public bool? SupportsVariablePaging { get; set; }

    [JsonPropertyName("supportsRunInTerminalRequest")]
    public bool? SupportsRunInTerminalRequest { get; set; }

    [JsonPropertyName("supportsMemoryReferences")]
    public bool? SupportsMemoryReferences { get; set; }

    [JsonPropertyName("supportsProgressReporting")]
    public bool? SupportsProgressReporting { get; set; }

    [JsonPropertyName("supportsInvalidatedEvent")]
    public bool? SupportsInvalidatedEvent { get; set; }

    [JsonPropertyName("supportsMemoryEvent")]
    public bool? SupportsMemoryEvent { get; set; }

    [JsonPropertyName("supportsArgsCanBeInterpretedByShell")]
    public bool? SupportsArgsCanBeInterpretedByShell { get; set; }

    [JsonPropertyName("supportsStartDebuggingRequest")]
    public bool? SupportsStartDebuggingRequest { get; set; }

    [JsonPropertyName("supportsANSIStyling")]
    public bool? SupportsANSIStyling { get; set; }
}

public class InitializeResponse : Response<Capabilities>
{
}
