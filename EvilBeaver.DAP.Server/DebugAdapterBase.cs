// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Linq;
using EvilBeaver.DAP.Dto.Requests;
using EvilBeaver.DAP.Dto.Types;

namespace EvilBeaver.DAP.Server;

/// <summary>
/// Abstract base class for debug adapters.
/// Implements <see cref="IDebugAdapter"/> and provides common functionality.
/// </summary>
public abstract class DebugAdapterBase : IDebugAdapter
{
    /// <summary>
    /// Channel for sending events to the client (IDE).
    /// Available after <see cref="OnServerStartAsync"/> is called.
    /// </summary>
    protected IClientChannel EventsChannel { get; private set; } = null!;

    /// <summary>
    /// Client parameters obtained in the 'initialize' request.
    /// Contains default values (<see cref="ClientInfo.Default"/>) until the request is received.
    /// </summary>
    protected ClientInfo Client { get; private set; } = ClientInfo.Default;

    /// <summary>
    /// Called before the message loop starts.
    /// Saves the <paramref name="channel"/> for subsequent event sending.
    /// This method is not overridable to ensure infrastructure logic is executed.
    /// </summary>
    public Task OnServerStartAsync(IClientChannel channel, CancellationToken ct)
    {
        EventsChannel = channel ?? throw new ArgumentNullException(nameof(channel));
        return OnServerStartedAsync(ct);
    }

    /// <summary>
    /// Called after <see cref="EventsChannel"/> is saved.
    /// Inheritors can override this method for their startup logic.
    /// </summary>
    protected virtual Task OnServerStartedAsync(CancellationToken ct)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Called by the server infrastructure when the client disconnects.
    /// This method is not overridable.
    /// </summary>
    public Task OnClientDisconnectedAsync(CancellationToken ct)
    {
        return OnClientDisconnectedCoreAsync(ct);
    }

    /// <summary>
    /// Overridable hook called after a normal 'disconnect' or input stream break.
    /// </summary>
    protected virtual Task OnClientDisconnectedCoreAsync(CancellationToken ct)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Converts a path from the client's format to the native operating system path.
    /// If the client is in <c>"path"</c> mode, the path is returned as is.
    /// If the client is in <c>"uri"</c> mode, the URI is parsed and the local path is extracted.
    /// </summary>
    /// <param name="clientPath">The path in the client's format (path or file URI).</param>
    /// <returns>The native operating system path.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="clientPath"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="clientPath"/> is empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="ClientInfo.PathFormat"/> is unsupported.</exception>
    protected string ToNativePath(string clientPath)
    {
        if (clientPath == null) throw new ArgumentNullException(nameof(clientPath));
        if (clientPath.Length == 0) throw new ArgumentException("Path cannot be empty.", nameof(clientPath));

        if (Client.PathFormat == "uri")
        {
            return new Uri(clientPath).LocalPath;
        }
        else if (Client.PathFormat == "path")
        {
            return clientPath;
        }
        else
        {
            throw new InvalidOperationException($"Unsupported PathFormat: {Client.PathFormat}");
        }
    }

    /// <summary>
    /// Converts a native operating system path to the format expected by the client.
    /// If the client is in <c>"path"</c> mode, the path is returned as is.
    /// If the client is in <c>"uri"</c> mode, the path is converted to an absolute file URI.
    /// </summary>
    /// <param name="nativePath">The native operating system path.</param>
    /// <returns>The path in the client's format.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="nativePath"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="nativePath"/> is empty or not absolute.</exception>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="ClientInfo.PathFormat"/> is unsupported.</exception>
    protected string ToClientPath(string nativePath)
    {
        if (nativePath == null) throw new ArgumentNullException(nameof(nativePath));
        if (nativePath.Length == 0) throw new ArgumentException("Path cannot be empty.", nameof(nativePath));
        if (!System.IO.Path.IsPathRooted(nativePath)) throw new ArgumentException("Path must be absolute.", nameof(nativePath));

        if (Client.PathFormat == "uri")
        {
            return new Uri(nativePath).AbsoluteUri;
        }
        else if (Client.PathFormat == "path")
        {
            return nativePath;
        }
        else
        {
            throw new InvalidOperationException($"Unsupported PathFormat: {Client.PathFormat}");
        }
    }

