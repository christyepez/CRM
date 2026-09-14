namespace CRM.Domain.AssignmentManagement;

public sealed record AssignmentSnapshot(
    string AssignmentId,
    AssignmentRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string AssigneeReferenceId,
    string? AssignmentLabel,
    AssignmentStatus Status);

public sealed record AssignmentCommand(
    AssignmentOperation Operation,
    string? AssignmentId,
    AssignmentRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? AssigneeReferenceId,
    string? AssignmentLabel,
    AssignmentSnapshot? ExistingAssignment = null);
