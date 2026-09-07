using CRM.Domain.Enums;

namespace CRM.Domain.ActivityManagement;

public sealed record ActivityManagementRuleResult(
    string? ActivityId,
    ActivityManagementOperation Operation,
    bool Allowed,
    bool Changed,
    ActivityManagementErrorCode ErrorCode,
    string Message,
    ActivityType Type,
    string? NormalizedSubject,
    DateTimeOffset ScheduledAtUtc,
    string? NormalizedLeadId,
    string? NormalizedContactId,
    ActivityStatus ResultStatus,
    DateTimeOffset? CompletedAtUtc = null)
{
    public bool Success => Allowed && ErrorCode == ActivityManagementErrorCode.None;

    public static ActivityManagementRuleResult Rejected(
        ActivityManagementCommand command,
        ActivityManagementErrorCode errorCode,
        string message,
        string? normalizedSubject = null,
        string? normalizedLeadId = null,
        string? normalizedContactId = null,
        ActivityStatus resultStatus = ActivityStatus.Scheduled,
        DateTimeOffset? completedAtUtc = null) =>
        new(
            command.ActivityId?.Trim(),
            command.Operation,
            Allowed: false,
            Changed: false,
            errorCode,
            message,
            command.Type,
            normalizedSubject,
            command.ScheduledAtUtc,
            normalizedLeadId,
            normalizedContactId,
            resultStatus,
            completedAtUtc);
}
