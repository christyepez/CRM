using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class StaticCrmPersistenceFeatureFlagProvider : ICrmPersistenceFeatureFlagProvider
{
    public CrmPersistenceFeatureFlagsContract GetFeatureFlags() =>
        new(
            FoundationMode: true,
            PersistenceSeamEnabled: true,
            PersistenceEnabled: false,
            ProductiveCrudEnabled: false,
            DurablePersistenceEnabled: false,
            RuntimeMode: "NonProduction",
            Warning: CrmPersistenceSeamStatusService.WarningText);
}
