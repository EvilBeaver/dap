// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Dto.Types;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace EvilBeaver.DAP.Server.Protocol;

internal sealed class MessageLoop
{
    private const string ErrorNotSupported = "notSupported";
    private const string ErrorInvalidRequest = "invalidRequest";
    private const string ErrorInternalError = "internalError";

    private const int ErrorIdUnsupportedCommand = 1001;
    private const int ErrorIdInvalidRequestPayload = 1002;
    private const int ErrorIdUnhandledAdapterError = 1003;

    private readonly DapReader _reader;
    private readonly DapWriter _writer;
    private readonly IDebugAdapter _adapter;
    private readonly Dictionary<string, Func<Request, CancellationToken, Task<Response>>> _handlers;
    
    private readonly ILogger _logger;

    public MessageLoop(
        DapReader reader,
        DapWriter writer,
        IDebugAdapter adapter,
        ILoggerFactory? loggerFactory = null)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        _adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));

        var activeFactory = loggerFactory ?? new NullLoggerFactory();
        _logger = activeFactory.CreateLogger<MessageLoop>();

        _handlers = new Dictionary<string, Func<Request, CancellationToken, Task<Response>>>(StringComparer.Ordinal)
        {
            ["attach"] = CreateHandler<AttachRequest, AttachResponse>(adapter.AttachAsync),
            ["breakpointLocations"] = CreateHandler<BreakpointLocationsRequest, BreakpointLocationsResponse>(adapter.BreakpointLocationsAsync),
            ["cancel"] = CreateHandler<CancelRequest, CancelResponse>(adapter.CancelAsync),
            ["completions"] = CreateHandler<CompletionsRequest, CompletionsResponse>(adapter.CompletionsAsync),
            ["configurationDone"] = CreateHandler<ConfigurationDoneRequest, ConfigurationDoneResponse>(adapter.ConfigurationDoneAsync),
            ["continue"] = CreateHandler<ContinueRequest, ContinueResponse>(adapter.ContinueAsync),
            ["dataBreakpointInfo"] = CreateHandler<DataBreakpointInfoRequest, DataBreakpointInfoResponse>(adapter.DataBreakpointInfoAsync),
            ["disassemble"] = CreateHandler<DisassembleRequest, DisassembleResponse>(adapter.DisassembleAsync),
            ["evaluate"] = CreateHandler<EvaluateRequest, EvaluateResponse>(adapter.EvaluateAsync),
            ["exceptionInfo"] = CreateHandler<ExceptionInfoRequest, ExceptionInfoResponse>(adapter.ExceptionInfoAsync),
            ["goto"] = CreateHandler<GotoRequest, GotoResponse>(adapter.GotoAsync),
            ["gotoTargets"] = CreateHandler<GotoTargetsRequest, GotoTargetsResponse>(adapter.GotoTargetsAsync),
            ["initialize"] = CreateHandler<InitializeRequest, InitializeResponse>(adapter.InitializeAsync),
            ["launch"] = CreateHandler<LaunchRequest, LaunchResponse>(adapter.LaunchAsync),
            ["loadedSources"] = CreateHandler<LoadedSourcesRequest, LoadedSourcesResponse>(adapter.LoadedSourcesAsync),
            ["locations"] = CreateHandler<LocationsRequest, LocationsResponse>(adapter.LocationsAsync),
            ["modules"] = CreateHandler<ModulesRequest, ModulesResponse>(adapter.ModulesAsync),
            ["next"] = CreateHandler<NextRequest, NextResponse>(adapter.NextAsync),
            ["pause"] = CreateHandler<PauseRequest, PauseResponse>(adapter.PauseAsync),
            ["readMemory"] = CreateHandler<ReadMemoryRequest, ReadMemoryResponse>(adapter.ReadMemoryAsync),
            ["restart"] = CreateHandler<RestartRequest, RestartResponse>(adapter.RestartAsync),
            ["restartFrame"] = CreateHandler<RestartFrameRequest, RestartFrameResponse>(adapter.RestartFrameAsync),
            ["reverseContinue"] = CreateHandler<ReverseContinueRequest, ReverseContinueResponse>(adapter.ReverseContinueAsync),
            ["scopes"] = CreateHandler<ScopesRequest, ScopesResponse>(adapter.ScopesAsync),
            ["setBreakpoints"] = CreateHandler<SetBreakpointsRequest, SetBreakpointsResponse>(adapter.SetBreakpointsAsync),
            ["setDataBreakpoints"] = CreateHandler<SetDataBreakpointsRequest, SetDataBreakpointsResponse>(adapter.SetDataBreakpointsAsync),
            ["setExceptionBreakpoints"] = CreateHandler<SetExceptionBreakpointsRequest, SetExceptionBreakpointsResponse>(adapter.SetExceptionBreakpointsAsync),
            ["setExpression"] = CreateHandler<SetExpressionRequest, SetExpressionResponse>(adapter.SetExpressionAsync),
            ["setFunctionBreakpoints"] = CreateHandler<SetFunctionBreakpointsRequest, SetFunctionBreakpointsResponse>(adapter.SetFunctionBreakpointsAsync),
            ["setInstructionBreakpoints"] = CreateHandler<SetInstructionBreakpointsRequest, SetInstructionBreakpointsResponse>(adapter.SetInstructionBreakpointsAsync),
            ["setVariable"] = CreateHandler<SetVariableRequest, SetVariableResponse>(adapter.SetVariableAsync),
            ["source"] = CreateHandler<SourceRequest, SourceResponse>(adapter.SourceAsync),
            ["stackTrace"] = CreateHandler<StackTraceRequest, StackTraceResponse>(adapter.StackTraceAsync),
            ["stepBack"] = CreateHandler<StepBackRequest, StepBackResponse>(adapter.StepBackAsync),
            ["stepIn"] = CreateHandler<StepInRequest, StepInResponse>(adapter.StepInAsync),
            ["stepInTargets"] = CreateHandler<StepInTargetsRequest, StepInTargetsResponse>(adapter.StepInTargetsAsync),
            ["stepOut"] = CreateHandler<StepOutRequest, StepOutResponse>(adapter.StepOutAsync),
            ["terminate"] = CreateHandler<TerminateRequest, TerminateResponse>(adapter.TerminateAsync),
            ["terminateThreads"] = CreateHandler<TerminateThreadsRequest, TerminateThreadsResponse>(adapter.TerminateThreadsAsync),
            ["threads"] = CreateHandler<ThreadsRequest, ThreadsResponse>(adapter.ThreadsAsync),
            ["variables"] = CreateHandler<VariablesRequest, VariablesResponse>(adapter.VariablesAsync),
            ["writeMemory"] = CreateHandler<WriteMemoryRequest, WriteMemoryResponse>(adapter.WriteMemoryAsync),
        };
    }

    public async Task RunAsync(CancellationToken ct)
    {
        _logger.LogInformation("Starting DAP Server");
        while (!ct.IsCancellationRequested)
        {
            ProtocolMessage? message;
            try
            {
                message = await _reader.ReadMessageAsync(ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                _logger.LogInformation("Message loop cancellation requested. Exiting");
                break;
            }
            catch (EndOfStreamException e)
            {
                _logger.LogError(e, "Input stream EOF. Exiting");
                await CallOnClientDisconnect(ct);
                break;
            }

            if (message is null)
            {
                _logger.LogError("Received null message. Maybe input EOF. Exiting");
                await CallOnClientDisconnect(ct);
                break;
            }

            if (message is not Request request)
            {
                _logger.LogWarning("Unexpected message type {Type}. Expecting Request", message.GetType());
                continue;
            }

            if (request is DisconnectRequest disconnectRequest)
            {
                await HandleDisconnectAsync(disconnectRequest, ct);
                break;
            }

            Response response;
            try
            {
                response = await DispatchAsync(request, ct);
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                break;
            }
            catch (AdapterNotSupportedException ex)
            {
                response = CreateErrorResponse(ErrorNotSupported, ErrorIdUnsupportedCommand, ex.Message);
            }
            catch (ErrorResponseException ex)
            {
                response = ex.ErrorResponse;
            }
            catch (Exception ex)
            {
                response = CreateErrorResponse(ErrorInternalError, ErrorIdUnhandledAdapterError, $"Unhandled adapter error: {ex.Message}");
            }

            response.RequestSeq = request.Seq;
            response.Command = request.Command;
            await _writer.WriteMessageAsync(response, ct);
        }
        _logger.LogInformation("DAP Server stopped");
    }

    private async Task CallOnClientDisconnect(CancellationToken ct)
    {
        try
        {
            _logger.LogDebug("Notifying adapter about disconnect");
            await _adapter.OnClientDisconnectedAsync(ct);
        }
        catch(Exception innerExc)
        {
            _logger.LogError(innerExc, "Unexpected adapter error");
        }
    }

    private async Task HandleDisconnectAsync(DisconnectRequest request, CancellationToken ct)
    {
        Response response;
        _logger.LogDebug("Disconnect request received. Starting exit sequence.");
        try
        {
            var result = await _adapter.DisconnectAsync(request, ct);
            result.Success = true;
            response = result;
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            return;
        }
        catch (AdapterNotSupportedException ex)
        {
            response = CreateErrorResponse(ErrorNotSupported, ErrorIdUnsupportedCommand, ex.Message);
        }
        catch (Exception ex)
        {
            response = CreateErrorResponse(ErrorInternalError, ErrorIdUnhandledAdapterError, $"Unhandled adapter error: {ex.Message}");
        }

        response.RequestSeq = request.Seq;
        response.Command = request.Command;
        await _writer.WriteMessageAsync(response, ct);

        await CallOnClientDisconnect(ct);
    }

    private async Task<Response> DispatchAsync(Request request, CancellationToken ct)
    {
        if (!_handlers.TryGetValue(request.Command, out var handler))
        {
            _logger.LogWarning("No handler registered for {Command}", request.Command);
            return CreateErrorResponse(ErrorNotSupported, ErrorIdUnsupportedCommand, $"Unsupported command: {request.Command}");
        }

        try
        {
            _logger.LogDebug("Dispatching command {Command}", request.Command);
            var response = await handler(request, ct);
            response.Success = true;
            _logger.LogDebug("Done dispatching command {Command}", request.Command);
            return response;
        }
        catch (InvalidCastException e)
        {
            _logger.LogWarning(e, "Payload for command {Command} had invalid type", request.Command);
            return CreateErrorResponse(ErrorInvalidRequest, ErrorIdInvalidRequestPayload, $"Invalid request payload for command: {request.Command}");
        }
    }

    private static Func<Request, CancellationToken, Task<Response>> CreateHandler<TRequest, TResponse>(
        Func<TRequest, CancellationToken, Task<TResponse>> handler)
        where TRequest : Request
        where TResponse : Response
    {
        return async (request, ct) =>
        {
            if (request is not TRequest typedRequest)
            {
                throw new InvalidCastException($"Expected request type {typeof(TRequest).Name}, got {request.GetType().Name}.");
            }

            return await handler(typedRequest, ct);
        };
    }

    private static ErrorResponse CreateErrorResponse(string code, int id, string details)
    {
        return new ErrorResponse
        {
            Success = false,
            Message = code,
            Body = new ErrorResponseBody
            {
                Error = new Message
                {
                    Id = id,
                    Format = details
                }
            }
        };
    }
}
