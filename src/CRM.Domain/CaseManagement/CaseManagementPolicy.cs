using CRM.Domain.Enums;

namespace CRM.Domain.CaseManagement;

public static class CaseManagementPolicy
{
    public const int MaxTitleLength = 160;
    public const int MaxSummaryLength = 1000;

    public static CaseManagementRuleResult Evaluate(CaseManagementCommand command)
    {
        var id = Normalize(command.CaseId);
        var customerId = Normalize(command.CustomerId);
        var title = Normalize(command.Title);
        var summary = Normalize(command.Summary);

        if (!Enum.IsDefined(command.Operation))
            return Reject(command, CaseManagementErrorCode.InvalidOperation, "Case operation is invalid.", id, customerId, title, summary);
        if (!Enum.IsDefined(command.Priority))
            return Reject(command, CaseManagementErrorCode.InvalidPriority, "Case priority is invalid.", id, customerId, title, summary);

        if (command.Operation != CaseManagementOperation.Create)
        {
            if (!ValidId(id))
                return Reject(command, CaseManagementErrorCode.InvalidCaseId, "Case id is required.", id, customerId, title, summary);
            if (command.ExistingCase is null)
                return Reject(command, CaseManagementErrorCode.CaseNotFound, "Existing case snapshot is required.", id, customerId, title, summary);
            if (!string.Equals(id, Normalize(command.ExistingCase.CaseId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, CaseManagementErrorCode.InvalidCaseId, "Case id cannot change.", id, customerId, title, summary, command.ExistingCase.Priority, command.ExistingCase.Status);
        }

        return command.Operation switch
        {
            CaseManagementOperation.Create or CaseManagementOperation.Update => EvaluateProfile(command, id, customerId, title, summary),
            CaseManagementOperation.Start => EvaluateStart(command, id!),
            CaseManagementOperation.Resolve => EvaluateResolve(command, id!),
            CaseManagementOperation.Close => EvaluateClose(command, id!),
            _ => Reject(command, CaseManagementErrorCode.InvalidOperation, "Case operation is invalid.", id, customerId, title, summary)
        };
    }

    private static CaseManagementRuleResult EvaluateProfile(CaseManagementCommand command, string? id, string? customerId, string? title, string? summary)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            return Reject(command, CaseManagementErrorCode.CustomerIdRequired, "Case customer id is required.", id, customerId, title, summary);
        if (!ValidId(customerId))
            return Reject(command, CaseManagementErrorCode.InvalidCustomerId, "Case customer id must be a non-empty GUID.", id, customerId, title, summary);
        if (string.IsNullOrWhiteSpace(title))
            return Reject(command, CaseManagementErrorCode.TitleRequired, "Case title is required.", id, customerId, title, summary);
        if (title.Length > MaxTitleLength)
            return Reject(command, CaseManagementErrorCode.TitleTooLong, "Case title exceeds the allowed length.", id, customerId, title, summary);
        if (string.IsNullOrWhiteSpace(summary))
            return Reject(command, CaseManagementErrorCode.SummaryRequired, "Case summary is required.", id, customerId, title, summary);
        if (summary.Length > MaxSummaryLength)
            return Reject(command, CaseManagementErrorCode.SummaryTooLong, "Case summary exceeds the allowed length.", id, customerId, title, summary);

        if (command.Operation == CaseManagementOperation.Update && command.ExistingCase!.Status is CaseStatus.Resolved or CaseStatus.Closed)
        {
            var code = command.ExistingCase.Status == CaseStatus.Resolved
                ? CaseManagementErrorCode.ResolvedCaseCannotBeModified
                : CaseManagementErrorCode.ClosedCaseCannotBeModified;
            return Reject(command, code, "Resolved and closed cases cannot be modified.", id, customerId, title, summary, command.ExistingCase.Priority, command.ExistingCase.Status);
        }

        var status = command.Operation == CaseManagementOperation.Create ? CaseStatus.Open : command.ExistingCase!.Status;
        var changed = command.Operation == CaseManagementOperation.Create || HasChanged(command.ExistingCase!, customerId, title, summary, command.Priority);
        return Result(command, id, customerId, title, summary, command.Priority, status, true, changed, CaseManagementErrorCode.None,
            changed ? "Case management operation is valid." : "Case update has no changes.");
    }

    private static CaseManagementRuleResult EvaluateStart(CaseManagementCommand command, string id)
    {
        var existing = command.ExistingCase!;
        if (existing.Status == CaseStatus.InProgress)
            return FromExisting(command, existing, id, CaseStatus.InProgress, true, false, CaseManagementErrorCode.None, "Case is already in progress.");
        if (existing.Status == CaseStatus.Open)
            return FromExisting(command, existing, id, CaseStatus.InProgress, true, true, CaseManagementErrorCode.None, "Case start is valid.");
        return FromExisting(command, existing, id, existing.Status, false, false, existing.Status == CaseStatus.Closed ? CaseManagementErrorCode.ClosedCaseCannotBeModified : CaseManagementErrorCode.InvalidStatusTransition, "Case cannot be started from its current status.");
    }

    private static CaseManagementRuleResult EvaluateResolve(CaseManagementCommand command, string id)
    {
        var existing = command.ExistingCase!;
        if (existing.Status == CaseStatus.Resolved)
            return FromExisting(command, existing, id, CaseStatus.Resolved, true, false, CaseManagementErrorCode.None, "Case is already resolved.");
        if (existing.Status is CaseStatus.Open or CaseStatus.InProgress)
            return FromExisting(command, existing, id, CaseStatus.Resolved, true, true, CaseManagementErrorCode.None, "Case resolution is valid.");
        return FromExisting(command, existing, id, existing.Status, false, false, existing.Status == CaseStatus.Closed ? CaseManagementErrorCode.ClosedCaseCannotBeModified : CaseManagementErrorCode.InvalidStatusTransition, "Case cannot be resolved from its current status.");
    }

    private static CaseManagementRuleResult EvaluateClose(CaseManagementCommand command, string id)
    {
        var existing = command.ExistingCase!;
        if (existing.Status == CaseStatus.Closed)
            return FromExisting(command, existing, id, CaseStatus.Closed, true, false, CaseManagementErrorCode.None, "Case is already closed.");
        if (existing.Status == CaseStatus.Resolved)
            return FromExisting(command, existing, id, CaseStatus.Closed, true, true, CaseManagementErrorCode.None, "Case closure is valid.");
        return FromExisting(command, existing, id, existing.Status, false, false, CaseManagementErrorCode.InvalidStatusTransition, "Only resolved cases can be closed.");
    }

    private static bool HasChanged(CaseManagementSnapshot existing, string customerId, string title, string summary, CasePriority priority) =>
        !string.Equals(customerId, Normalize(existing.CustomerId), StringComparison.OrdinalIgnoreCase)
        || !string.Equals(title, Normalize(existing.Title), StringComparison.Ordinal)
        || !string.Equals(summary, Normalize(existing.Summary), StringComparison.Ordinal)
        || priority != existing.Priority;

    private static CaseManagementRuleResult FromExisting(CaseManagementCommand command, CaseManagementSnapshot existing, string id, CaseStatus status, bool allowed, bool changed, CaseManagementErrorCode code, string message) =>
        new(id, command.Operation, allowed, changed, code, message, Normalize(existing.CustomerId), Normalize(existing.Title), Normalize(existing.Summary), existing.Priority, status);

    private static CaseManagementRuleResult Reject(CaseManagementCommand command, CaseManagementErrorCode code, string message, string? id, string? customerId, string? title, string? summary, CasePriority priority = CasePriority.Medium, CaseStatus status = CaseStatus.Open) =>
        Result(command, id, customerId, title, summary, priority, status, false, false, code, message);

    private static CaseManagementRuleResult Result(CaseManagementCommand command, string? id, string? customerId, string? title, string? summary, CasePriority priority, CaseStatus status, bool allowed, bool changed, CaseManagementErrorCode code, string message) =>
        new(id, command.Operation, allowed, changed, code, message, customerId, title, summary, priority, status);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool ValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
