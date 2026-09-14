namespace CRM.Domain.DocumentMetadata;

public static class DocumentMetadataPolicy
{
    public const int MaxFileReferenceIdLength = 300;
    public const int MaxFileNameLength = 255;
    public const int MaxContentTypeLength = 120;
    public const int MaxDescriptionLength = 1000;

    public static DocumentMetadataRuleResult Evaluate(DocumentMetadataCommand command)
    {
        var id = Normalize(command.DocumentId);
        var relatedId = Normalize(command.RelatedEntityId);
        var fileReferenceId = Normalize(command.FileReferenceId);
        var fileName = Normalize(command.FileName);
        var contentType = NormalizeOptional(command.ContentType);
        var description = NormalizeOptional(command.Description);

        if (!Enum.IsDefined(command.Operation)) return Reject(command, DocumentMetadataErrorCode.InvalidOperation, "Invalid operation.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (!Enum.IsDefined(command.RelatedEntityType)) return Reject(command, DocumentMetadataErrorCode.InvalidRelatedEntityType, "Invalid related entity type.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (relatedId is null) return Reject(command, DocumentMetadataErrorCode.RelatedEntityIdRequired, "Related entity id is required.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (!ValidId(relatedId)) return Reject(command, DocumentMetadataErrorCode.InvalidRelatedEntityId, "Related entity id must be a non-empty GUID.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (fileReferenceId is null) return Reject(command, DocumentMetadataErrorCode.FileReferenceIdRequired, "File reference id is required.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (fileReferenceId.Length > MaxFileReferenceIdLength) return Reject(command, DocumentMetadataErrorCode.FileReferenceIdTooLong, "File reference id is too long.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (fileName is null) return Reject(command, DocumentMetadataErrorCode.FileNameRequired, "File name is required.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (fileName.Length > MaxFileNameLength) return Reject(command, DocumentMetadataErrorCode.FileNameTooLong, "File name is too long.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (contentType?.Length > MaxContentTypeLength) return Reject(command, DocumentMetadataErrorCode.ContentTypeTooLong, "Content type is too long.", id, relatedId, fileReferenceId, fileName, contentType, description);
        if (description?.Length > MaxDescriptionLength) return Reject(command, DocumentMetadataErrorCode.DescriptionTooLong, "Description is too long.", id, relatedId, fileReferenceId, fileName, contentType, description);

        if (command.Operation != DocumentMetadataOperation.Create)
        {
            if (!ValidId(id)) return Reject(command, DocumentMetadataErrorCode.InvalidDocumentId, "Document id must be a non-empty GUID.", id, relatedId, fileReferenceId, fileName, contentType, description);
            if (command.ExistingDocument is null) return Reject(command, DocumentMetadataErrorCode.DocumentNotFound, "Document metadata was not found.", id, relatedId, fileReferenceId, fileName, contentType, description);
            if (!Enum.IsDefined(command.ExistingDocument.Status)) return Reject(command, DocumentMetadataErrorCode.InvalidStatus, "Invalid document status.", id, relatedId, fileReferenceId, fileName, contentType, description);
            if (!string.Equals(command.ExistingDocument.DocumentId, id, StringComparison.OrdinalIgnoreCase)) return Reject(command, DocumentMetadataErrorCode.InvalidDocumentId, "Document id does not match snapshot.", id, relatedId, fileReferenceId, fileName, contentType, description);
        }

        if (command.Operation == DocumentMetadataOperation.Archive)
        {
            var status = command.ExistingDocument!.Status;
            return Success(command, id!, relatedId, fileReferenceId, fileName, contentType, description, DocumentMetadataStatus.Archived, status != DocumentMetadataStatus.Archived);
        }
        if (command.Operation == DocumentMetadataOperation.Update && command.ExistingDocument!.Status == DocumentMetadataStatus.Archived)
            return Reject(command, DocumentMetadataErrorCode.ArchivedDocumentCannotBeModified, "Archived document metadata is read-only.", id, relatedId, fileReferenceId, fileName, contentType, description, DocumentMetadataStatus.Archived);

        var resultId = command.Operation == DocumentMetadataOperation.Create ? Guid.NewGuid().ToString("D") : id!;
        var changed = command.Operation == DocumentMetadataOperation.Create || !Equivalent(command.ExistingDocument!, command.RelatedEntityType, relatedId, fileReferenceId, fileName, contentType, description);
        return Success(command, resultId, relatedId, fileReferenceId, fileName, contentType, description, DocumentMetadataStatus.Active, changed);
    }

    private static bool Equivalent(DocumentMetadataSnapshot existing, DocumentRelatedEntityType type, string relatedId, string fileReferenceId, string fileName, string? contentType, string? description) =>
        existing.RelatedEntityType == type &&
        string.Equals(existing.RelatedEntityId, relatedId, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(existing.FileReferenceId, fileReferenceId, StringComparison.Ordinal) &&
        string.Equals(existing.FileName, fileName, StringComparison.Ordinal) &&
        string.Equals(existing.ContentType, contentType, StringComparison.Ordinal) &&
        string.Equals(existing.Description, description, StringComparison.Ordinal);

    private static DocumentMetadataRuleResult Success(DocumentMetadataCommand command, string id, string relatedId, string fileReferenceId, string fileName, string? contentType, string? description, DocumentMetadataStatus status, bool changed) =>
        new(id, command.Operation, true, changed, DocumentMetadataErrorCode.None, changed ? "Document metadata operation completed." : "No changes were necessary.", command.RelatedEntityType, relatedId, fileReferenceId, fileName, contentType, description, status);

    private static DocumentMetadataRuleResult Reject(DocumentMetadataCommand command, DocumentMetadataErrorCode code, string message, string? id, string? relatedId, string? fileReferenceId, string? fileName, string? contentType, string? description, DocumentMetadataStatus status = DocumentMetadataStatus.Active) =>
        new(id, command.Operation, false, false, code, message, command.RelatedEntityType, relatedId, fileReferenceId, fileName, contentType, description, status);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static string? NormalizeOptional(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool ValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
