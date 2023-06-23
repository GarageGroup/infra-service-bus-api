using System;

namespace GarageGroup.Infra;

public readonly record struct BusBatchScheduleIn<TMessageJson>
{
    public BusBatchScheduleIn(FlatArray<TMessageJson> messages, DateTimeOffset scheduledTime)
    {
        Messages = messages;
        ScheduledTime = scheduledTime;
    }

    public FlatArray<TMessageJson> Messages { get; }

    public DateTimeOffset ScheduledTime { get; }
}