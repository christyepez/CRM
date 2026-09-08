using CRM.Domain.Enums;

namespace CRM.Application.Ports.Persistence;

public sealed record OpportunityFoundationRecord(string Id,string AccountName,decimal ExpectedValue,string Currency,int Probability,string PipelineId,string StageId,OpportunityStatus Status,string? LeadId=null,string? ContactId=null,string? AccountId=null,string? ActivityId=null);

public interface IOpportunityFoundationStore
{
    Task<IReadOnlyCollection<OpportunityFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<OpportunityFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
    Task<OpportunityFoundationRecord> SaveAsync(OpportunityFoundationRecord opportunity,CancellationToken cancellationToken=default);
}
