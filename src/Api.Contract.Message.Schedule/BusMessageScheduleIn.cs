using System;

namespace GarageGroup.Infra;

public sealed record class BusMessageScheduleIn<TMessageJson>
{
    public BusMessageScheduleIn(TMessageJson message, DateTimeOffset scheduledTime)
    {
        Message = message;
        ScheduledTime = scheduledTime;
    }

    public TMessageJson Message { get; }

    public DateTimeOffset ScheduledTime { get; }
}