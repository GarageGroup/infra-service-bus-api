using System;

namespace GarageGroup.Infra;

public readonly record struct BusBatchScheduleOut
{
    public BusBatchScheduleOut(FlatArray<long> sequenceNumbers)
        =>
        SequenceNumbers = sequenceNumbers;

    public FlatArray<long> SequenceNumbers { get; }
}