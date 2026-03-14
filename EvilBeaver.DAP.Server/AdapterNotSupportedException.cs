// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace EvilBeaver.DAP.Server;

/// <summary>
/// Internal exception used to signal that a DAP command is not supported by the adapter.
/// This exception is mapped to a "notSupported" DAP response in the message loop.
/// </summary>
internal sealed class AdapterNotSupportedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AdapterNotSupportedException"/> class.
    /// </summary>
    /// <param name="command">The name of the command that is not supported.</param>
    /// <param name="message">An optional custom error message.</param>
    public AdapterNotSupportedException(string command, string? message = null)
        : base(message ?? $"Command '{command}' is not supported by this adapter.")
    {
        Command = command;
    }

    /// <summary>
    /// Gets the name of the command that is not supported.
    /// </summary>
    public string Command { get; }
}
