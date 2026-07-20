using CRM.Domain.Common;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public sealed class Activity
{
    private Activity(CrmId id, ActivityType type, string subject, DateTimeOffset scheduledAtUtc)
    {
        Id = id;
        Type = type;
        Subject = subject;
        ScheduledAtUtc = scheduledAtUtc;
        Status = ActivityStatus.Scheduled;
    }

    public CrmId Id { get; }
    public ActivityType Type { get; }
    public string Subject { get; }
    public DateTimeOffset ScheduledAtUtc { get; }
    public DateTimeOffset? CompletedAtUtc { get; private set; }
    public ActivityStatus Status { get; private set; }

    public static Activity Schedule(CrmId id, ActivityType type, string subject, DateTimeOffset scheduledAtUtc)
    {
        var normalizedSubject = (subject ?? string.Empty).Trim();
        return normalizedSubject.Length == 0
            ? throw new ArgumentException("Activity subject is required.", nameof(subject))
            : new Activity(id, type, normalizedSubject, scheduledAtUtc);
    }

    public void Complete(DateTimeOffset completedAtUtc)
    {
        if (Status != ActivityStatus.Scheduled)
        {
            throw new InvalidOperationException("Only scheduled activities can be completed.");
        }

        Status = ActivityStatus.Completed;
        CompletedAtUtc = completedAtUtc;
    }
}
