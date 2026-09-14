using CRM.Application.DocumentMetadata;
using CRM.Domain.DocumentMetadata;
namespace CRM.Api.Foundation;
public sealed record FoundationDocumentCreateRequest(DocumentRelatedEntityType RelatedEntityType,string? RelatedEntityId,string? FileReferenceId,string? FileName,string? ContentType,string? Description);
public sealed record FoundationDocumentUpdateRequest(DocumentRelatedEntityType RelatedEntityType,string? RelatedEntityId,string? FileReferenceId,string? FileName,string? ContentType,string? Description);
public sealed record DocumentMetadataApiResponse(string? Id,string Operation,bool Allowed,bool Changed,string ErrorCode,string Message,DocumentMetadataStatus? Status,DocumentMetadataApplicationDocument? Document,bool FoundationMode,bool ProductiveCrudEnabled,bool BinaryStorageEnabled,bool PortalContentRuntimeEnabled,string Warning)
{
 public static DocumentMetadataApiResponse From(DocumentMetadataApplicationResult r)=>new(r.DocumentId,r.Operation,r.Allowed,r.Changed,r.ErrorCode,r.Message,r.Status,r.Document,true,false,false,false,"Foundation CRM document metadata API only; binary content remains owned by Portal Content/File API.");
 public static int ToStatusCode(DocumentMetadataApplicationResult r)=>r.ErrorCode switch{nameof(DocumentMetadataErrorCode.None)=>StatusCodes.Status200OK,nameof(DocumentMetadataErrorCode.DocumentNotFound)=>StatusCodes.Status404NotFound,nameof(DocumentMetadataErrorCode.ArchivedDocumentCannotBeModified)=>StatusCodes.Status409Conflict,nameof(DocumentMetadataErrorCode.InvalidStatus)=>StatusCodes.Status409Conflict,_=>StatusCodes.Status400BadRequest};
 public static DocumentMetadataCreateRequest ToApplication(FoundationDocumentCreateRequest r)=>new(r.RelatedEntityType,r.RelatedEntityId,r.FileReferenceId,r.FileName,r.ContentType,r.Description);
 public static DocumentMetadataUpdateRequest ToApplication(FoundationDocumentUpdateRequest r)=>new(r.RelatedEntityType,r.RelatedEntityId,r.FileReferenceId,r.FileName,r.ContentType,r.Description);
}
