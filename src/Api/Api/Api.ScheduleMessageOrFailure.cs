using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public Task<Result<BusMessageScheduleOut, Failure<Unit>>> ScheduleMessageOrFailureAsync(
        BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<Result<BusMessageScheduleOut, Failure<Unit>>>(cancellationToken);
        }

        return InnerScheduleMessageOrFailureAsync(input, cancellationToken);
    }

    private async Task<Result<BusMessageScheduleOut, Failure<Unit>>> InnerScheduleMessageOrFailureAsync(
        BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        try
        {
            return await InnerScheduleMessageAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception.ToFailure("An unexpected exception was thrown when scheduling a message to the service bus");
        }
    }
}