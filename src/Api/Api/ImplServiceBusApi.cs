using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

internal sealed partial class ImplServiceBusApi : IServiceBusApi
{
    private static readonly JsonSerializerOptions SerializerOptions;

    static ImplServiceBusApi()
        =>
        SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ServiceBusApiOption option;

    internal ImplServiceBusApi(ServiceBusApiOption option)
        =>
        this.option = option;

    private static ServiceBusMessage CreateServiceBusMessage<TMessageJson>(TMessageJson message)
    {
        var json = JsonSerializer.Serialize(message, SerializerOptions);

        return new(json)
        {
            MessageId = Guid.NewGuid().ToString()
        };
    }

    private static ServiceBusMessage CreateServiceBusMessage<TMessageJson>(TMessageJson message, DateTimeOffset? scheduledTime)
    {
        var busMessage = CreateServiceBusMessage(message);

        if (scheduledTime is not null)
        {
            busMessage.ScheduledEnqueueTime = scheduledTime.Value;
        }

        return busMessage;
    }
}