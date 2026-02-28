using System.Text.Json.Serialization;

namespace EvilBeaver.DAP.Dto.Types;

/// <summary>
/// Information about the capabilities of a debug adapter.
/// </summary>
public class Capabilities
{
    [JsonPropertyName("supportsConfigurationDoneRequest")]
    public bool? SupportsConfigurationDoneRequest { get; set; }

    [JsonPropertyName("supportsFunctionBreakpoints")]
    public bool? SupportsFunctionBreakpoints { get; set; }

    [JsonPropertyName("supportsConditionalBreakpoints")]
    public bool? SupportsConditionalBreakpoints { get; set; }

    [JsonPropertyName("supportsHitConditionalBreakpoints")]
    public bool? SupportsHitConditionalBreakpoints { get; set; }

    [JsonPropertyName("supportsEvaluateForHovers")]
    public bool? SupportsEvaluateForHovers { get; set; }

    [JsonPropertyName("exceptionBreakpointFilters")]
    public ExceptionBreakpointsFilter[]? ExceptionBreakpointFilters { get; set; }

    [JsonPropertyName("supportsStepBack")]
    public bool? SupportsStepBack { get; set; }

    [JsonPropertyName("supportsSetVariable")]
    public bool? SupportsSetVariable { get; set; }

    [JsonPropertyName("supportsRestartFrame")]
    public bool? SupportsRestartFrame { get; set; }

    [JsonPropertyName("supportsGotoTargetsRequest")]
    public bool? SupportsGotoTargetsRequest { get; set; }

    [JsonPropertyName("supportsStepInTargetsRequest")]
    public bool? SupportsStepInTargetsRequest { get; set; }

    [JsonPropertyName("supportsCompletionsRequest")]
    public bool? SupportsCompletionsRequest { get; set; }

    [JsonPropertyName("completionTriggerCharacters")]
    public string[]? CompletionTriggerCharacters { get; set; }

    [JsonPropertyName("supportsModulesRequest")]
    public bool? SupportsModulesRequest { get; set; }

    [JsonPropertyName("additionalModuleColumns")]
    public ColumnDescriptor[]? AdditionalModuleColumns { get; set; }

    [JsonPropertyName("supportedChecksumAlgorithms")]
    public string[]? SupportedChecksumAlgorithms { get; set; }

    [JsonPropertyName("supportsRestartRequest")]
    public bool? SupportsRestartRequest { get; set; }

    [JsonPropertyName("supportsExceptionOptions")]
    public bool? SupportsExceptionOptions { get; set; }

    [JsonPropertyName("supportsValueFormattingOptions")]
    public bool? SupportsValueFormattingOptions { get; set; }

    [JsonPropertyName("supportsExceptionInfoRequest")]
    public bool? SupportsExceptionInfoRequest { get; set; }

    [JsonPropertyName("supportTerminateDebuggee")]
    public bool? SupportTerminateDebuggee { get; set; }

    [JsonPropertyName("supportSuspendDebuggee")]
    public bool? SupportSuspendDebuggee { get; set; }

    [JsonPropertyName("supportsDelayedStackTraceLoading")]
    public bool? SupportsDelayedStackTraceLoading { get; set; }

    [JsonPropertyName("supportsLoadedSourcesRequest")]
    public bool? SupportsLoadedSourcesRequest { get; set; }

    [JsonPropertyName("supportsLogPoints")]
    public bool? SupportsLogPoints { get; set; }

    [JsonPropertyName("supportsTerminateThreadsRequest")]
    public bool? SupportsTerminateThreadsRequest { get; set; }

    [JsonPropertyName("supportsSetExpression")]
    public bool? SupportsSetExpression { get; set; }

    [JsonPropertyName("supportsTerminateRequest")]
    public bool? SupportsTerminateRequest { get; set; }

    [JsonPropertyName("supportsDataBreakpoints")]
    public bool? SupportsDataBreakpoints { get; set; }

    [JsonPropertyName("supportsReadMemoryRequest")]
    public bool? SupportsReadMemoryRequest { get; set; }

    [JsonPropertyName("supportsWriteMemoryRequest")]
    public bool? SupportsWriteMemoryRequest { get; set; }

    [JsonPropertyName("supportsDisassembleRequest")]
    public bool? SupportsDisassembleRequest { get; set; }

    [JsonPropertyName("supportsCancelRequest")]
    public bool? SupportsCancelRequest { get; set; }

    [JsonPropertyName("supportsBreakpointLocationsRequest")]
    public bool? SupportsBreakpointLocationsRequest { get; set; }

    [JsonPropertyName("supportsClipboardContext")]
    public bool? SupportsClipboardContext { get; set; }

    [JsonPropertyName("supportsSteppingGranularity")]
    public bool? SupportsSteppingGranularity { get; set; }

    [JsonPropertyName("supportsInstructionBreakpoints")]
    public bool? SupportsInstructionBreakpoints { get; set; }

    [JsonPropertyName("supportsExceptionFilterOptions")]
    public bool? SupportsExceptionFilterOptions { get; set; }

    [JsonPropertyName("supportsSingleThreadExecutionRequests")]
    public bool? SupportsSingleThreadExecutionRequests { get; set; }

    [JsonPropertyName("supportsDataBreakpointBytes")]
    public bool? SupportsDataBreakpointBytes { get; set; }

    [JsonPropertyName("breakpointModes")]
    public BreakpointMode[]? BreakpointModes { get; set; }

    [JsonPropertyName("supportsANSIStyling")]
    public bool? SupportsANSIStyling { get; set; }
}
