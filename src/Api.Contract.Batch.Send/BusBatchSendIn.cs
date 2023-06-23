using System;

namespace GarageGroup.Infra;

public readonly record struct BusBatchSendIn<TMessageJson>
{
    public BusBatchSendIn(FlatArray<TMessageJson> messages, DateTimeOffset? scheduledTime = null)
    {
        Messages = messages;
        ScheduledTime = scheduledTime;
    }

    public FlatArray<TMessageJson> Messages { get; }

    public DateTimeOffset? ScheduledTime { get; init; }
}