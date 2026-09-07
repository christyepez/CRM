using CRM.Domain.Enums;

namespace CRM.Domain.ActivityManagement;

public sealed record ActivityManagementCommand(
    ActivityManagementOperation Operation,
    string? ActivityId,
    ActivityType Type,
    string? Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId,
    ActivityManagementSnapshot? ExistingActivity = null);

public sealed record ActivityManagementSnapshot(
    string ActivityId,
    ActivityType Type,
    string Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId,
    ActivityStatus Status,
    DateTimeOffset? CompletedAtUtc = null);
