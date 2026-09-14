namespace CRM.Domain.InteractionManagement;

public sealed record InteractionManagementSnapshot(
    string InteractionId,
    InteractionRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string Subject,
    string Summary,
    DateTimeOffset OccurredAtUtc,
    InteractionStatus Status);

public sealed record InteractionManagementCommand(
    InteractionManagementOperation Operation,
    string? InteractionId,
    InteractionRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string? Subject,
    string? Summary,
    DateTimeOffset OccurredAtUtc,
    DateTimeOffset EvaluatedAtUtc,
    InteractionManagementSnapshot? ExistingInteraction = null);

public sealed record InteractionManagementRuleResult(
    string? InteractionId,
    InteractionManagementOperation Operation,
    bool Allowed,
    bool Changed,
    InteractionManagementErrorCode ErrorCode,
    string Message,
    InteractionRelatedEntityType RelatedEntityType,
    string? NormalizedRelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string? NormalizedSubject,
    string? NormalizedSummary,
    DateTimeOffset OccurredAtUtc,
    InteractionStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == InteractionManagementErrorCode.None;

    public static InteractionManagementRuleResult Rejected(
        InteractionManagementCommand command,
        InteractionManagementErrorCode errorCode,
        string message,
        string? interactionId,
        string? normalizedRelatedEntityId,
        string? normalizedSubject,
        string? normalizedSummary,
        InteractionStatus resultStatus = InteractionStatus.Recorded) =>
        new(
            interactionId,
            command.Operation,
            Allowed: false,
            Changed: false,
            errorCode,
            message,
            command.RelatedEntityType,
            normalizedRelatedEntityId,
            command.Channel,
            command.Direction,
            normalizedSubject,
            normalizedSummary,
            command.OccurredAtUtc,
            resultStatus);
}
