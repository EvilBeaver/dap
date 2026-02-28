namespace EvilBeaver.DAP.Server.Transport;

public interface ITransport : IAsyncDisposable
{
    Stream Input { get; }
    Stream Output { get; }
}
