using CRM.Application.Persistence;

namespace CRM.Application.Ports.Persistence;

/// <summary>NonProductionPersistenceSeam: account preview storage contract only; no durable database or productive CRUD.</summary>
public interface IAccountFoundationStore
{
    Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default);

    Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default);

    Task ClearPreviewAsync(CancellationToken cancellationToken = default);

    Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default);
}
