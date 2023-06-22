using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusBatchScheduleSupplier
{
    ValueTask<BusBatchScheduleOut> ScheduleBatchAsync<TMessageJson>(BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken);
}