using System;
using Microsoft.Extensions.Configuration;
using PrimeFuncPack;

namespace GarageGroup.Infra;

public static class ServiceBusMessageApi
{
    public static Dependency<IBusMessageApi<TMessageJson>> Configure<TMessageJson>(string sectionName)
    {
        return Dependency.From<IBusMessageApi<TMessageJson>>(ResolveApi);

        ImplBusMessageApi<TMessageJson> ResolveApi(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            var section = serviceProvider.GetServiceOrThrow<IConfiguration>().GetRequiredSection(sectionName ?? string.Empty);
            return new(section.GetApiOption("ServiceBusConnectionString", "QueueName"));
        }
    }

    public static Dependency<IBusMessageApi<TMessageJson>> Configure<TMessageJson>(
        string serviceBusConnectionStringConfigurationKey,
        string queueNameConfigurationKey)
    {
        return Dependency.From<IBusMessageApi<TMessageJson>>(ResolveApi);

        ImplBusMessageApi<TMessageJson> ResolveApi(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            var configuration = serviceProvider.GetServiceOrThrow<IConfiguration>();
            return new(configuration.GetApiOption(serviceBusConnectionStringConfigurationKey, queueNameConfigurationKey));
        }
    }

    private static ServiceBusApiOption GetApiOption(
        this IConfiguration configuration,
        string serviceBusConnectionStringConfigurationKey,
        string queueNameConfigurationKey)
        =>
        new(
            serviceBusConnectionString: configuration[serviceBusConnectionStringConfigurationKey] ?? string.Empty,
            queueName: configuration[queueNameConfigurationKey] ?? string.Empty);
}