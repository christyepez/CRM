using CRM.Application.Persistence;

namespace CRM.Application.Ports.Persistence;

public interface ICampaignFoundationStore
{
    Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default);
    Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default);
    Task ClearPreviewAsync(CancellationToken cancellationToken = default);
    Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default);
}