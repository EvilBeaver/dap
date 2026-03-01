// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace EvilBeaver.DAP.Server.Transport;

public sealed class StdioTransport : ITransport
{
    public Stream Input { get; } = Console.OpenStandardInput();
    public Stream Output { get; } = Console.OpenStandardOutput();

    public ValueTask DisposeAsync()
    {
        Input.Dispose();
        Output.Dispose();
#if NET8_0_OR_GREATER
        return ValueTask.CompletedTask;
#else
        return new ValueTask(Task.CompletedTask);
#endif
    }
}
