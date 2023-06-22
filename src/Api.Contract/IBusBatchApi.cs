namespace GarageGroup.Infra;

public interface IBusBatchApi : IBusBatchSendSupplier, IBusBatchScheduleSupplier, IBusBatchCancelSupplier
{
}