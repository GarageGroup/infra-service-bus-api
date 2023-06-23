namespace GarageGroup.Infra;

public readonly record struct BusMessageCancelIn
{
    public BusMessageCancelIn(long sequenceNumber)
        =>
        SequenceNumber = sequenceNumber;

    public long SequenceNumber { get; }
}