namespace CRM.Application.Persistence;

public sealed record LeadPersistenceModelContract(
    string Aggregate,
    string PersistenceMode,
    bool DatabaseConfigured,
    bool MigrationReady,
    string RuntimeMode,
    string Warning);

public sealed record AccountPersistenceModelContract(
    string Aggregate,
    string PersistenceMode,
    bool DatabaseConfigured,
    bool MigrationReady,
    string RuntimeMode,
    string Warning);

public sealed record ContactPersistenceModelContract(
    string Aggregate,
    string PersistenceMode,
    bool DatabaseConfigured,
    bool MigrationReady,
    string RuntimeMode,
    string Warning);

public sealed record CrmPersistenceGateStatusContract(
    string Gate,
    string Status,
    string RequiredDecision);

public sealed record CrmPersistenceReadinessResponse(
    string Module,
    string Status,
    string PersistenceMode,
    bool DatabaseConfigured,
    bool MigrationReady,
    bool DbContextConfigured,
    bool SqlServerOwnedByCrm,
    string RuntimeMode,
    string NextGate,
    string Warning,
    bool FoundationMode,
    IReadOnlyCollection<string> FirstAggregates,
    IReadOnlyCollection<string> DeferredAggregates,
    IReadOnlyCollection<CrmPersistenceGateStatusContract> Gates);
