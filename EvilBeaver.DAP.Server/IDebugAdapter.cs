// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Requests;

namespace EvilBeaver.DAP.Server;

public interface IDebugAdapter
{
    Task ConnectAsync(IClientChannel channel, CancellationToken ct);

    Task<AttachResponse> AttachAsync(AttachRequest request, CancellationToken ct);
    Task<BreakpointLocationsResponse> BreakpointLocationsAsync(BreakpointLocationsRequest request, CancellationToken ct);
    Task<CancelResponse> CancelAsync(CancelRequest request, CancellationToken ct);
    Task<CompletionsResponse> CompletionsAsync(CompletionsRequest request, CancellationToken ct);
    Task<ConfigurationDoneResponse> ConfigurationDoneAsync(ConfigurationDoneRequest request, CancellationToken ct);
    Task<ContinueResponse> ContinueAsync(ContinueRequest request, CancellationToken ct);
    Task<DataBreakpointInfoResponse> DataBreakpointInfoAsync(DataBreakpointInfoRequest request, CancellationToken ct);
    Task<DisassembleResponse> DisassembleAsync(DisassembleRequest request, CancellationToken ct);
    Task<DisconnectResponse> DisconnectAsync(DisconnectRequest request, CancellationToken ct);
    Task<EvaluateResponse> EvaluateAsync(EvaluateRequest request, CancellationToken ct);
    Task<ExceptionInfoResponse> ExceptionInfoAsync(ExceptionInfoRequest request, CancellationToken ct);
    Task<GotoResponse> GotoAsync(GotoRequest request, CancellationToken ct);
    Task<GotoTargetsResponse> GotoTargetsAsync(GotoTargetsRequest request, CancellationToken ct);
    Task<InitializeResponse> InitializeAsync(InitializeRequest request, CancellationToken ct);
    Task<LaunchResponse> LaunchAsync(LaunchRequest request, CancellationToken ct);
    Task<LoadedSourcesResponse> LoadedSourcesAsync(LoadedSourcesRequest request, CancellationToken ct);
    Task<LocationsResponse> LocationsAsync(LocationsRequest request, CancellationToken ct);
    Task<ModulesResponse> ModulesAsync(ModulesRequest request, CancellationToken ct);
    Task<NextResponse> NextAsync(NextRequest request, CancellationToken ct);
    Task<PauseResponse> PauseAsync(PauseRequest request, CancellationToken ct);
    Task<ReadMemoryResponse> ReadMemoryAsync(ReadMemoryRequest request, CancellationToken ct);
    Task<RestartResponse> RestartAsync(RestartRequest request, CancellationToken ct);
    Task<RestartFrameResponse> RestartFrameAsync(RestartFrameRequest request, CancellationToken ct);
    Task<ReverseContinueResponse> ReverseContinueAsync(ReverseContinueRequest request, CancellationToken ct);
    Task<ScopesResponse> ScopesAsync(ScopesRequest request, CancellationToken ct);
    Task<SetBreakpointsResponse> SetBreakpointsAsync(SetBreakpointsRequest request, CancellationToken ct);
    Task<SetDataBreakpointsResponse> SetDataBreakpointsAsync(SetDataBreakpointsRequest request, CancellationToken ct);
    Task<SetExceptionBreakpointsResponse> SetExceptionBreakpointsAsync(SetExceptionBreakpointsRequest request, CancellationToken ct);
    Task<SetExpressionResponse> SetExpressionAsync(SetExpressionRequest request, CancellationToken ct);
    Task<SetFunctionBreakpointsResponse> SetFunctionBreakpointsAsync(SetFunctionBreakpointsRequest request, CancellationToken ct);
    Task<SetInstructionBreakpointsResponse> SetInstructionBreakpointsAsync(SetInstructionBreakpointsRequest request, CancellationToken ct);
    Task<SetVariableResponse> SetVariableAsync(SetVariableRequest request, CancellationToken ct);
    Task<SourceResponse> SourceAsync(SourceRequest request, CancellationToken ct);
    Task<StackTraceResponse> StackTraceAsync(StackTraceRequest request, CancellationToken ct);
    Task<StepBackResponse> StepBackAsync(StepBackRequest request, CancellationToken ct);
    Task<StepInResponse> StepInAsync(StepInRequest request, CancellationToken ct);
    Task<StepInTargetsResponse> StepInTargetsAsync(StepInTargetsRequest request, CancellationToken ct);
    Task<StepOutResponse> StepOutAsync(StepOutRequest request, CancellationToken ct);
    Task<TerminateResponse> TerminateAsync(TerminateRequest request, CancellationToken ct);
    Task<TerminateThreadsResponse> TerminateThreadsAsync(TerminateThreadsRequest request, CancellationToken ct);
    Task<ThreadsResponse> ThreadsAsync(ThreadsRequest request, CancellationToken ct);
    Task<VariablesResponse> VariablesAsync(VariablesRequest request, CancellationToken ct);
    Task<WriteMemoryResponse> WriteMemoryAsync(WriteMemoryRequest request, CancellationToken ct);
}
