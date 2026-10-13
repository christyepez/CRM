using CRM.Application.NoteManagement;
using CRM.Domain.NoteManagement;

namespace CRM.Api.Foundation;

public sealed record FoundationNoteCreateRequest(
    NoteRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? Text);

public sealed record FoundationNoteUpdateRequest(
    NoteRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? Text);

public sealed record NoteManagementApiNote(
    string Id,
    NoteRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string Text,
    NoteStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record NoteManagementApiResponse(
    string? Id,
    string Operation,
    bool Allowed,
    bool Changed,    string ErrorCode,
    string Message,
    NoteStatus? Status,
    NoteManagementApiNote? Note,
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
    public static NoteManagementApiResponse From(NoteManagementApplicationResult result) =>
        new(
            result.NoteId,
            result.Operation,
            result.Allowed,
            result.Changed,
            result.ErrorCode,
            result.Message,
            result.Status,
            result.Note is null ? null : new(
                result.Note.Id,
                result.Note.RelatedEntityType,
                result.Note.RelatedEntityId,
                result.Note.Text,
                result.Note.Status,
                result.Note.PersistenceMode,
                result.Note.ProductiveCrudEnabled),
            FoundationMode: true,
            PersistenceMode: result.Note?.PersistenceMode ?? "NonProductionSeam",
            DurablePersistence: false,
            ProductiveCrudEnabled: false,
            PortalRuntimeEnabled: false,
            CommonDbRuntimeEnabled: false,
            ActivitySchedulingEnabled: false,
            CrossEntityMutationEnabled: false,
            "Foundation Note API only; productive route remains locked");

    public static int ToStatusCode(NoteManagementApplicationResult result) => result.ErrorCode switch
    {
        nameof(NoteManagementErrorCode.None) => StatusCodes.Status200OK,
        nameof(NoteManagementErrorCode.NoteNotFound) => StatusCodes.Status404NotFound,
        nameof(NoteManagementErrorCode.ArchivedNoteCannotBeModified) => StatusCodes.Status409Conflict,
        nameof(NoteManagementErrorCode.InvalidStatus) => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };

    public static NoteManagementCreateRequest ToApplicationRequest(FoundationNoteCreateRequest request) =>
        new(request.RelatedEntityType, request.RelatedEntityId, request.Text);

    public static NoteManagementUpdateRequest ToApplicationRequest(FoundationNoteUpdateRequest request) =>
        new(request.RelatedEntityType, request.RelatedEntityId, request.Text);
}
