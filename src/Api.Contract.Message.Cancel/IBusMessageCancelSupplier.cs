using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IBusMessageCancelSupplier
{
    Task<Unit> CancelScheduledMessageAsync(BusMessageCancelIn input, CancellationToken cancellationToken);
}