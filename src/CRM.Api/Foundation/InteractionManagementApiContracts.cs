using CRM.Application.InteractionManagement;
using CRM.Domain.InteractionManagement;

namespace CRM.Api.Foundation;

public sealed record FoundationInteractionCreateRequest(
    InteractionRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string? Subject,
    string? Summary,
    DateTimeOffset? OccurredAtUtc);

public sealed record FoundationInteractionUpdateRequest(
    InteractionRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string? Subject,
    string? Summary,
    DateTimeOffset? OccurredAtUtc);

public sealed record InteractionManagementApiInteraction(
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

public sealed record InteractionManagementApiResponse(
    string? Id,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    InteractionStatus? Status,
    InteractionManagementApiInteraction? Interaction,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    bool ActivitySchedulingEnabled,
    bool CrossEntityMutationEnabled,
    string Warning)
{
    public static InteractionManagementApiResponse From(InteractionManagementApplicationResult result) =>
        new(
            result.InteractionId,
            result.Operation,
            result.Allowed,
            result.Changed,
            result.ErrorCode,
            result.Message,
            result.Status,
            result.Interaction is null ? null : new(
                result.Interaction.Id,
                result.Interaction.RelatedEntityType,
                result.Interaction.RelatedEntityId,
                result.Interaction.Channel,
                result.Interaction.Direction,
                result.Interaction.Subject,
                result.Interaction.Summary,
                result.Interaction.OccurredAtUtc,
                result.Interaction.Status,
                result.Interaction.PersistenceMode,
                result.Interaction.ProductiveCrudEnabled),
            FoundationMode: true,
            PersistenceMode: result.Interaction?.PersistenceMode ?? "NonProductionSeam",
            DurablePersistence: false,
            ProductiveCrudEnabled: false,
            PortalRuntimeEnabled: false,
            CommonDbRuntimeEnabled: false,
            ActivitySchedulingEnabled: false,
            CrossEntityMutationEnabled: false,
            "Foundation Interaction API only; productive route remains locked");

    public static int ToStatusCode(InteractionManagementApplicationResult result) => result.ErrorCode switch
    {
        nameof(InteractionManagementErrorCode.None) => StatusCodes.Status200OK,
        nameof(InteractionManagementErrorCode.InteractionNotFound) => StatusCodes.Status404NotFound,
        nameof(InteractionManagementErrorCode.VoidedInteractionCannotBeModified) => StatusCodes.Status409Conflict,
        nameof(InteractionManagementErrorCode.InvalidStatus) => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };

    public static InteractionManagementCreateRequest ToApplicationRequest(FoundationInteractionCreateRequest request) =>
        new(
            request.RelatedEntityType,
            request.RelatedEntityId,
            request.Channel,
            request.Direction,
            request.Subject,
            request.Summary,
            request.OccurredAtUtc);

    public static InteractionManagementUpdateRequest ToApplicationRequest(FoundationInteractionUpdateRequest request) =>
        new(
            request.RelatedEntityType,
            request.RelatedEntityId,
            request.Channel,
            request.Direction,
            request.Subject,
            request.Summary,
            request.OccurredAtUtc);
}
