using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusBatchScheduleSupplier<TMessageJson>
{
    ValueTask<BusBatchScheduleOut> ScheduleBatchAsync(
        BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken);

    ValueTask<Result<BusBatchScheduleOut, Failure<Unit>>> ScheduleBatchOrFailureAsync(
        BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken);
}