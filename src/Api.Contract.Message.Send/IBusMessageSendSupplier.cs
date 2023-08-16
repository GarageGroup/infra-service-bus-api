using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusMessageSendSupplier<TMessageJson>
{
    Task<Unit> SendMessageAsync(BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken);

    Task<Result<Unit, Failure<Unit>>> SendMessageOrFailureAsync(BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken);
}