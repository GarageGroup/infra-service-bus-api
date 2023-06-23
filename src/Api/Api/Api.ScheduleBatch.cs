using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public ValueTask<BusBatchScheduleOut> ScheduleBatchAsync(
        BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<BusBatchScheduleOut>(cancellationToken);
        }

        if (input.Messages.IsEmpty)
        {
            return default;
        }

        return InnerScheduleBatchAsync(input, cancellationToken);
    }

    private async ValueTask<BusBatchScheduleOut> InnerScheduleBatchAsync(
        BusBatchScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        await using var client = new ServiceBusClient(option.ServiceBusConnectionString);
        var sender = client.CreateSender(option.QueueName);

        var messages = input.Messages.AsEnumerable().Select(CreateServiceBusMessage);
        var sequenceNumbers = await sender.ScheduleMessagesAsync(messages, input.ScheduledTime, cancellationToken).ConfigureAwait(false);

        return new(sequenceNumbers.ToFlatArray());
    }
}