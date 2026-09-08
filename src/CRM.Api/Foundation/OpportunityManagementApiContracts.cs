using CRM.Application.OpportunityManagement;
using CRM.Domain.OpportunityManagement;
using CRM.Domain.Enums;

namespace CRM.Api.Foundation;

public sealed record FoundationOpportunityStageRequest(string StageId,string Name,int Order);
public sealed record FoundationOpportunityCreateRequest(string? AccountName,decimal ExpectedValue,string? Currency,int Probability,string? PipelineId,string? StageId,IReadOnlyCollection<FoundationOpportunityStageRequest>? Stages,string? LeadId=null,string? ContactId=null,string? AccountId=null,string? ActivityId=null);
public sealed record FoundationOpportunityUpdateRequest(string? AccountName,decimal ExpectedValue,string? Currency,int Probability,string? PipelineId,string? StageId,IReadOnlyCollection<FoundationOpportunityStageRequest>? Stages,string? LeadId=null,string? ContactId=null,string? AccountId=null,string? ActivityId=null);
public sealed record FoundationOpportunityProgressRequest(string? StageId,IReadOnlyCollection<FoundationOpportunityStageRequest>? Stages);

public sealed record OpportunityManagementApiResponse(string? Id,OpportunityPipelineOperation Operation,bool Allowed,bool Changed,string ErrorCode,string Message,OpportunityStatus? Status,OpportunityManagementApplicationOpportunity? Opportunity,bool FoundationMode,string PersistenceMode,bool DurablePersistence,bool ProductiveCrudEnabled,bool DatabaseConfigured,bool PortalRuntimeEnabled,bool CommonDbRuntimeEnabled,string Warning)
{
    private static IReadOnlyCollection<OpportunityPipelineStageDefinition> Map(IReadOnlyCollection<FoundationOpportunityStageRequest>? stages)=>(stages??Array.Empty<FoundationOpportunityStageRequest>()).Select(x=>new OpportunityPipelineStageDefinition(x.StageId,x.Name,x.Order)).ToArray();
    public static OpportunityManagementCreateRequest ToApplicationRequest(FoundationOpportunityCreateRequest r)=>new(r.AccountName,r.ExpectedValue,r.Currency,r.Probability,r.PipelineId,r.StageId,Map(r.Stages),r.LeadId,r.ContactId,r.AccountId,r.ActivityId);
    public static OpportunityManagementUpdateRequest ToApplicationRequest(FoundationOpportunityUpdateRequest r)=>new(r.AccountName,r.ExpectedValue,r.Currency,r.Probability,r.PipelineId,r.StageId,Map(r.Stages),r.LeadId,r.ContactId,r.AccountId,r.ActivityId);
    public static OpportunityManagementProgressRequest ToApplicationRequest(FoundationOpportunityProgressRequest r)=>new(r.StageId,Map(r.Stages));
    public static OpportunityManagementApiResponse From(OpportunityManagementApplicationResult r)=>new(r.OpportunityId,r.Operation,r.Allowed,r.Changed,r.ErrorCode,r.Message,r.Status,r.Opportunity,true,r.Opportunity?.PersistenceMode??"NonProductionSeam",false,false,false,false,false,"Foundation Opportunity API only; productive route remains locked");
    public static int ToStatusCode(OpportunityManagementApplicationResult r)=>r.ErrorCode switch { nameof(OpportunityPipelineErrorCode.None)=>StatusCodes.Status200OK, nameof(OpportunityPipelineErrorCode.OpportunityNotFound)=>StatusCodes.Status404NotFound, _=>StatusCodes.Status400BadRequest };
}
