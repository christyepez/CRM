using CRM.Application.Ports.Persistence;
using CRM.Domain.DocumentMetadata;
namespace CRM.Infrastructure.Persistence.Foundation;
public sealed class InMemoryDocumentMetadataFoundationStore:IDocumentMetadataFoundationStore
{
 private readonly List<DocumentMetadataFoundationRecord> items=[new("99999999-9999-9999-9999-999999999999",DocumentRelatedEntityType.Contact,"11111111-1111-1111-1111-111111111111","portal-content-synthetic-001","synthetic-foundation.pdf","application/pdf","Synthetic CRM document metadata reference.",DocumentMetadataStatus.Active)];
 private readonly object sync=new();
 public Task<IReadOnlyCollection<DocumentMetadataFoundationRecord>> GetAllAsync(CancellationToken c=default){c.ThrowIfCancellationRequested();lock(sync)return Task.FromResult<IReadOnlyCollection<DocumentMetadataFoundationRecord>>(items.ToArray());}
 public Task<DocumentMetadataFoundationRecord?> GetByIdAsync(string id,CancellationToken c=default){c.ThrowIfCancellationRequested();lock(sync)return Task.FromResult(items.FirstOrDefault(x=>string.Equals(x.Id,id,StringComparison.OrdinalIgnoreCase)));}
 public Task<DocumentMetadataFoundationRecord> SaveAsync(DocumentMetadataFoundationRecord item,CancellationToken c=default){c.ThrowIfCancellationRequested();lock(sync){items.RemoveAll(x=>string.Equals(x.Id,item.Id,StringComparison.OrdinalIgnoreCase));items.Add(item);}return Task.FromResult(item);}
}
