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
