using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

partial class ImplServiceBusApi
{
    public Task<BusMessageScheduleOut> ScheduleMessageAsync<TMessageJson>(
        BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(input);

        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<BusMessageScheduleOut>(cancellationToken);
        }

        return InnerScheduleMessageAsync(input, cancellationToken);
    }

    private async Task<BusMessageScheduleOut> InnerScheduleMessageAsync<TMessageJson>(
        BusMessageScheduleIn<TMessageJson> input, CancellationToken cancellationToken)
    {
        await using var client = new ServiceBusClient(option.ServiceBusConnectionString);
        var sender = client.CreateSender(option.QueueName);

        var busMessage = CreateServiceBusMessage(input.Message);
        var sequenceNumber = await sender.ScheduleMessageAsync(busMessage, input.ScheduledTime, cancellationToken).ConfigureAwait(false);

        return new(sequenceNumber);
    }
}