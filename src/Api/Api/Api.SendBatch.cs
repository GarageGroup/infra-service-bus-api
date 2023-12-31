using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public ValueTask<Unit> SendBatchAsync(BusBatchSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Unit>(cancellationToken);
        }

        if (input.Messages.IsEmpty)
        {
            return default;
        }

        return InnerSendBatchAsync(input, cancellationToken);
    }

    private async ValueTask<Unit> InnerSendBatchAsync(BusBatchSendIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        var messageBatch = await serviceBusSender.CreateMessageBatchAsync(cancellationToken).ConfigureAwait(false);
        foreach (var busMessage in input.Messages.AsEnumerable().Select(InnerCreateServiceBusMessage))
        {
            if (messageBatch.TryAddMessage(busMessage))
            {
                continue;
            }

            await serviceBusSender.SendMessagesAsync(messageBatch, cancellationToken).ConfigureAwait(false);
            messageBatch = await serviceBusSender.CreateMessageBatchAsync(cancellationToken).ConfigureAwait(false);

            if (messageBatch.TryAddMessage(busMessage) is false)
            {
                throw new InvalidOperationException($"Message {busMessage.Body} is too large and cannot be sent.");
            }
        }

        if (messageBatch.Count > 0)
        {
            await serviceBusSender.SendMessagesAsync(messageBatch, cancellationToken).ConfigureAwait(false);
        }

        return default;

        ServiceBusMessage InnerCreateServiceBusMessage(TMessageJson message)
            =>
            CreateServiceBusMessage(message, input.ScheduledTime);
    }
}