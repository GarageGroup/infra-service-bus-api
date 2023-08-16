using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public Task<Result<Unit, Failure<Unit>>> CancelScheduledMessageOrFailureAsync(
        BusMessageCancelIn input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<Result<Unit, Failure<Unit>>>(cancellationToken);
        }

        return InnerCancelScheduledMessageOrFailureAsync(input, cancellationToken);
    }

    private async Task<Result<Unit, Failure<Unit>>> InnerCancelScheduledMessageOrFailureAsync(
        BusMessageCancelIn input, CancellationToken cancellationToken)
    {
        try
        {
            return await InnerCancelScheduledMessageAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception.ToFailure("An unexpected exception was thrown when canceling a scheduled message to the service bus");
        }
    }
}