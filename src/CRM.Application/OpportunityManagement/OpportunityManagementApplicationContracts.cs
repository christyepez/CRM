using CRM.Domain.Enums;
using CRM.Domain.OpportunityManagement;

namespace CRM.Application.OpportunityManagement;

public sealed record OpportunityManagementCreateRequest(string? AccountName, decimal ExpectedValue, string? Currency, int Probability, string? PipelineId, string? StageId, IReadOnlyCollection<OpportunityPipelineStageDefinition> Stages, string? LeadId=null, string? ContactId=null, string? AccountId=null, string? ActivityId=null);
public sealed record OpportunityManagementUpdateRequest(string? AccountName, decimal ExpectedValue, string? Currency, int Probability, string? PipelineId, string? StageId, IReadOnlyCollection<OpportunityPipelineStageDefinition> Stages, string? LeadId=null, string? ContactId=null, string? AccountId=null, string? ActivityId=null);
public sealed record OpportunityManagementProgressRequest(string? StageId, IReadOnlyCollection<OpportunityPipelineStageDefinition> Stages);
public sealed record OpportunityManagementApplicationOpportunity(string Id,string AccountName,decimal ExpectedValue,string Currency,int Probability,string PipelineId,string StageId,OpportunityStatus Status,string? LeadId,string? ContactId,string? AccountId,string? ActivityId,string PersistenceMode,bool ProductiveCrudEnabled);
public sealed record OpportunityManagementApplicationResult(string? OpportunityId,OpportunityPipelineOperation Operation,bool Allowed,bool Changed,string ErrorCode,string Message,OpportunityStatus? Status,OpportunityManagementApplicationOpportunity? Opportunity){ public bool Success => Allowed && ErrorCode == OpportunityPipelineErrorCode.None.ToString(); }
