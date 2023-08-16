using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusMessageScheduleSupplier<TMessageJson>
{
    Task<BusMessageScheduleOut> ScheduleMessageAsync(
        BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken);

    Task<Result<BusMessageScheduleOut, Failure<Unit>>> ScheduleMessageOrFailureAsync(
        BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken);
}