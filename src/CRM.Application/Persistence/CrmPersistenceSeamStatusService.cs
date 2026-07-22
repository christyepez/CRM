using CRM.Application.Ports.Persistence;

namespace CRM.Application.Persistence;

public sealed class CrmPersistenceSeamStatusService(
    ICrmPersistenceFeatureFlagProvider featureFlags,
    ILeadFoundationStore leadStore,
    IAccountFoundationStore accountStore,
    IContactFoundationStore contactStore)
{
    public const string WarningText = "Non-production persistence seam only; no database configured";

    public async Task<CrmPersistenceSeamStatusResponse> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        var flags = featureFlags.GetFeatureFlags();
        var stores = new[]
        {
            await leadStore.GetStatusAsync(cancellationToken),
            await accountStore.GetStatusAsync(cancellationToken),
            await contactStore.GetStatusAsync(cancellationToken)
        };

        return new CrmPersistenceSeamStatusResponse(
            "CRM",
            "PersistenceSeamActive",
            "NonProductionSeam",
            true,
            false,
            false,
            false,
            false,
            false,
            "NonProduction",
            "Sprint2P3PortalAuthorizationAdapterSimulation",
            WarningText,
            true,
            flags,
            stores);
    }

    public CrmPersistenceFeatureFlagsContract GetFeatureFlags() => featureFlags.GetFeatureFlags();

    public async Task<IReadOnlyCollection<CrmFoundationStoreStatusContract>> GetStoresStatusAsync(CancellationToken cancellationToken = default) =>
        [
            await leadStore.GetStatusAsync(cancellationToken),
            await accountStore.GetStatusAsync(cancellationToken),
            await contactStore.GetStatusAsync(cancellationToken)
        ];

    public async Task<CrmPersistenceSeamStatusResponse> ClearPreviewAsync(CancellationToken cancellationToken = default)
    {
        await leadStore.ClearPreviewAsync(cancellationToken);
        await accountStore.ClearPreviewAsync(cancellationToken);
        await contactStore.ClearPreviewAsync(cancellationToken);
        return await GetStatusAsync(cancellationToken);
    }
}
