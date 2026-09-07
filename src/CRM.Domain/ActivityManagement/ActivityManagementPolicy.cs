using CRM.Domain.Enums;

namespace CRM.Domain.ActivityManagement;

public static class ActivityManagementPolicy
{
    public const int MaxSubjectLength = 160;

    public static ActivityManagementRuleResult Evaluate(ActivityManagementCommand command)
    {
        var normalizedActivityId = Normalize(command.ActivityId);
        var normalizedSubject = Normalize(command.Subject);
        var normalizedLeadId = Normalize(command.LeadId);
        var normalizedContactId = Normalize(command.ContactId);

        if (!Enum.IsDefined(command.Operation))
        {
            return Reject(command, ActivityManagementErrorCode.InvalidOperation, "Activity operation is invalid.", normalizedSubject, normalizedLeadId, normalizedContactId);
        }

        if (!Enum.IsDefined(command.Type))
        {
            return Reject(command, ActivityManagementErrorCode.InvalidActivityType, "Activity type is invalid.", normalizedSubject, normalizedLeadId, normalizedContactId);
        }

        if (command.Operation != ActivityManagementOperation.Create)
        {
            if (!IsValidId(normalizedActivityId))
            {
                return Reject(command, ActivityManagementErrorCode.InvalidActivityId, "Activity id is required.", normalizedSubject, normalizedLeadId, normalizedContactId);
            }

            if (command.ExistingActivity is null)
            {
                return Reject(command, ActivityManagementErrorCode.ActivityNotFound, "Existing activity snapshot is required.", normalizedSubject, normalizedLeadId, normalizedContactId);
            }

            if (!string.Equals(normalizedActivityId, Normalize(command.ExistingActivity.ActivityId), StringComparison.OrdinalIgnoreCase))
            {
                return Reject(command, ActivityManagementErrorCode.InvalidActivityId, "Activity id cannot change.", normalizedSubject, normalizedLeadId, normalizedContactId, command.ExistingActivity.Status, command.ExistingActivity.CompletedAtUtc);
            }
        }

        return command.Operation switch
        {
            ActivityManagementOperation.Complete => EvaluateCompletion(command, normalizedActivityId, normalizedSubject, normalizedLeadId, normalizedContactId),
            ActivityManagementOperation.Cancel => EvaluateCancellation(command, normalizedActivityId, normalizedSubject, normalizedLeadId, normalizedContactId),
            _ => EvaluateCreateOrUpdate(command, normalizedActivityId, normalizedSubject, normalizedLeadId, normalizedContactId)
        };
    }

    private static ActivityManagementRuleResult EvaluateCreateOrUpdate(ActivityManagementCommand command, string? normalizedActivityId, string? normalizedSubject, string? normalizedLeadId, string? normalizedContactId)
    {
        var targetValidation = ValidateTarget(command, normalizedSubject, normalizedLeadId, normalizedContactId);
        if (targetValidation is not null)
        {
            return targetValidation;
        }

        if (string.IsNullOrWhiteSpace(normalizedSubject))
        {
            return Reject(command, ActivityManagementErrorCode.SubjectRequired, "Activity subject is required.", normalizedSubject, normalizedLeadId, normalizedContactId, command.ExistingActivity?.Status ?? ActivityStatus.Scheduled, command.ExistingActivity?.CompletedAtUtc);
        }

        if (normalizedSubject.Length > MaxSubjectLength)
        {
            return Reject(command, ActivityManagementErrorCode.SubjectTooLong, "Activity subject exceeds the allowed length.", normalizedSubject, normalizedLeadId, normalizedContactId, command.ExistingActivity?.Status ?? ActivityStatus.Scheduled, command.ExistingActivity?.CompletedAtUtc);
        }

        if (command.ScheduledAtUtc == default)
        {
            return Reject(command, ActivityManagementErrorCode.ScheduledAtRequired, "Activity scheduled date is required.", normalizedSubject, normalizedLeadId, normalizedContactId, command.ExistingActivity?.Status ?? ActivityStatus.Scheduled, command.ExistingActivity?.CompletedAtUtc);
        }

        if (command.Operation == ActivityManagementOperation.Update)
        {
            if (command.ExistingActivity!.Status == ActivityStatus.Completed)
            {
                return Reject(command, ActivityManagementErrorCode.CompletedActivityCannotBeModified, "Completed activities cannot be modified.", normalizedSubject, normalizedLeadId, normalizedContactId, ActivityStatus.Completed, command.ExistingActivity.CompletedAtUtc);
            }

            if (command.ExistingActivity.Status == ActivityStatus.Cancelled)
            {
                return Reject(command, ActivityManagementErrorCode.CancelledActivityCannotBeModified, "Cancelled activities cannot be modified.", normalizedSubject, normalizedLeadId, normalizedContactId, ActivityStatus.Cancelled, command.ExistingActivity.CompletedAtUtc);
            }
        }

        var changed = command.Operation == ActivityManagementOperation.Create || HasChanged(command, normalizedSubject, normalizedLeadId, normalizedContactId);

        return new ActivityManagementRuleResult(
            normalizedActivityId,
            command.Operation,
            Allowed: true,
            changed,
            ActivityManagementErrorCode.None,
            changed ? "Activity management operation is valid." : "Activity update has no changes.",
            command.Type,
            normalizedSubject,
            command.ScheduledAtUtc,
            normalizedLeadId,
            normalizedContactId,
            ActivityStatus.Scheduled);
    }

