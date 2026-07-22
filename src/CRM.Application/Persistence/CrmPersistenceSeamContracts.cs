namespace CRM.Application.Persistence;

public sealed record CrmFoundationPreviewItemContract(
    string Id,
    string Entity,
    string DisplayName,
    string Status,
    DateTimeOffset CreatedAtUtc,
    IReadOnlyDictionary<string, string> Metadata);

public sealed record CrmFoundationStoreStatusContract(
    string Store,
    bool FoundationMode,
    bool FoundationStoreEnabled,
    bool DurablePersistence,
    int PreviewCount,
    string PersistenceMode,
    string RuntimeMode,
    string Warning);

public sealed record CrmPersistenceFeatureFlagsContract(
    bool FoundationMode,
    bool PersistenceSeamEnabled,
    bool PersistenceEnabled,
    bool ProductiveCrudEnabled,
    bool DurablePersistenceEnabled,
    string RuntimeMode,
    string Warning);

public sealed record CrmPersistenceSeamStatusResponse(
    string Module,
    string Status,
    string PersistenceMode,
    bool FoundationStoreEnabled,
    bool DatabaseConfigured,
    bool DbContextConfigured,
    bool MigrationReady,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    string RuntimeMode,
    string NextGate,
    string Warning,
    bool FoundationMode,
    CrmPersistenceFeatureFlagsContract FeatureFlags,
    IReadOnlyCollection<CrmFoundationStoreStatusContract> Stores);
