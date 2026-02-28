using EvilBeaver.DAP.Dto.Base;

namespace EvilBeaver.DAP.Server;

public interface IClientChannel
{
    Task SendEventAsync(Event @event, CancellationToken ct = default);
}
