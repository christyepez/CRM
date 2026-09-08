using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryOpportunityFoundationStore : IOpportunityFoundationStore
{
    private readonly List<OpportunityFoundationRecord> items=[];
    private readonly object sync=new();
    public InMemoryOpportunityFoundationStore(){ items.Add(new("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa","Synthetic Account",25000m,"USD",25,"bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb","cccccccc-cccc-cccc-cccc-cccccccccccc",OpportunityStatus.Open)); }
    public Task<IReadOnlyCollection<OpportunityFoundationRecord>> GetAllAsync(CancellationToken ct=default){ct.ThrowIfCancellationRequested(); lock(sync)return Task.FromResult<IReadOnlyCollection<OpportunityFoundationRecord>>(items.ToArray());}
    public Task<OpportunityFoundationRecord?> GetByIdAsync(string id,CancellationToken ct=default){ct.ThrowIfCancellationRequested(); lock(sync)return Task.FromResult(items.FirstOrDefault(x=>string.Equals(x.Id,id,StringComparison.OrdinalIgnoreCase)));}
    public Task<OpportunityFoundationRecord> SaveAsync(OpportunityFoundationRecord opportunity,CancellationToken ct=default){ct.ThrowIfCancellationRequested(); lock(sync){items.RemoveAll(x=>string.Equals(x.Id,opportunity.Id,StringComparison.OrdinalIgnoreCase));items.Add(opportunity);return Task.FromResult(opportunity);}}
}
