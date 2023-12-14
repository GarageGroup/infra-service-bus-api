using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public Task<Unit> SendMessageAsync(BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<Unit>(cancellationToken);
        }

        return InnerSendMessageAsync(input, cancellationToken);
    }

    private async Task<Unit> InnerSendMessageAsync(BusMessageSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        var serviceBusMessage = CreateServiceBusMessage(input.Message, input.ScheduledTime);
        await serviceBusSender.SendMessageAsync(serviceBusMessage, cancellationToken).ConfigureAwait(false);

        return default;
    }
}