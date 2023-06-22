namespace GarageGroup.Infra;

public readonly record struct BusMessageScheduleOut
{
    public BusMessageScheduleOut(long sequenceNumber)
        =>
        SequenceNumber = sequenceNumber;

    public long SequenceNumber { get; }
}