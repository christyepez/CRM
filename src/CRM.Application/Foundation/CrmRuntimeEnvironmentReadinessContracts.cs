namespace CRM.Application.Foundation;

public sealed record CrmRuntimeToolingCheckContract(
    string Tool,
    string Expected,
    string Status,
    string Guidance);

public sealed record CrmRuntimeHealthCheckContract(
    string Endpoint,
    string Expected,
    string Status,
    bool RequiredForLocalReadiness);

public sealed record CrmRuntimeBlockedItemContract(
    string Item,
    string Status,
    string Reason);

public sealed record CrmRuntimeEnvironmentReadinessStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool DockerComposeExpected,
    int CrmApiPort,
    bool SqlServerOwnedByCrm,
    bool NodePathRequiredForFrontendVerifier,
    bool ProductiveRoutesActive,
    bool DeleteEndpointsEnabled,
    bool RealDatabaseConfigured,
    bool AuthRuntimeEnabled,
    bool PortalRuntimeConnected,
    string ProductizationStatus,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmRuntimeToolingCheckContract> ToolingChecks,
    IReadOnlyCollection<CrmRuntimeHealthCheckContract> HealthChecks,
    IReadOnlyCollection<CrmRuntimeBlockedItemContract> BlockedItems);
