using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public Task<Unit> CancelScheduledMessageAsync(BusMessageCancelIn input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<Unit>(cancellationToken);
        }

        return InnerCancelScheduledMessageAsync(input, cancellationToken);
    }

    private async Task<Unit> InnerCancelScheduledMessageAsync(BusMessageCancelIn input, CancellationToken cancellationToken)
    {
        await using var client = new ServiceBusClient(option.ServiceBusConnectionString);
        var sender = client.CreateSender(option.QueueName);

        await sender.CancelScheduledMessageAsync(input.SequenceNumber, cancellationToken).ConfigureAwait(false);
        return default;
    }
}