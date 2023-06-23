using System;

namespace GarageGroup.Infra;

public readonly record struct BusBatchCancelIn
{
    public BusBatchCancelIn(FlatArray<long> sequenceNumbers)
        =>
        SequenceNumbers = sequenceNumbers;

    public FlatArray<long> SequenceNumbers { get; }
}