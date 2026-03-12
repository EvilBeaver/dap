// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System;

namespace EvilBeaver.DAP.Server.Transport;

public class StreamTransport(Stream input, Stream output) : ITransport
{
    public Stream Input { get; } = input ?? throw new ArgumentNullException(nameof(input));
    public Stream Output { get; } = output ?? throw new ArgumentNullException(nameof(output));

    public void Dispose()
    {
        // Streams are owned by the host and should be disposed by the host.
    }
}
