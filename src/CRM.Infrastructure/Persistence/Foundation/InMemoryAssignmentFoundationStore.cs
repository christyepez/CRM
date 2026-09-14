using CRM.Application.Ports.Persistence;
using CRM.Domain.AssignmentManagement;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryAssignmentFoundationStore : IAssignmentFoundationStore
{
    private readonly List<AssignmentFoundationRecord> items =
    [
        new("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", AssignmentRelatedEntityType.Contact,
            "11111111-1111-1111-1111-111111111111", "portal-user-ref-foundation",
            "Synthetic owner reference", AssignmentStatus.Active)
    ];
    private readonly object sync = new();

    public Task<IReadOnlyCollection<AssignmentFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync) return Task.FromResult<IReadOnlyCollection<AssignmentFoundationRecord>>(items.ToArray());
    }

    public Task<AssignmentFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync) return Task.FromResult(items.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<AssignmentFoundationRecord> SaveAsync(AssignmentFoundationRecord item, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            items.RemoveAll(x => string.Equals(x.Id, item.Id, StringComparison.OrdinalIgnoreCase));
            items.Add(item);
        }
        return Task.FromResult(item);
    }
}
