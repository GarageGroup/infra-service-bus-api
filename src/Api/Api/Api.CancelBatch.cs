using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public ValueTask<Unit> CancelScheduledBatchAsync(BusBatchCancelIn input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Unit>(cancellationToken);
        }

        if (input.SequenceNumbers.IsEmpty)
        {
            return default;
        }

        return InnerCancelScheduledBatchAsync(input, cancellationToken);
    }

    private async ValueTask<Unit> InnerCancelScheduledBatchAsync(BusBatchCancelIn input, CancellationToken cancellationToken)
    {
        await using var client = new ServiceBusClient(option.ServiceBusConnectionString);
        var sender = client.CreateSender(option.QueueName);

        await sender.CancelScheduledMessagesAsync(input.SequenceNumbers.AsEnumerable(), cancellationToken).ConfigureAwait(false);
        return default;
    }
}