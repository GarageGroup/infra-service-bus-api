using System;
using PrimeFuncPack;

namespace GarageGroup.Infra;

public static class ServiceBusMessageApiDependency
{
    public static Dependency<IBusMessageApi> UseBusMessageApi(this Dependency<ServiceBusApiOption> dependency)
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IBusMessageApi>(CreateApi);

        static ImplServiceBusApi CreateApi(ServiceBusApiOption option)
        {
            ArgumentNullException.ThrowIfNull(option);
            return new(option);
        }
    }
}