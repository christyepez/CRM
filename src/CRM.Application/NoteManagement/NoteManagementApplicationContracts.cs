using CRM.Domain.NoteManagement;

namespace CRM.Application.NoteManagement;

public sealed record NoteManagementCreateRequest(
    NoteRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? Text);

public sealed record NoteManagementUpdateRequest(
    NoteRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? Text);

public sealed record NoteManagementApplicationNote(
    string Id,
    NoteRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string Text,
    NoteStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record NoteManagementApplicationResult(
    string? NoteId,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    NoteStatus? Status,
    NoteManagementApplicationNote? Note)
{
    public bool Success => Allowed && ErrorCode == "None";
}
