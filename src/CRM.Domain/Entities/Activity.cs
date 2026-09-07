using CRM.Domain.Common;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public sealed class Activity
{
    private Activity(CrmId id, ActivityType type, string subject, DateTimeOffset scheduledAtUtc, CrmId? leadId = null, CrmId? contactId = null)
    {
        Id = id;
        Type = type;
        Subject = subject;
        ScheduledAtUtc = scheduledAtUtc;
        LeadId = leadId;
        ContactId = contactId;
        Status = ActivityStatus.Scheduled;
    }

    public CrmId Id { get; }
    public ActivityType Type { get; private set; }
    public string Subject { get; private set; }
    public DateTimeOffset ScheduledAtUtc { get; private set; }
    public CrmId? LeadId { get; private set; }
    public CrmId? ContactId { get; private set; }
    public DateTimeOffset? CompletedAtUtc { get; private set; }
    public ActivityStatus Status { get; private set; }

    public static Activity Schedule(CrmId id, ActivityType type, string subject, DateTimeOffset scheduledAtUtc)
    {
        var normalizedSubject = (subject ?? string.Empty).Trim();
        return normalizedSubject.Length == 0
            ? throw new ArgumentException("Activity subject is required.", nameof(subject))
            : new Activity(id, type, normalizedSubject, scheduledAtUtc);
    }

    public static Activity ScheduleForLead(CrmId id, CrmId leadId, ActivityType type, string subject, DateTimeOffset scheduledAtUtc)
    {
        var normalizedSubject = (subject ?? string.Empty).Trim();
        return normalizedSubject.Length == 0
            ? throw new ArgumentException("Activity subject is required.", nameof(subject))
            : new Activity(id, type, normalizedSubject, scheduledAtUtc, leadId: leadId);
    }

    public static Activity ScheduleForContact(CrmId id, CrmId contactId, ActivityType type, string subject, DateTimeOffset scheduledAtUtc)
    {
        var normalizedSubject = (subject ?? string.Empty).Trim();
        return normalizedSubject.Length == 0
            ? throw new ArgumentException("Activity subject is required.", nameof(subject))
            : new Activity(id, type, normalizedSubject, scheduledAtUtc, contactId: contactId);
    }

    public static Activity Restore(CrmId id, ActivityType type, string subject, DateTimeOffset scheduledAtUtc, CrmId? leadId, CrmId? contactId, ActivityStatus status, DateTimeOffset? completedAtUtc = null)
    {
        var activity = new Activity(id, type, subject, scheduledAtUtc, leadId, contactId)
        {
            Status = status,
            CompletedAtUtc = completedAtUtc
        };

        return activity;
    }

    public void UpdateSchedule(ActivityType type, string subject, DateTimeOffset scheduledAtUtc, CrmId? leadId, CrmId? contactId)
    {
        if (Status != ActivityStatus.Scheduled)
        {
            throw new InvalidOperationException("Only scheduled activities can be modified.");
        }

        Type = type;
        Subject = subject;
        ScheduledAtUtc = scheduledAtUtc;
        LeadId = leadId;
        ContactId = contactId;
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

    public void Cancel()
    {
        if (Status == ActivityStatus.Completed)
        {
            throw new InvalidOperationException("Completed activities cannot be cancelled.");
        }

        Status = ActivityStatus.Cancelled;
    }
}
