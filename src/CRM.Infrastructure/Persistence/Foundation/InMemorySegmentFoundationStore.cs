using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemorySegmentFoundationStore : ISegmentFoundationStore
{
    private readonly List<SegmentFoundationRecord> segments =
    [
        new("bbbbbbbb-2222-2222-2222-222222222222", "Enterprise Customers", "High-value enterprise audience.", SegmentStatus.Draft)
    ];
    private readonly object sync = new();

    public Task<IReadOnlyCollection<SegmentFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock(sync) return Task.FromResult<IReadOnlyCollection<SegmentFoundationRecord>>(segments.ToArray());
    }

    public Task<SegmentFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock(sync) return Task.FromResult(segments.FirstOrDefault(x=>string.Equals(x.Id,id,StringComparison.OrdinalIgnoreCase)));
    }
    public Task<SegmentFoundationRecord> SaveAsync(SegmentFoundationRecord segment,CancellationToken cancellationToken=default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock(sync)
        {
            segments.RemoveAll(x=>string.Equals(x.Id,segment.Id,StringComparison.OrdinalIgnoreCase));
            segments.Add(segment);
        }
        return Task.FromResult(segment);
    }
}