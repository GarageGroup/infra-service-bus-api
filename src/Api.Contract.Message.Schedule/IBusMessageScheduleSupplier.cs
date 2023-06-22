using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusMessageScheduleSupplier
{
    Task<BusMessageScheduleOut> ScheduleMessageAsync<TMessageJson>(BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken);
}