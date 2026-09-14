using CRM.Domain.TagManagement;
namespace CRM.Application.TagManagement;
public sealed record TagCreateRequest(string? Name,string? Description,TagRelatedEntityType? RelatedEntityType=null,string? RelatedEntityId=null);
public sealed record TagUpdateRequest(string? Name,string? Description,TagRelatedEntityType? RelatedEntityType=null,string? RelatedEntityId=null);
public sealed record TagApplicationItem(string Id,string Name,string? Description,TagStatus Status,TagRelatedEntityType? RelatedEntityType,string? RelatedEntityId,string PersistenceMode,bool ProductiveCrudEnabled,bool PortalIdentityRuntimeEnabled);
public sealed record TagApplicationResult(string? TagId,string Operation,bool Allowed,bool Changed,string ErrorCode,string Message,TagStatus? Status,TagApplicationItem? Tag){public bool Success=>Allowed&&ErrorCode=="None";}
