namespace CRM.Application.Foundation;

public sealed record CrmCommonDbRuntimeProbeCapabilityContract(
    string Capability,
    string Status,
    bool Ready,
    string Evidence);

public sealed record CrmCommonDbRuntimeProbeSafetyGateContract(
    string Gate,
    string Decision,
    bool Approved,
    string RequiredBeforeEnablement);

public sealed record CrmCommonDbRuntimeProbeBlockedItemContract(
    string Item,
    string Status,
    string Reason);

public sealed record CrmCommonDbRuntimeProbeStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool CommonDbRuntimeProbeExists,
    bool CommonDbRuntimeProbeEnabled,
    bool RealDatabaseConfigured,
    bool ConnectionStringsConfigured,
    bool SecretProviderRuntimeConnected,
    bool DbConnectionAttemptedByRuntime,
    bool SqlServerOwnedByCrm,
    bool EfRuntimeEnabled,
    bool DbContextRuntimeActive,
    bool MigrationsCreated,
    bool DurablePersistenceEnabled,
    bool ProductiveCrudEnabled,
    bool ApiRequiresDatabase,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmCommonDbRuntimeProbeCapabilityContract> Capabilities,
    IReadOnlyCollection<CrmCommonDbRuntimeProbeSafetyGateContract> SafetyGates,
    IReadOnlyCollection<CrmCommonDbRuntimeProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
