using CRM.Application.Ports.Persistence;
using CRM.Domain.InteractionManagement;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryInteractionFoundationStore : IInteractionFoundationStore
{
    private readonly List<InteractionFoundationRecord> interactions =
    [
        new(
            "66666666-6666-6666-6666-666666666666",
            InteractionRelatedEntityType.Contact,
            "22222222-2222-2222-2222-222222222222",
            InteractionChannel.Phone,
            InteractionDirection.Outbound,
            "Synthetic follow-up call",
            "Synthetic contact confirmed a follow-up discussion for foundation validation.",
            new DateTimeOffset(2026, 1, 15, 15, 0, 0, TimeSpan.Zero),
            InteractionStatus.Recorded)
    ];

    private readonly object sync = new();

    public Task<IReadOnlyCollection<InteractionFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync) return Task.FromResult<IReadOnlyCollection<InteractionFoundationRecord>>(interactions.ToArray());
    }

    public Task<InteractionFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync) return Task.FromResult(interactions.FirstOrDefault(item => string.Equals(item.Id, id, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<InteractionFoundationRecord> SaveAsync(InteractionFoundationRecord item, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            interactions.RemoveAll(existing => string.Equals(existing.Id, item.Id, StringComparison.OrdinalIgnoreCase));
            interactions.Add(item);
        }
        return Task.FromResult(item);
    }
}