    private static ActivityManagementRuleResult EvaluateCompletion(ActivityManagementCommand command, string? normalizedActivityId, string? normalizedSubject, string? normalizedLeadId, string? normalizedContactId)
    {
        var existing = command.ExistingActivity!;
        if (existing.Status == ActivityStatus.Cancelled)
        {
            return Reject(command, ActivityManagementErrorCode.CancelledActivityCannotBeCompleted, "Cancelled activities cannot be completed.", normalizedSubject, normalizedLeadId, normalizedContactId, ActivityStatus.Cancelled, existing.CompletedAtUtc);
        }

        if (existing.Status == ActivityStatus.Completed)
        {
            return new ActivityManagementRuleResult(
                normalizedActivityId,
                command.Operation,
                Allowed: true,
                Changed: false,
                ActivityManagementErrorCode.None,
                "Activity is already completed.",
                existing.Type,
                Normalize(existing.Subject),
                existing.ScheduledAtUtc,
                Normalize(existing.LeadId),
                Normalize(existing.ContactId),
                ActivityStatus.Completed,
                existing.CompletedAtUtc);
        }

        return new ActivityManagementRuleResult(
            normalizedActivityId,
            command.Operation,
            Allowed: true,
            Changed: true,
            ActivityManagementErrorCode.None,
            "Activity completion is valid.",
            existing.Type,
            Normalize(existing.Subject),
            existing.ScheduledAtUtc,
            Normalize(existing.LeadId),
            Normalize(existing.ContactId),
            ActivityStatus.Completed);
    }

    private static ActivityManagementRuleResult EvaluateCancellation(ActivityManagementCommand command, string? normalizedActivityId, string? normalizedSubject, string? normalizedLeadId, string? normalizedContactId)
    {
        var existing = command.ExistingActivity!;
        if (existing.Status == ActivityStatus.Completed)
        {
            return Reject(command, ActivityManagementErrorCode.CompletedActivityCannotBeCancelled, "Completed activities cannot be cancelled.", normalizedSubject, normalizedLeadId, normalizedContactId, ActivityStatus.Completed, existing.CompletedAtUtc);
        }

        if (existing.Status == ActivityStatus.Cancelled)
        {
            return new ActivityManagementRuleResult(
                normalizedActivityId,
                command.Operation,
                Allowed: true,
                Changed: false,
                ActivityManagementErrorCode.None,
                "Activity is already cancelled.",
                existing.Type,
                Normalize(existing.Subject),
                existing.ScheduledAtUtc,
                Normalize(existing.LeadId),
                Normalize(existing.ContactId),
                ActivityStatus.Cancelled,
                existing.CompletedAtUtc);
        }

        return new ActivityManagementRuleResult(
            normalizedActivityId,
            command.Operation,
            Allowed: true,
            Changed: true,
            ActivityManagementErrorCode.None,
            "Activity cancellation is valid.",
            existing.Type,
            Normalize(existing.Subject),
            existing.ScheduledAtUtc,
            Normalize(existing.LeadId),
            Normalize(existing.ContactId),
            ActivityStatus.Cancelled);
    }

    private static ActivityManagementRuleResult? ValidateTarget(ActivityManagementCommand command, string? normalizedSubject, string? normalizedLeadId, string? normalizedContactId)
    {
        if (normalizedLeadId is null && normalizedContactId is null)
        {
            return Reject(command, ActivityManagementErrorCode.ActivityTargetRequired, "Activity requires exactly one Lead or Contact target.", normalizedSubject, normalizedLeadId, normalizedContactId);
        }

        if (normalizedLeadId is not null && normalizedContactId is not null)
        {
            return Reject(command, ActivityManagementErrorCode.MultipleActivityTargetsNotAllowed, "Activity cannot target both Lead and Contact.", normalizedSubject, normalizedLeadId, normalizedContactId);
        }

        if (normalizedLeadId is not null && !IsValidId(normalizedLeadId))
        {
            return Reject(command, ActivityManagementErrorCode.InvalidLeadId, "Lead id format is invalid.", normalizedSubject, normalizedLeadId, normalizedContactId);
        }

        if (normalizedContactId is not null && !IsValidId(normalizedContactId))
        {
            return Reject(command, ActivityManagementErrorCode.InvalidContactId, "Contact id format is invalid.", normalizedSubject, normalizedLeadId, normalizedContactId);
        }

        return null;
    }

    private static bool HasChanged(ActivityManagementCommand command, string? normalizedSubject, string? normalizedLeadId, string? normalizedContactId)
    {
        var existing = command.ExistingActivity;
        if (existing is null)
        {
            return true;
        }

        return command.Type != existing.Type
            || !string.Equals(normalizedSubject, Normalize(existing.Subject), StringComparison.Ordinal)
            || command.ScheduledAtUtc != existing.ScheduledAtUtc
            || !string.Equals(normalizedLeadId, Normalize(existing.LeadId), StringComparison.OrdinalIgnoreCase)
            || !string.Equals(normalizedContactId, Normalize(existing.ContactId), StringComparison.OrdinalIgnoreCase);
    }

    private static ActivityManagementRuleResult Reject(
        ActivityManagementCommand command,
        ActivityManagementErrorCode errorCode,
        string message,
        string? normalizedSubject,
        string? normalizedLeadId,
        string? normalizedContactId,
        ActivityStatus resultStatus = ActivityStatus.Scheduled,
        DateTimeOffset? completedAtUtc = null) =>
        ActivityManagementRuleResult.Rejected(command, errorCode, message, normalizedSubject, normalizedLeadId, normalizedContactId, resultStatus, completedAtUtc);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool IsValidId(string? value) =>
        Guid.TryParse(value, out var parsed) && parsed != Guid.Empty;
}
