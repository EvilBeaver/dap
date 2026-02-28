using EvilBeaver.DAP.Dto.Base;
using EvilBeaver.DAP.Server.Protocol;
using EvilBeaver.DAP.Server.Transport;

namespace EvilBeaver.DAP.Server;

public class DapServer : IClientChannel
{
    private readonly ITransport _transport;
    
    public DapReader Reader { get; }
    public DapWriter Writer { get; }

    public DapServer(ITransport transport)
    {
        _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        Reader = new DapReader(_transport.Input);
        Writer = new DapWriter(_transport.Output);
    }

    public Task SendEventAsync(Event @event, CancellationToken ct = default)
    {
        return Writer.WriteMessageAsync(@event, ct);
    }
}
