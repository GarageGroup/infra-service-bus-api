using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusBatchSendSupplier
{
    ValueTask<Unit> SendBatchAsync<TMessageJson>(BusBatchSendIn<TMessageJson> input, CancellationToken cancellationToken);
}