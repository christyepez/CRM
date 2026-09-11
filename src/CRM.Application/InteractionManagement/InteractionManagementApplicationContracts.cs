using CRM.Domain.InteractionManagement;

namespace CRM.Application.InteractionManagement;

public sealed record InteractionManagementCreateRequest(
    InteractionRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string? Subject,
    string? Summary,
    DateTimeOffset? OccurredAtUtc);

public sealed record InteractionManagementUpdateRequest(
    InteractionRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string? Subject,
    string? Summary,
    DateTimeOffset? OccurredAtUtc);

public sealed record InteractionManagementApplicationInteraction(
    string Id,
    InteractionRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string Subject,
    string Summary,
    DateTimeOffset OccurredAtUtc,
    InteractionStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record InteractionManagementApplicationResult(
    string? InteractionId,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    InteractionStatus? Status,
    InteractionManagementApplicationInteraction? Interaction)
{
    public bool Success => Allowed && ErrorCode == "None";
}
