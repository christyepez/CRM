namespace CRM.Domain.InteractionManagement;

public static class InteractionManagementPolicy
{
    public const int MaxSubjectLength = 160;
    public const int MaxSummaryLength = 2000;

    public static InteractionManagementRuleResult Evaluate(InteractionManagementCommand command)
    {
        var interactionId = Normalize(command.InteractionId);
        var relatedEntityId = Normalize(command.RelatedEntityId);
        var subject = Normalize(command.Subject);
        var summary = Normalize(command.Summary);

        if (!Enum.IsDefined(command.Operation))
            return Reject(command, InteractionManagementErrorCode.InvalidOperation, "Interaction operation is invalid.", interactionId, relatedEntityId, subject, summary);
        if (!Enum.IsDefined(command.RelatedEntityType))
            return Reject(command, InteractionManagementErrorCode.InvalidRelatedEntityType, "Interaction related entity type is invalid.", interactionId, relatedEntityId, subject, summary);
        if (!Enum.IsDefined(command.Channel))
            return Reject(command, InteractionManagementErrorCode.InvalidChannel, "Interaction channel is invalid.", interactionId, relatedEntityId, subject, summary);
        if (!Enum.IsDefined(command.Direction))
            return Reject(command, InteractionManagementErrorCode.InvalidDirection, "Interaction direction is invalid.", interactionId, relatedEntityId, subject, summary);

        if (!IsUtc(command.EvaluatedAtUtc))
            return Reject(command, InteractionManagementErrorCode.EvaluationTimestampMustBeUtc, "Interaction evaluation timestamp must be UTC.", interactionId, relatedEntityId, subject, summary);

        if (command.Operation != InteractionManagementOperation.Create)
        {
            if (!IsValidId(interactionId))
                return Reject(command, InteractionManagementErrorCode.InvalidInteractionId, "Interaction id is required.", interactionId, relatedEntityId, subject, summary);
            if (command.ExistingInteraction is null)
                return Reject(command, InteractionManagementErrorCode.InteractionNotFound, "Existing interaction snapshot is required.", interactionId, relatedEntityId, subject, summary);
            if (!string.Equals(interactionId, Normalize(command.ExistingInteraction.InteractionId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, InteractionManagementErrorCode.InvalidInteractionId, "Interaction id cannot change.", interactionId, relatedEntityId, subject, summary, command.ExistingInteraction.Status);
            if (!Enum.IsDefined(command.ExistingInteraction.Status))
                return Reject(command, InteractionManagementErrorCode.InvalidStatus, "Existing interaction status is invalid.", interactionId, relatedEntityId, subject, summary);
        }

        return command.Operation switch
        {
            InteractionManagementOperation.Create or InteractionManagementOperation.Update => EvaluateRecord(command, interactionId, relatedEntityId, subject, summary),
            InteractionManagementOperation.Void => EvaluateVoid(command, interactionId!),
            _ => Reject(command, InteractionManagementErrorCode.InvalidOperation, "Interaction operation is invalid.", interactionId, relatedEntityId, subject, summary)
        };
    }

    private static InteractionManagementRuleResult EvaluateRecord(InteractionManagementCommand command, string? interactionId, string? relatedEntityId, string? subject, string? summary)
    {
        var validation = ValidateRecordFields(command, interactionId, relatedEntityId, subject, summary);
        if (validation is not null)
            return validation;

        if (command.Operation == InteractionManagementOperation.Update && command.ExistingInteraction!.Status == InteractionStatus.Voided)
            return Reject(command, InteractionManagementErrorCode.VoidedInteractionCannotBeModified, "Voided interactions cannot be modified.", interactionId, relatedEntityId, subject, summary, InteractionStatus.Voided);

        var changed = command.Operation == InteractionManagementOperation.Create || HasChanged(command, relatedEntityId!, subject!, summary!);
        var status = command.Operation == InteractionManagementOperation.Create ? InteractionStatus.Recorded : command.ExistingInteraction!.Status;

        return Result(
            command,
            interactionId,
            relatedEntityId,
            subject,
            summary,
            status,
            Allowed: true,
            changed,
            InteractionManagementErrorCode.None,
            changed ? "Interaction management operation is valid." : "Interaction update has no changes.");
    }

    private static InteractionManagementRuleResult EvaluateVoid(InteractionManagementCommand command, string interactionId)
    {
        var existing = command.ExistingInteraction!;
        var relatedEntityId = Normalize(existing.RelatedEntityId);
        var subject = Normalize(existing.Subject);
        var summary = Normalize(existing.Summary);

        if (existing.Status == InteractionStatus.Voided)
        {
            return Result(
                command,
                interactionId,
                relatedEntityId,
                subject,
                summary,
                InteractionStatus.Voided,
                Allowed: true,
                Changed: false,
                InteractionManagementErrorCode.None,
                "Interaction is already voided.",
                existing);
        }

        return Result(
            command,
            interactionId,
            relatedEntityId,
            subject,
            summary,
            InteractionStatus.Voided,
            Allowed: true,
            Changed: true,
            InteractionManagementErrorCode.None,
            "Interaction void is valid.",
            existing);
    }

    private static InteractionManagementRuleResult? ValidateRecordFields(InteractionManagementCommand command, string? interactionId, string? relatedEntityId, string? subject, string? summary)
    {
        if (string.IsNullOrWhiteSpace(relatedEntityId))
            return Reject(command, InteractionManagementErrorCode.RelatedEntityIdRequired, "Interaction related entity id is required.", interactionId, relatedEntityId, subject, summary);
        if (!IsValidId(relatedEntityId))
            return Reject(command, InteractionManagementErrorCode.InvalidRelatedEntityId, "Interaction related entity id must be a non-empty GUID.", interactionId, relatedEntityId, subject, summary);
        if (string.IsNullOrWhiteSpace(subject))
            return Reject(command, InteractionManagementErrorCode.SubjectRequired, "Interaction subject is required.", interactionId, relatedEntityId, subject, summary);
        if (subject.Length > MaxSubjectLength)
            return Reject(command, InteractionManagementErrorCode.SubjectTooLong, "Interaction subject exceeds the allowed length.", interactionId, relatedEntityId, subject, summary);
        if (string.IsNullOrWhiteSpace(summary))
            return Reject(command, InteractionManagementErrorCode.SummaryRequired, "Interaction summary is required.", interactionId, relatedEntityId, subject, summary);
        if (summary.Length > MaxSummaryLength)
            return Reject(command, InteractionManagementErrorCode.SummaryTooLong, "Interaction summary exceeds the allowed length.", interactionId, relatedEntityId, subject, summary);
        if (command.OccurredAtUtc == default)
            return Reject(command, InteractionManagementErrorCode.OccurredAtUtcRequired, "Interaction occurred timestamp is required.", interactionId, relatedEntityId, subject, summary);
        if (!IsUtc(command.OccurredAtUtc))
            return Reject(command, InteractionManagementErrorCode.OccurredAtUtcMustBeUtc, "Interaction occurred timestamp must be UTC.", interactionId, relatedEntityId, subject, summary);
        if (command.OccurredAtUtc > command.EvaluatedAtUtc)
            return Reject(command, InteractionManagementErrorCode.OccurredAtUtcInFuture, "Interaction occurred timestamp cannot be in the future.", interactionId, relatedEntityId, subject, summary);

        return null;
    }

    private static bool HasChanged(InteractionManagementCommand command, string relatedEntityId, string subject, string summary)
    {
        var existing = command.ExistingInteraction!;
        return command.RelatedEntityType != existing.RelatedEntityType
            || !string.Equals(relatedEntityId, Normalize(existing.RelatedEntityId), StringComparison.OrdinalIgnoreCase)
            || command.Channel != existing.Channel
            || command.Direction != existing.Direction
            || !string.Equals(subject, Normalize(existing.Subject), StringComparison.Ordinal)
            || !string.Equals(summary, Normalize(existing.Summary), StringComparison.Ordinal)
            || command.OccurredAtUtc != existing.OccurredAtUtc;
    }

    private static InteractionManagementRuleResult Reject(
        InteractionManagementCommand command,
        InteractionManagementErrorCode errorCode,
        string message,
        string? interactionId,
        string? relatedEntityId,
        string? subject,
        string? summary,
        InteractionStatus resultStatus = InteractionStatus.Recorded) =>
        InteractionManagementRuleResult.Rejected(command, errorCode, message, interactionId, relatedEntityId, subject, summary, resultStatus);

    private static InteractionManagementRuleResult Result(
        InteractionManagementCommand command,
        string? interactionId,
        string? relatedEntityId,
        string? subject,
        string? summary,
        InteractionStatus status,
        bool Allowed,
        bool Changed,
        InteractionManagementErrorCode errorCode,
        string message,
        InteractionManagementSnapshot? source = null) =>
        new(
            interactionId,
            command.Operation,
            Allowed,
            Changed,
            errorCode,
            message,
            source?.RelatedEntityType ?? command.RelatedEntityType,
            relatedEntityId,
            source?.Channel ?? command.Channel,
            source?.Direction ?? command.Direction,
            subject,
            summary,
            source?.OccurredAtUtc ?? command.OccurredAtUtc,
            status);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool IsValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;

    private static bool IsUtc(DateTimeOffset value) => value.Offset == TimeSpan.Zero;
}
