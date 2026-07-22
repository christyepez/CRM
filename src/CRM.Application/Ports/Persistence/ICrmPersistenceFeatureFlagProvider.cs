using CRM.Application.Persistence;

namespace CRM.Application.Ports.Persistence;

/// <summary>NonProductionPersistenceSeam: exposes safe hardcoded feature flags until productive persistence is approved.</summary>
public interface ICrmPersistenceFeatureFlagProvider
{
    CrmPersistenceFeatureFlagsContract GetFeatureFlags();
}
