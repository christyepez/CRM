namespace CRM.Application.Foundation;

public sealed record CrmEfPrototypeCapabilityContract(
    string Capability,
    string Status,
    bool Enabled,
    string Guardrail);

public sealed record CrmDbContextPrototypeContract(
    string Name,
    bool Exists,
    bool RuntimeActive,
    bool InheritsRealDbContext,
    string Decision);

public sealed record CrmEfRuntimeGateContract(
    string Gate,
    string Status,
    bool Ready,
    string RequiredBeforeActivation);

public sealed record CrmEfPrototypeStatusResponse(
    string Module,
    string Status,
    bool EfPrototypeExists,
    bool EfRuntimeEnabled,
    bool DbContextRuntimeActive,
    bool MigrationsCreated,
    bool RealDatabaseConfigured,
    bool ConnectionStringsConfigured,
    bool ProviderConfigured,
    bool UseSqlServerConfigured,
    bool FoundationStoresRemainActive,
    bool ProductiveCrudEnabled,
    string NextGate,
    string Warning,
    bool FoundationMode,
    IReadOnlyCollection<string> FeatureFlags,
    CrmDbContextPrototypeContract Prototype,
    IReadOnlyCollection<CrmEfPrototypeCapabilityContract> Capabilities,
    IReadOnlyCollection<CrmEfRuntimeGateContract> Gates,
    IReadOnlyCollection<string> Risks);
