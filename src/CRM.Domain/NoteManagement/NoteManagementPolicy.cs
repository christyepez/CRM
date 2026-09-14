namespace CRM.Domain.NoteManagement;

public static class NoteManagementPolicy
{
    public const int MaxTextLength = 4000;

    public static NoteManagementRuleResult Evaluate(NoteManagementCommand command)
    {
        var noteId = Normalize(command.NoteId);
        var relatedEntityId = Normalize(command.RelatedEntityId);
        var text = Normalize(command.Text);

        if (!Enum.IsDefined(command.Operation))
            return Reject(command, NoteManagementErrorCode.InvalidOperation, "Note operation is invalid.", noteId, relatedEntityId, text);
        if (!Enum.IsDefined(command.RelatedEntityType))
            return Reject(command, NoteManagementErrorCode.InvalidRelatedEntityType, "Note related entity type is invalid.", noteId, relatedEntityId, text);

        if (command.Operation != NoteManagementOperation.Create)
        {
            if (!IsValidId(noteId))
                return Reject(command, NoteManagementErrorCode.InvalidNoteId, "Note id is required.", noteId, relatedEntityId, text);
            if (command.ExistingNote is null)
                return Reject(command, NoteManagementErrorCode.NoteNotFound, "Existing note snapshot is required.", noteId, relatedEntityId, text);
            if (!string.Equals(noteId, Normalize(command.ExistingNote.NoteId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, NoteManagementErrorCode.InvalidNoteId, "Note id cannot change.", noteId, relatedEntityId, text, command.ExistingNote.Status);
            if (!Enum.IsDefined(command.ExistingNote.Status))
                return Reject(command, NoteManagementErrorCode.InvalidStatus, "Existing note status is invalid.", noteId, relatedEntityId, text);
        }

        return command.Operation switch
        {
            NoteManagementOperation.Create or NoteManagementOperation.Update => EvaluateRecord(command, noteId, relatedEntityId, text),
            NoteManagementOperation.Archive => EvaluateArchive(command, noteId!),
            _ => Reject(command, NoteManagementErrorCode.InvalidOperation, "Note operation is invalid.", noteId, relatedEntityId, text)
        };
    }

    private static NoteManagementRuleResult EvaluateRecord(NoteManagementCommand command, string? noteId, string? relatedEntityId, string? text)
    {
        var validation = ValidateFields(command, noteId, relatedEntityId, text);
        if (validation is not null)
            return validation;

        if (command.Operation == NoteManagementOperation.Update && command.ExistingNote!.Status == NoteStatus.Archived)
            return Reject(command, NoteManagementErrorCode.ArchivedNoteCannotBeModified, "Archived notes cannot be modified.", noteId, relatedEntityId, text, NoteStatus.Archived);

        var changed = command.Operation == NoteManagementOperation.Create || HasChanged(command, relatedEntityId!, text!);
        var status = command.Operation == NoteManagementOperation.Create ? NoteStatus.Active : command.ExistingNote!.Status;
        return Result(command, noteId, relatedEntityId, text, status, true, changed, NoteManagementErrorCode.None,
            changed ? "Note management operation is valid." : "Note update has no changes.");
    }

    private static NoteManagementRuleResult EvaluateArchive(NoteManagementCommand command, string noteId)
    {
        var existing = command.ExistingNote!;
        var relatedEntityId = Normalize(existing.RelatedEntityId);
        var text = Normalize(existing.Text);
        if (existing.Status == NoteStatus.Archived)
            return Result(command, noteId, relatedEntityId, text, NoteStatus.Archived, true, false, NoteManagementErrorCode.None, "Note is already archived.", existing);

        return Result(command, noteId, relatedEntityId, text, NoteStatus.Archived, true, true, NoteManagementErrorCode.None, "Note archive is valid.", existing);
    }

    private static NoteManagementRuleResult? ValidateFields(NoteManagementCommand command, string? noteId, string? relatedEntityId, string? text)
    {
        if (string.IsNullOrWhiteSpace(relatedEntityId))
            return Reject(command, NoteManagementErrorCode.RelatedEntityIdRequired, "Note related entity id is required.", noteId, relatedEntityId, text);
        if (!IsValidId(relatedEntityId))
            return Reject(command, NoteManagementErrorCode.InvalidRelatedEntityId, "Note related entity id must be a non-empty GUID.", noteId, relatedEntityId, text);
        if (string.IsNullOrWhiteSpace(text))
            return Reject(command, NoteManagementErrorCode.TextRequired, "Note text is required.", noteId, relatedEntityId, text);
        if (text.Length > MaxTextLength)
            return Reject(command, NoteManagementErrorCode.TextTooLong, "Note text exceeds the allowed length.", noteId, relatedEntityId, text);
        return null;
    }

    private static bool HasChanged(NoteManagementCommand command, string relatedEntityId, string text)
    {
        var existing = command.ExistingNote!;
        return command.RelatedEntityType != existing.RelatedEntityType
            || !string.Equals(relatedEntityId, Normalize(existing.RelatedEntityId), StringComparison.OrdinalIgnoreCase)
            || !string.Equals(text, Normalize(existing.Text), StringComparison.Ordinal);
    }

    private static NoteManagementRuleResult Reject(NoteManagementCommand command, NoteManagementErrorCode errorCode, string message,
        string? noteId, string? relatedEntityId, string? text, NoteStatus resultStatus = NoteStatus.Active) =>
        NoteManagementRuleResult.Rejected(command, errorCode, message, noteId, relatedEntityId, text, resultStatus);

    private static NoteManagementRuleResult Result(NoteManagementCommand command, string? noteId, string? relatedEntityId, string? text,
        NoteStatus status, bool allowed, bool changed, NoteManagementErrorCode errorCode, string message,
        NoteManagementSnapshot? source = null) =>
        new(noteId, command.Operation, allowed, changed, errorCode, message,
            source?.RelatedEntityType ?? command.RelatedEntityType, relatedEntityId, text, status);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool IsValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
