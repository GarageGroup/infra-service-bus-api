using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace GarageGroup.Infra;

internal sealed partial class ImplBusMessageApi<TMessageJson> : IBusMessageApi<TMessageJson>
{
    private static readonly JsonSerializerOptions SerializerOptions;

    static ImplBusMessageApi()
        =>
        SerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ServiceBusApiOption option;

    internal ImplBusMessageApi(ServiceBusApiOption option)
        =>
        this.option = option;

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