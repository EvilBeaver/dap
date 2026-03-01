// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;
using System.Threading.Tasks;

namespace EvilBeaver.DAP.Server.Transport;

public class StreamTransport : ITransport
{
    public StreamTransport(Stream input, Stream output)
    {
        Input = input ?? throw new ArgumentNullException(nameof(input));
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public Stream Input { get; }
    public Stream Output { get; }

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