    /// <summary>
    /// Creates a <see cref="Source"/> object with the path converted to the client's format.
    /// This is a helper method for forming responses that contain source code references.
    /// </summary>
    /// <param name="nativePath">The native path to the source code file.</param>
    /// <param name="name">
    /// The display name of the source. If not specified, the filename from the path is used.
    /// </param>
    /// <returns>A <see cref="Source"/> object ready for the adapter's response.</returns>
    protected Source CreateSource(string nativePath, string? name = null)
    {
        return new Source
        {
            Path = ToClientPath(nativePath),
            Name = name ?? System.IO.Path.GetFileName(nativePath)
        };
    }

    /// <summary>
    /// Processes the 'initialize' request.
    /// Saves client parameters and calls <see cref="OnInitializeAsync"/>.
    /// This method is not overridable — use <see cref="OnInitializeAsync"/>.
    /// </summary>
    public Task<InitializeResponse> InitializeAsync(InitializeRequest request, CancellationToken ct)
    {
        if (request.Arguments is not null)
            Client = ClientInfo.FromArguments(request.Arguments);

        return OnInitializeAsync(request, ct);
    }

    /// <summary>
    /// Called from <see cref="InitializeAsync"/> after client parameters are saved.
    /// Override this method to declare the adapter's <see cref="Capabilities"/>.
    /// <para>
    /// Concrete adapter is responsible for sending the <c>initialized</c> event
    /// through <see cref="EventsChannel"/> when it is ready to accept configuration
    /// requests from the client.
    /// </para>
    /// </summary>
    /// <param name="request">The original 'initialize' request.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Response with declared adapter capabilities.</returns>
    protected virtual Task<InitializeResponse> OnInitializeAsync(InitializeRequest request, CancellationToken ct)
    {
        return Task.FromResult(new InitializeResponse
        {
            Success = true,
            Body = new Capabilities()
        });
    }

