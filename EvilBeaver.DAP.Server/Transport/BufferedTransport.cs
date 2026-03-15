/*----------------------------------------------------------
This Source Code Form is subject to the terms of the
Mozilla Public License, v.2.0. If a copy of the MPL
was not distributed with this file, You can obtain one
at http://mozilla.org/MPL/2.0/.
----------------------------------------------------------*/

namespace EvilBeaver.DAP.Server.Transport;

/// <summary>
/// Transport which wraps IO streams into buffered stream for read/write performance.
/// Transport owns original streams and disposes them on Dispose call.
/// </summary>
public class BufferedTransport : ITransport
{
    public BufferedTransport(Stream input, Stream output)
    {
        Input = new BufferedStream(input);
        Output = new BufferedStream(output);
    }

    public void Dispose()
    {
        Input.Dispose();
        Output.Dispose();
    }

    public Stream Input { get; }
    public Stream Output { get; }
}