namespace CRM.Application.Foundation;

public sealed record CrmCommonDbProbeActivationGateContract(
    string Gate,
    string Owner,
    bool Required,
    bool Approved,
    string RequiredEvidence);

public sealed record CrmCommonDbProbeDependencyContract(
    string Dependency,
    bool Required,
    bool Available,
    string Status);

public sealed record CrmCommonDbProbeRollbackContract(
    string Requirement,
    bool Required,
    string Trigger);

public sealed record CrmCommonDbProbeBlockedItemContract(
    string Item,
    string Reason);

public sealed record CrmCommonDbProbeOptionalActivationStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool CommonDbProbeOptionalActivationExists,
    bool CommonDbProbeActivationApproved,
    bool CommonDbProbeEnabled,
    bool CommonDbConnectionAttempted,
    bool SecretProviderRuntimeRequired,
    bool SecretProviderRuntimeConnected,
    bool SecretReadsRequiredBeforeActivation,
    bool SecretReadsEnabled,
    bool RealDatabaseConfigured,
    bool ConnectionStringsConfigured,
    bool EfRuntimeEnabled,
    bool MigrationsCreated,
    bool DurablePersistenceEnabled,
    bool ApiRequiresDatabase,
    bool NonProductionOnly,
    bool SyntheticDataRequired,
    bool RollbackRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmCommonDbProbeActivationGateContract> ActivationGates,
    IReadOnlyCollection<CrmCommonDbProbeDependencyContract> Dependencies,
    IReadOnlyCollection<CrmCommonDbProbeRollbackContract> RollbackRequirements,
    IReadOnlyCollection<CrmCommonDbProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
