using CRM.Domain.AssignmentManagement;

namespace CRM.Application.AssignmentManagement;

public sealed record AssignmentCreateRequest(
    AssignmentRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? AssigneeReferenceId,
    string? AssignmentLabel);

public sealed record AssignmentUpdateRequest(
    AssignmentRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? AssigneeReferenceId,
    string? AssignmentLabel);

public sealed record AssignmentApplicationItem(
    string Id,
    AssignmentRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string AssigneeReferenceId,
    string? AssignmentLabel,
    AssignmentStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled,
    bool PortalIdentityRuntimeEnabled);

public sealed record AssignmentApplicationResult(
    string? AssignmentId,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    AssignmentStatus? Status,
    AssignmentApplicationItem? Assignment)
{
    public bool Success => Allowed && ErrorCode == "None";
}
