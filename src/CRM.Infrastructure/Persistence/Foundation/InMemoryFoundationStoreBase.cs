using CRM.Application.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public abstract class InMemoryFoundationStoreBase
{
    private readonly List<CrmFoundationPreviewItemContract> previews = [];
    private readonly object sync = new();

    protected InMemoryFoundationStoreBase(string storeName)
    {
        StoreName = storeName;
    }

    protected string StoreName { get; }

    public Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult<IReadOnlyCollection<CrmFoundationPreviewItemContract>>(previews.ToArray());
        }
    }

    public Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult(previews.FirstOrDefault(item => string.Equals(item.Id, id, StringComparison.OrdinalIgnoreCase)));
        }
    }

    public Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            previews.RemoveAll(item => item.Id == preview.Id);
            previews.Add(preview);
        }

        return Task.FromResult(preview);
    }

    public Task ClearPreviewAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            previews.Clear();
        }

        return Task.CompletedTask;
    }

    public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult(new CrmFoundationStoreStatusContract(
                StoreName,
                true,
                true,
                false,
                previews.Count,
                "NonProductionSeam",
                "NonProduction",
                CrmPersistenceSeamStatusService.WarningText));
        }
    }

    protected void AddSeed(string id, string entity, string displayName, string status, IReadOnlyDictionary<string, string> metadata)
    {
        previews.Add(new CrmFoundationPreviewItemContract(
            id,
            entity,
            displayName,
            status,
            new DateTimeOffset(2026, 7, 22, 0, 0, 0, TimeSpan.Zero),
            metadata));
    }
}
