using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public ValueTask<Result<Unit, Failure<Unit>>> SendBatchOrFailureAsync(
        BusBatchSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Result<Unit, Failure<Unit>>>(cancellationToken);
        }

        if (input.Messages.IsEmpty)
        {
            return default;
        }

        return InnerSendBatchOrFailureAsync(input, cancellationToken);
    }

    private async ValueTask<Result<Unit, Failure<Unit>>> InnerSendBatchOrFailureAsync(
        BusBatchSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        try
        {
            return await InnerSendBatchAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            return exception.ToFailure("An unexpected exception was thrown when sending a batch to the service bus");
        }
    }
}