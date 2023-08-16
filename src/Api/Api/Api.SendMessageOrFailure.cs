using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public Task<Result<Unit, Failure<Unit>>> SendMessageOrFailureAsync(
        BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<Result<Unit, Failure<Unit>>>(cancellationToken);
        }

        return InnerSendMessageOrFailureAsync(input, cancellationToken);
    }

    private async Task<Result<Unit, Failure<Unit>>> InnerSendMessageOrFailureAsync(
        BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        try
        {
            return await InnerSendMessageAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return exception.ToFailure("An unexpected exception was thrown when sending a message to the service bus");
        }
    }
}