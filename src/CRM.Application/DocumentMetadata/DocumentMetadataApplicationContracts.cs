using CRM.Domain.DocumentMetadata;
namespace CRM.Application.DocumentMetadata;
public sealed record DocumentMetadataCreateRequest(DocumentRelatedEntityType RelatedEntityType,string? RelatedEntityId,string? FileReferenceId,string? FileName,string? ContentType,string? Description);
public sealed record DocumentMetadataUpdateRequest(DocumentRelatedEntityType RelatedEntityType,string? RelatedEntityId,string? FileReferenceId,string? FileName,string? ContentType,string? Description);
public sealed record DocumentMetadataApplicationDocument(string Id,DocumentRelatedEntityType RelatedEntityType,string RelatedEntityId,string FileReferenceId,string FileName,string? ContentType,string? Description,DocumentMetadataStatus Status,string PersistenceMode,bool ProductiveCrudEnabled,bool BinaryStorageEnabled,bool PortalContentRuntimeEnabled);
public sealed record DocumentMetadataApplicationResult(string? DocumentId,string Operation,bool Allowed,bool Changed,string ErrorCode,string Message,DocumentMetadataStatus? Status,DocumentMetadataApplicationDocument? Document){public bool Success=>Allowed&&ErrorCode=="None";}
