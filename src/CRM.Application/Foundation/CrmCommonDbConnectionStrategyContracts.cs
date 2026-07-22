namespace CRM.Application.Foundation;

public sealed record CrmLogicalDatabaseTargetContract(
    string LogicalDatabaseName,
    bool LogicalDatabaseNameIsPlaceholder,
    string Ownership,
    string CommonDatabaseRule,
    string CrossDomainBoundary);

public sealed record CrmSecretProviderStrategyContract(
    string Status,
    bool Configured,
    bool RuntimeConnected,
    string FutureSources,
    string ProhibitedSources);

public sealed record CrmConnectionStringPolicyContract(
    string Policy,
    bool RealValuesAllowedInRepository,
    bool PasswordsAllowedInRepository,
    string PlaceholderRule);

public sealed record CrmDbRuntimeReadinessGateContract(
    string Gate,
    string Status,
    bool Ready,
    string RequiredBeforeNextGate);

public sealed record CrmCommonDbConnectionStrategyStatusResponse(
    string Module,
    string Status,
    bool RealDatabaseConfigured,
    bool ConnectionStringsConfigured,
    bool SecretProviderConfigured,
    bool SecretProviderRuntimeConnected,
    bool SqlServerOwnedByCrm,
    bool EfRuntimeEnabled,
    bool DbContextConfigured,
    bool MigrationsCreated,
    string LogicalDatabaseName,
    bool LogicalDatabaseNameIsPlaceholder,
    string SecretStrategy,
    string ConnectionStringPolicy,
    string NextGate,
    string Warning,
    bool FoundationMode,
    CrmLogicalDatabaseTargetContract LogicalDatabaseTarget,
    CrmSecretProviderStrategyContract SecretProvider,
    CrmConnectionStringPolicyContract ConnectionPolicy,
    IReadOnlyCollection<CrmDbRuntimeReadinessGateContract> ReadinessGates,
    IReadOnlyCollection<string> Risks);
