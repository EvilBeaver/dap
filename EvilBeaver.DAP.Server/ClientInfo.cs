// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using EvilBeaver.DAP.Dto.Requests;

namespace EvilBeaver.DAP.Server;

/// <summary>
/// Immutable record with client parameters obtained in the 'initialize' request.
/// </summary>
public sealed class ClientInfo
{
    /// <summary>
    /// The ID of the client using this adapter.
    /// </summary>
    public string? ClientId { get; private set; }

    /// <summary>
    /// The human-readable name of the client using this adapter.
    /// </summary>
    public string? ClientName { get; private set; }

    /// <summary>
    /// The ID of the debug adapter.
    /// </summary>
    public string AdapterId { get; private set; } = string.Empty;

    /// <summary>
    /// The ISO-639 locale of the client.
    /// </summary>
    public string? Locale { get; private set; }

    /// <summary>
    /// If true, all line numbers are 1-based (default).
    /// </summary>
    public bool LinesStartAt1 { get; private set; } = true;

    /// <summary>
    /// If true, all column numbers are 1-based (default).
    /// </summary>
    public bool ColumnsStartAt1 { get; private set; } = true;

    /// <summary>
    /// Determines in what format paths are exchanged between client and debug adapter.
    /// Default is 'path'.
    /// </summary>
    public string PathFormat { get; private set; } = "path";

    /// <summary>
    /// Client supports the 'variableType' aspect of variables.
    /// </summary>
    public bool SupportsVariableType { get; private set; }

    /// <summary>
    /// Client supports the paging of variables.
    /// </summary>
    public bool SupportsVariablePaging { get; private set; }

    /// <summary>
    /// Client supports the 'runInTerminal' request.
    /// </summary>
    public bool SupportsRunInTerminalRequest { get; private set; }

    /// <summary>
    /// Client supports memory references.
    /// </summary>
    public bool SupportsMemoryReferences { get; private set; }

    /// <summary>
    /// Client supports progress reporting.
    /// </summary>
    public bool SupportsProgressReporting { get; private set; }

    /// <summary>
    /// Client supports the 'invalidated' event.
    /// </summary>
    public bool SupportsInvalidatedEvent { get; private set; }

    /// <summary>
    /// Client supports the 'memory' event.
    /// </summary>
    public bool SupportsMemoryEvent { get; private set; }

    /// <summary>
    /// Client supports the 'argsCanBeInterpretedByShell' property on the 'runInTerminal' request.
    /// </summary>
    public bool SupportsArgsCanBeInterpretedByShell { get; private set; }

    /// <summary>
    /// Client supports the 'startDebugging' request.
    /// </summary>
    public bool SupportsStartDebuggingRequest { get; private set; }

    /// <summary>
    /// Client supports ANSI styling.
    /// </summary>
    public bool SupportsANSIStyling { get; private set; }

    /// <summary>
    /// Default instance with default values used before the 'initialize' request is received.
    /// </summary>
    public static readonly ClientInfo Default = new();

    /// <summary>
    /// Creates a <see cref="ClientInfo"/> from the 'initialize' request arguments.
    /// </summary>
    /// <param name="args">The arguments from the 'initialize' request.</param>
    /// <returns>A new <see cref="ClientInfo"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="args"/> is null.</exception>
    public static ClientInfo FromArguments(InitializeRequestArguments args)
    {
        if (args == null) throw new ArgumentNullException(nameof(args));

        return new ClientInfo
        {
            ClientId = args.ClientId,
            ClientName = args.ClientName,
            AdapterId = args.AdapterId ?? string.Empty,
            Locale = args.Locale,
            LinesStartAt1 = args.LinesStartAt1 ?? true,
            ColumnsStartAt1 = args.ColumnsStartAt1 ?? true,
            PathFormat = args.PathFormat ?? "path",
            SupportsVariableType = args.SupportsVariableType ?? false,
            SupportsVariablePaging = args.SupportsVariablePaging ?? false,
            SupportsRunInTerminalRequest = args.SupportsRunInTerminalRequest ?? false,
            SupportsMemoryReferences = args.SupportsMemoryReferences ?? false,
            SupportsProgressReporting = args.SupportsProgressReporting ?? false,
            SupportsInvalidatedEvent = args.SupportsInvalidatedEvent ?? false,
            SupportsMemoryEvent = args.SupportsMemoryEvent ?? false,
            SupportsArgsCanBeInterpretedByShell = args.SupportsArgsCanBeInterpretedByShell ?? false,
            SupportsStartDebuggingRequest = args.SupportsStartDebuggingRequest ?? false,
            SupportsANSIStyling = args.SupportsANSIStyling ?? false
        };
    }
}
