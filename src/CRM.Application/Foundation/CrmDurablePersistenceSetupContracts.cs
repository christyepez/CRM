namespace CRM.Application.Foundation;

public sealed record CrmDurablePersistenceCapabilityContract(
    string Capability,
    string Status,
    string Decision,
    string RequiredBeforeNextGate);

public sealed record CrmDatabaseTargetContract(
    string Ownership,
    string ContainerStrategy,
    string LogicalDatabaseStrategy,
    string CrossDomainRule);

public sealed record CrmMigrationStrategyContract(
    string Status,
    bool RollbackRequired,
    bool BackupRestoreRequired,
    bool RealMigrationsCreated);

public sealed record CrmSecretStrategyContract(
    string Status,
    bool RepoSecretsAllowed,
    string FutureProvider,
    string Rule);

public sealed record CrmDurablePersistenceSetupStatusResponse(
    string Module,
    string Status,
    string DurablePersistenceMode,
    bool RealDatabaseConfigured,
    bool EfRuntimeEnabled,
    bool DbContextConfigured,
    bool MigrationsCreated,
    bool ConnectionStringsConfigured,
    bool SqlServerOwnedByCrm,
    string SecretStrategy,
    string MigrationStrategy,
    string ProductiveActivation,
    string NextGate,
    string Warning,
    bool FoundationMode,
    CrmDatabaseTargetContract DatabaseTarget,
    CrmMigrationStrategyContract Migration,
    CrmSecretStrategyContract Secrets,
    IReadOnlyCollection<CrmDurablePersistenceCapabilityContract> Capabilities,
    IReadOnlyCollection<string> RequiredGates,
    IReadOnlyCollection<string> Risks);
