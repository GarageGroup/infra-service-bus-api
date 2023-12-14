using System;
using System.Text.Json;
using System.Threading;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

internal sealed partial class ImplBusMessageApi<TMessageJson> : IBusMessageApi<TMessageJson>
{
    private static readonly JsonSerializerOptions SerializerOptions;

    static ImplBusMessageApi()
        =>
        SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ServiceBusClient serviceBusClient;

    private readonly ServiceBusSender serviceBusSender;

    private readonly Lazy<ServiceBusReceiver> lazyServiceBusReceiver;

    internal ImplBusMessageApi(ServiceBusApiOption option)
    {
        serviceBusClient = new(option.ServiceBusConnectionString);
        serviceBusSender = serviceBusClient.CreateSender(option.QueueName);
        lazyServiceBusReceiver = new(CreateReceiver, LazyThreadSafetyMode.ExecutionAndPublication);

        ServiceBusReceiver CreateReceiver()
            =>
            serviceBusClient.CreateReceiver(option.QueueName);
    }

    private static ServiceBusMessage CreateServiceBusMessage(TMessageJson message)
    {
        var json = JsonSerializer.Serialize(message, SerializerOptions);

        return new(json)
        {
            MessageId = Guid.NewGuid().ToString()
        };
    }

    private static ServiceBusMessage CreateServiceBusMessage(TMessageJson message, DateTimeOffset? scheduledTime)
    {
        var busMessage = CreateServiceBusMessage(message);

        if (scheduledTime is not null)
        {
            busMessage.ScheduledEnqueueTime = scheduledTime.Value;
        }

        return busMessage;
    }
}