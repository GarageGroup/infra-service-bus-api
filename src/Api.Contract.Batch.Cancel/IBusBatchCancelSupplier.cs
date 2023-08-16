using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusBatchCancelSupplier
{
    ValueTask<Unit> CancelScheduledBatchAsync(BusBatchCancelIn input, CancellationToken cancellationToken);

    ValueTask<Result<Unit, Failure<Unit>>> CancelScheduledBatchOrFailureAsync(BusBatchCancelIn input, CancellationToken cancellationToken);
}