// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
        return ValueTask.CompletedTask;
    }
}
