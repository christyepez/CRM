using CRM.Application.Ports.Persistence; using CRM.Domain.TagManagement;
namespace CRM.Infrastructure.Persistence.Foundation;
public sealed class InMemoryTagFoundationStore:ITagFoundationStore
{
 readonly List<TagFoundationRecord> items=[new("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa","Priority","Synthetic foundation tag",TagStatus.Active,TagRelatedEntityType.Contact,"11111111-1111-1111-1111-111111111111")]; readonly object sync=new();
 public Task<IReadOnlyCollection<TagFoundationRecord>> GetAllAsync(CancellationToken ct=default){ct.ThrowIfCancellationRequested();lock(sync)return Task.FromResult<IReadOnlyCollection<TagFoundationRecord>>(items.ToArray());}
 public Task<TagFoundationRecord?> GetByIdAsync(string id,CancellationToken ct=default){ct.ThrowIfCancellationRequested();lock(sync)return Task.FromResult(items.FirstOrDefault(x=>string.Equals(x.Id,id,StringComparison.OrdinalIgnoreCase)));}
 public Task<TagFoundationRecord> SaveAsync(TagFoundationRecord item,CancellationToken ct=default){ct.ThrowIfCancellationRequested();lock(sync){items.RemoveAll(x=>string.Equals(x.Id,item.Id,StringComparison.OrdinalIgnoreCase));items.Add(item);}return Task.FromResult(item);}
}