    /// <inheritdoc />
    public virtual Task<AttachResponse> AttachAsync(AttachRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<BreakpointLocationsResponse> BreakpointLocationsAsync(BreakpointLocationsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<CancelResponse> CancelAsync(CancelRequest request, CancellationToken ct) => Task.FromResult(new CancelResponse { Success = true });

    /// <inheritdoc />
    public virtual Task<CompletionsResponse> CompletionsAsync(CompletionsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ConfigurationDoneResponse> ConfigurationDoneAsync(ConfigurationDoneRequest request, CancellationToken ct) => Task.FromResult(new ConfigurationDoneResponse { Success = true });

    /// <inheritdoc />
    public virtual Task<ContinueResponse> ContinueAsync(ContinueRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<DataBreakpointInfoResponse> DataBreakpointInfoAsync(DataBreakpointInfoRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<DisassembleResponse> DisassembleAsync(DisassembleRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<DisconnectResponse> DisconnectAsync(DisconnectRequest request, CancellationToken ct) => Task.FromResult(new DisconnectResponse { Success = true });

    /// <inheritdoc />
    public virtual Task<EvaluateResponse> EvaluateAsync(EvaluateRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ExceptionInfoResponse> ExceptionInfoAsync(ExceptionInfoRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<GotoResponse> GotoAsync(GotoRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<GotoTargetsResponse> GotoTargetsAsync(GotoTargetsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<LaunchResponse> LaunchAsync(LaunchRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<LoadedSourcesResponse> LoadedSourcesAsync(LoadedSourcesRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<LocationsResponse> LocationsAsync(LocationsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ModulesResponse> ModulesAsync(ModulesRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<NextResponse> NextAsync(NextRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<PauseResponse> PauseAsync(PauseRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ReadMemoryResponse> ReadMemoryAsync(ReadMemoryRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<RestartResponse> RestartAsync(RestartRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<RestartFrameResponse> RestartFrameAsync(RestartFrameRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ReverseContinueResponse> ReverseContinueAsync(ReverseContinueRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ScopesResponse> ScopesAsync(ScopesRequest request, CancellationToken ct) => Task.FromResult(new ScopesResponse
    {
        Success = true,
        Body = new ScopesResponseBody { Scopes = Array.Empty<Scope>() }
    });

    /// <inheritdoc />
    public virtual Task<SetBreakpointsResponse> SetBreakpointsAsync(SetBreakpointsRequest request, CancellationToken ct)
    {
        var bps = request.Arguments?.Breakpoints ?? Array.Empty<SourceBreakpoint>();
        return Task.FromResult(new SetBreakpointsResponse
        {
            Success = true,
            Body = new SetBreakpointsResponseBody
            {
                Breakpoints = bps.Select(_ => new Breakpoint { Verified = false }).ToArray()
            }
        });
    }

    /// <inheritdoc />
    public virtual Task<SetDataBreakpointsResponse> SetDataBreakpointsAsync(SetDataBreakpointsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<SetExceptionBreakpointsResponse> SetExceptionBreakpointsAsync(SetExceptionBreakpointsRequest request, CancellationToken ct) => Task.FromResult(new SetExceptionBreakpointsResponse
    {
        Success = true,
        Body = new SetExceptionBreakpointsResponseBody { Breakpoints = Array.Empty<Breakpoint>() }
    });

    /// <inheritdoc />
    public virtual Task<SetExpressionResponse> SetExpressionAsync(SetExpressionRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<SetFunctionBreakpointsResponse> SetFunctionBreakpointsAsync(SetFunctionBreakpointsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<SetInstructionBreakpointsResponse> SetInstructionBreakpointsAsync(SetInstructionBreakpointsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<SetVariableResponse> SetVariableAsync(SetVariableRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<SourceResponse> SourceAsync(SourceRequest request, CancellationToken ct) => Task.FromResult(new SourceResponse
    {
        Success = true,
        Body = new SourceResponseBody { Content = string.Empty }
    });

    /// <inheritdoc />
    public virtual Task<StackTraceResponse> StackTraceAsync(StackTraceRequest request, CancellationToken ct) => Task.FromResult(new StackTraceResponse
    {
        Success = true,
        Body = new StackTraceResponseBody { StackFrames = Array.Empty<StackFrame>() }
    });

    /// <inheritdoc />
    public virtual Task<StepBackResponse> StepBackAsync(StepBackRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<StepInResponse> StepInAsync(StepInRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<StepInTargetsResponse> StepInTargetsAsync(StepInTargetsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<StepOutResponse> StepOutAsync(StepOutRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<TerminateResponse> TerminateAsync(TerminateRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<TerminateThreadsResponse> TerminateThreadsAsync(TerminateThreadsRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);

    /// <inheritdoc />
    public virtual Task<ThreadsResponse> ThreadsAsync(ThreadsRequest request, CancellationToken ct) => Task.FromResult(new ThreadsResponse
    {
        Success = true,
        Body = new ThreadsResponseBody { Threads = Array.Empty<Dto.Types.Thread>() }
    });

    /// <inheritdoc />
    public virtual Task<VariablesResponse> VariablesAsync(VariablesRequest request, CancellationToken ct) => Task.FromResult(new VariablesResponse
    {
        Success = true,
        Body = new VariablesResponseBody { Variables = Array.Empty<Variable>() }
    });

    /// <inheritdoc />
    public virtual Task<WriteMemoryResponse> WriteMemoryAsync(WriteMemoryRequest request, CancellationToken ct) => throw new AdapterNotSupportedException(request.Command);
}
