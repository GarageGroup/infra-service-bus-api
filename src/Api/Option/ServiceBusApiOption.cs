using System;

namespace GarageGroup.Infra;

public sealed record class ServiceBusApiOption
{
    public ServiceBusApiOption(string serviceBusConnectionString, string queueName)
    {
        ServiceBusConnectionString = serviceBusConnectionString ?? string.Empty;
        QueueName = queueName ?? string.Empty;
    }

    public string ServiceBusConnectionString { get; }

    public string QueueName { get; }
}