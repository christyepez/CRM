namespace CRM.Domain.AssignmentManagement;

public sealed record AssignmentRuleResult(
    string? AssignmentId,
    AssignmentOperation Operation,
    bool Allowed,
    bool Changed,
    AssignmentErrorCode ErrorCode,
    string Message,
    AssignmentRelatedEntityType RelatedEntityType,
    string? NormalizedRelatedEntityId,
    string? NormalizedAssigneeReferenceId,
    string? NormalizedAssignmentLabel,
    AssignmentStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == AssignmentErrorCode.None;
}
