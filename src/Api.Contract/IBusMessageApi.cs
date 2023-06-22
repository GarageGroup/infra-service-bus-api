namespace GarageGroup.Infra;

public interface IBusMessageApi : IBusMessageSendSupplier, IBusMessageScheduleSupplier, IBusMessageCancelSupplier
{
}