using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusBatchSendSupplier<TMessageJson>
{
    ValueTask<Unit> SendBatchAsync(BusBatchSendIn<TMessageJson> input, CancellationToken cancellationToken);
}