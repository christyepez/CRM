using CRM.Application.AssignmentManagement;
using CRM.Domain.AssignmentManagement;
namespace CRM.Api.Foundation;
public sealed record FoundationAssignmentCreateRequest(AssignmentRelatedEntityType RelatedEntityType,string? RelatedEntityId,string? AssigneeReferenceId,string? AssignmentLabel);
public sealed record FoundationAssignmentUpdateRequest(AssignmentRelatedEntityType RelatedEntityType,string? RelatedEntityId,string? AssigneeReferenceId,string? AssignmentLabel);
public sealed record AssignmentManagementApiResponse(string? Id,string Operation,bool Allowed,bool Changed,string ErrorCode,string Message,AssignmentStatus? Status,AssignmentApplicationItem? Assignment,bool FoundationMode,bool ProductiveCrudEnabled,bool PortalIdentityRuntimeEnabled,bool CrossEntityMutationEnabled,string Warning)
{
 public static AssignmentManagementApiResponse From(AssignmentApplicationResult r)=>new(r.AssignmentId,r.Operation,r.Allowed,r.Changed,r.ErrorCode,r.Message,r.Status,r.Assignment,true,false,false,false,"Foundation CRM assignment reference API only; Portal identity remains external and unresolved.");
 public static int ToStatusCode(AssignmentApplicationResult r)=>r.ErrorCode switch{nameof(AssignmentErrorCode.None)=>StatusCodes.Status200OK,nameof(AssignmentErrorCode.AssignmentNotFound)=>StatusCodes.Status404NotFound,nameof(AssignmentErrorCode.ArchivedAssignmentCannotBeModified)=>StatusCodes.Status409Conflict,nameof(AssignmentErrorCode.InvalidStatus)=>StatusCodes.Status409Conflict,_=>StatusCodes.Status400BadRequest};
 public static AssignmentCreateRequest ToApplication(FoundationAssignmentCreateRequest r)=>new(r.RelatedEntityType,r.RelatedEntityId,r.AssigneeReferenceId,r.AssignmentLabel);
 public static AssignmentUpdateRequest ToApplication(FoundationAssignmentUpdateRequest r)=>new(r.RelatedEntityType,r.RelatedEntityId,r.AssigneeReferenceId,r.AssignmentLabel);
}