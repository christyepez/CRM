using CRM.Application.TagManagement;
using CRM.Domain.TagManagement;
namespace CRM.Api.Foundation;
public sealed record FoundationTagCreateRequest(string? Name,string? Description,TagRelatedEntityType? RelatedEntityType,string? RelatedEntityId);
public sealed record FoundationTagUpdateRequest(string? Name,string? Description,TagRelatedEntityType? RelatedEntityType,string? RelatedEntityId);
public sealed record TagManagementApiResponse(string? Id,string Operation,bool Allowed,bool Changed,string ErrorCode,string Message,TagStatus? Status,TagApplicationItem? Tag,bool FoundationMode,bool ProductiveCrudEnabled,bool PortalIdentityRuntimeEnabled,bool CrossEntityMutationEnabled,string Warning)
{
 public static TagManagementApiResponse From(TagApplicationResult r)=>new(r.TagId,r.Operation,r.Allowed,r.Changed,r.ErrorCode,r.Message,r.Status,r.Tag,true,false,false,false,"Foundation CRM Tag API only; Portal identity/config runtime remains disabled.");
 public static int ToStatusCode(TagApplicationResult r)=>r.ErrorCode switch{nameof(TagErrorCode.None)=>StatusCodes.Status200OK,nameof(TagErrorCode.TagNotFound)=>StatusCodes.Status404NotFound,nameof(TagErrorCode.ArchivedTagCannotBeModified)=>StatusCodes.Status409Conflict,nameof(TagErrorCode.InvalidStatus)=>StatusCodes.Status409Conflict,_=>StatusCodes.Status400BadRequest};
 public static TagCreateRequest ToApplication(FoundationTagCreateRequest r)=>new(r.Name,r.Description,r.RelatedEntityType,r.RelatedEntityId);
 public static TagUpdateRequest ToApplication(FoundationTagUpdateRequest r)=>new(r.Name,r.Description,r.RelatedEntityType,r.RelatedEntityId);
}
