using System.Threading.Tasks;

namespace GarageGroup.Infra;

partial class ImplBusMessageApi<TMessageJson>
{
    public async ValueTask DisposeAsync()
    {
        if (lazyServiceBusReceiver.IsValueCreated)
        {
            await lazyServiceBusReceiver.Value.DisposeAsync().ConfigureAwait(false);
        }

        await serviceBusSender.DisposeAsync().ConfigureAwait(false);
        await serviceBusClient.DisposeAsync().ConfigureAwait(false);
    }
}
