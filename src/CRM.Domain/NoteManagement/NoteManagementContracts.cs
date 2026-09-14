namespace CRM.Domain.NoteManagement;

public sealed record NoteManagementSnapshot(
    string NoteId,
    NoteRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string Text,
    NoteStatus Status);

public sealed record NoteManagementCommand(
    NoteManagementOperation Operation,
    string? NoteId,
    NoteRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? Text,
    NoteManagementSnapshot? ExistingNote = null);

public sealed record NoteManagementRuleResult(
    string? NoteId,
    NoteManagementOperation Operation,
    bool Allowed,
    bool Changed,
    NoteManagementErrorCode ErrorCode,
    string Message,
    NoteRelatedEntityType RelatedEntityType,
    string? NormalizedRelatedEntityId,
    string? NormalizedText,
    NoteStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == NoteManagementErrorCode.None;

    public static NoteManagementRuleResult Rejected(
        NoteManagementCommand command,
        NoteManagementErrorCode errorCode,
        string message,
        string? noteId,
        string? normalizedRelatedEntityId,
        string? normalizedText,
        NoteStatus resultStatus = NoteStatus.Active) =>
        new(
            noteId,
            command.Operation,
            Allowed: false,
            Changed: false,
            errorCode,
            message,
            command.RelatedEntityType,
            normalizedRelatedEntityId,
            normalizedText,
            resultStatus);
}
