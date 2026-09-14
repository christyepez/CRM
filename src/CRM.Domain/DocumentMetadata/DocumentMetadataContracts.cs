namespace CRM.Domain.DocumentMetadata;

public sealed record DocumentMetadataSnapshot(
    string DocumentId,
    DocumentRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string FileReferenceId,
    string FileName,
    string? ContentType,
    string? Description,
    DocumentMetadataStatus Status);

public sealed record DocumentMetadataCommand(
    DocumentMetadataOperation Operation,
    string? DocumentId,
    DocumentRelatedEntityType RelatedEntityType,
    string? RelatedEntityId,
    string? FileReferenceId,
    string? FileName,
    string? ContentType,
    string? Description,
    DocumentMetadataSnapshot? ExistingDocument = null);
