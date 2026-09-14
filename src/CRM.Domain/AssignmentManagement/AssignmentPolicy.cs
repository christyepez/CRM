namespace CRM.Domain.AssignmentManagement;

public static class AssignmentPolicy
{
    public const int MaxAssigneeReferenceIdLength = 200;
    public const int MaxAssignmentLabelLength = 120;

    public static AssignmentRuleResult Evaluate(AssignmentCommand command)
    {
        var id = Normalize(command.AssignmentId);
        var relatedId = Normalize(command.RelatedEntityId);
        var assignee = Normalize(command.AssigneeReferenceId);
        var label = Normalize(command.AssignmentLabel);

        if (!Enum.IsDefined(command.Operation)) return Reject(command, AssignmentErrorCode.InvalidOperation, "Invalid operation.", id, relatedId, assignee, label);
        if (!Enum.IsDefined(command.RelatedEntityType)) return Reject(command, AssignmentErrorCode.InvalidRelatedEntityType, "Invalid related entity type.", id, relatedId, assignee, label);
        if (relatedId is null) return Reject(command, AssignmentErrorCode.RelatedEntityIdRequired, "Related entity id is required.", id, relatedId, assignee, label);
        if (!ValidId(relatedId)) return Reject(command, AssignmentErrorCode.InvalidRelatedEntityId, "Related entity id must be a non-empty GUID.", id, relatedId, assignee, label);
        if (assignee is null) return Reject(command, AssignmentErrorCode.AssigneeReferenceIdRequired, "Assignee reference is required.", id, relatedId, assignee, label);
        if (assignee.Length > MaxAssigneeReferenceIdLength) return Reject(command, AssignmentErrorCode.AssigneeReferenceIdTooLong, "Assignee reference is too long.", id, relatedId, assignee, label);
        if (label?.Length > MaxAssignmentLabelLength) return Reject(command, AssignmentErrorCode.AssignmentLabelTooLong, "Assignment label is too long.", id, relatedId, assignee, label);

        if (command.Operation != AssignmentOperation.Create)
        {
            if (!ValidId(id)) return Reject(command, AssignmentErrorCode.InvalidAssignmentId, "Assignment id must be a non-empty GUID.", id, relatedId, assignee, label);
            if (command.ExistingAssignment is null) return Reject(command, AssignmentErrorCode.AssignmentNotFound, "Assignment was not found.", id, relatedId, assignee, label);
            if (!string.Equals(command.ExistingAssignment.AssignmentId, id, StringComparison.OrdinalIgnoreCase)) return Reject(command, AssignmentErrorCode.InvalidAssignmentId, "Assignment id mismatch.", id, relatedId, assignee, label);
            if (!Enum.IsDefined(command.ExistingAssignment.Status)) return Reject(command, AssignmentErrorCode.InvalidStatus, "Invalid status.", id, relatedId, assignee, label);
            if (command.Operation == AssignmentOperation.Update && command.ExistingAssignment.Status == AssignmentStatus.Archived)
                return Reject(command, AssignmentErrorCode.ArchivedAssignmentCannotBeModified, "Archived assignments are read-only.", id, relatedId, assignee, label, AssignmentStatus.Archived);
        }

        if (command.Operation == AssignmentOperation.Archive)
        {
            var archiveChanged = command.ExistingAssignment!.Status != AssignmentStatus.Archived;
            return new(id, command.Operation, true, archiveChanged, AssignmentErrorCode.None,
                archiveChanged ? "Assignment archived." : "No changes were necessary.",
                command.ExistingAssignment.RelatedEntityType,
                command.ExistingAssignment.RelatedEntityId,
                command.ExistingAssignment.AssigneeReferenceId,
                command.ExistingAssignment.AssignmentLabel,
                AssignmentStatus.Archived);
        }

        var contentChanged = command.Operation == AssignmentOperation.Create ||
            command.ExistingAssignment!.RelatedEntityType != command.RelatedEntityType ||
            !string.Equals(command.ExistingAssignment.RelatedEntityId, relatedId, StringComparison.Ordinal) ||
            !string.Equals(command.ExistingAssignment.AssigneeReferenceId, assignee, StringComparison.Ordinal) ||
            !string.Equals(command.ExistingAssignment.AssignmentLabel, label, StringComparison.Ordinal);

        return new(id, command.Operation, true, contentChanged, AssignmentErrorCode.None,
            contentChanged ? "Assignment accepted." : "No changes were necessary.",
            command.RelatedEntityType, relatedId, assignee, label, AssignmentStatus.Active);
    }

    private static AssignmentRuleResult Reject(AssignmentCommand c, AssignmentErrorCode e, string m,
        string? id, string? relatedId, string? assignee, string? label,
        AssignmentStatus status = AssignmentStatus.Active) =>
        new(id, c.Operation, false, false, e, m, c.RelatedEntityType, relatedId, assignee, label, status);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool ValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
