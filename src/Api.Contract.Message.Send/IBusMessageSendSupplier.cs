using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusMessageSendSupplier
{
    Task<Unit> SendMessageAsync<TMessageJson>(BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken);
}