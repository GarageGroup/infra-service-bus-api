using System;

namespace GarageGroup.Infra;

public sealed record class BusMessageSendIn<TMessageJson>
{
    public BusMessageSendIn(TMessageJson message)
        =>
        Message = message;

    public TMessageJson Message { get; }

    public DateTimeOffset? ScheduledTime { get; init; }
}