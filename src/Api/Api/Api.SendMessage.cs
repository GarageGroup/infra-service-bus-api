using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

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
        await using var client = new ServiceBusClient(option.ServiceBusConnectionString);
        var sender = client.CreateSender(option.QueueName);

        var serviceBusMessage = CreateServiceBusMessage(input.Message, input.ScheduledTime);
        await sender.SendMessageAsync(serviceBusMessage, cancellationToken).ConfigureAwait(false);

        return default;
    }
}