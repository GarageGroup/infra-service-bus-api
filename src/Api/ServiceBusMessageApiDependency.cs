using System;
using PrimeFuncPack;

namespace GarageGroup.Infra;

public static class ServiceBusMessageApiDependency
{
    public static Dependency<IBusMessageApi<TMessageJson>> UseBusMessageApi<TMessageJson>(this Dependency<ServiceBusApiOption> dependency)
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IBusMessageApi<TMessageJson>>(CreateApi);

        static ImplBusMessageApi<TMessageJson> CreateApi(ServiceBusApiOption option)
        {
            ArgumentNullException.ThrowIfNull(option);
            return new(option);
        }
    }
}