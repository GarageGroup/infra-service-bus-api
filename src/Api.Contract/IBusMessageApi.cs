namespace GarageGroup.Infra;

public interface IBusMessageApi<TMessageJson> :
    IBusMessageSendSupplier<TMessageJson>, IBusMessageScheduleSupplier<TMessageJson>, IBusMessageCancelSupplier,
    IBusBatchSendSupplier<TMessageJson>, IBusBatchScheduleSupplier<TMessageJson>, IBusBatchCancelSupplier
{
}