using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryCaseFoundationStore : ICaseFoundationStore
{
    private readonly List<CaseFoundationRecord> cases =
    [
        new(
            "55555555-5555-5555-5555-555555555555",
            "11111111-1111-1111-1111-111111111111",
            "Synthetic billing case",
            "Synthetic customer reported a billing mismatch for foundation validation.",
            CasePriority.Medium,
            CaseStatus.Open)
    ];

    private readonly object sync = new();

    public Task<IReadOnlyCollection<CaseFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult<IReadOnlyCollection<CaseFoundationRecord>>(cases.ToArray());
        }
    }

    public Task<CaseFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult(cases.FirstOrDefault(item => string.Equals(item.Id, id, StringComparison.OrdinalIgnoreCase)));
        }
    }

    public Task<CaseFoundationRecord> SaveAsync(CaseFoundationRecord item, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            cases.RemoveAll(existing => string.Equals(existing.Id, item.Id, StringComparison.OrdinalIgnoreCase));
            cases.Add(item);
        }

        return Task.FromResult(item);
    }
}
