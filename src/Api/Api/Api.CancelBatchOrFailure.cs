using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public ValueTask<Result<Unit, Failure<Unit>>> CancelScheduledBatchOrFailureAsync(
        BusBatchCancelIn input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Result<Unit, Failure<Unit>>>(cancellationToken);
        }

        if (input.SequenceNumbers.IsEmpty)
        {
            return default;
        }

        return InnerCancelScheduledBatchOrFailureAsync(input, cancellationToken);
    }

    private async ValueTask<Result<Unit, Failure<Unit>>> InnerCancelScheduledBatchOrFailureAsync(
        BusBatchCancelIn input, CancellationToken cancellationToken)
    {
        try
        {
            return await InnerCancelScheduledBatchAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            return exception.ToFailure("An unexpected exception was thrown when canceling a scheduled batch to the service bus");
        }
    }
}