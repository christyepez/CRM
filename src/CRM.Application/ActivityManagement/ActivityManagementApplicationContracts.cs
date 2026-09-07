using CRM.Domain.ActivityManagement;
using CRM.Domain.Enums;

namespace CRM.Application.ActivityManagement;

public sealed record ActivityManagementCreateApplicationRequest(
    ActivityType Type,
    string? Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId);

public sealed record ActivityManagementUpdateApplicationRequest(
    ActivityType Type,
    string? Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId);

public sealed record ActivityManagementApplicationActivity(
    string Id,
    ActivityType Type,
    string Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId,
    ActivityStatus Status,
    DateTimeOffset? CompletedAtUtc,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record ActivityManagementApplicationResult(
    string? ActivityId,
    ActivityManagementOperation Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    ActivityStatus? Status,
    ActivityManagementApplicationActivity? Activity)
{
    public bool Success => Allowed && string.Equals(ErrorCode, ActivityManagementErrorCode.None.ToString(), StringComparison.Ordinal);
}
