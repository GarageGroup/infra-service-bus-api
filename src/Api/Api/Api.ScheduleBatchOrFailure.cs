using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public ValueTask<Result<BusBatchScheduleOut, Failure<Unit>>> ScheduleBatchOrFailureAsync(
        BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Result<BusBatchScheduleOut, Failure<Unit>>>(cancellationToken);
        }

        if (input.Messages.IsEmpty)
        {
            return default;
        }

        return InnerScheduleBatchOrFailureAsync(input, cancellationToken);
    }

    private async ValueTask<Result<BusBatchScheduleOut, Failure<Unit>>> InnerScheduleBatchOrFailureAsync(
        BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        try
        {
            return await InnerScheduleBatchAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception.ToFailure("An unexpected exception was thrown when scheduling a batch to the service bus");
        }
    }
}