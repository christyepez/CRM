using System.Text.Json.Serialization;

namespace CRM.Application.Portal;

public sealed record CrmPortalTenantSimulationContract(
    string TenantId,
    string DisplayName,
    string SimulationMode,
    bool PortalRuntimeConnected,
    string Warning);

public sealed record CrmPortalUserSimulationContract(
    string UserId,
    string DisplayName,
    string RuntimeMode,
    string SimulationMode,
    bool PortalRuntimeConnected,
    [property: JsonPropertyName("tokenStorageEnabled")] bool CredentialStorageEnabled,
    CrmPortalTenantSimulationContract Tenant,
    IReadOnlyCollection<string> Permissions,
    string Warning);

public sealed record CrmPortalPermissionCheckContract(
    string Permission,
    bool Allowed,
    string Reason,
    string SimulationMode,
    string AuthOwnedBy,
    bool CrmOwnsAuth,
    [property: JsonPropertyName("tokenStorageEnabled")] bool CredentialStorageEnabled,
    string Warning);

public sealed record CrmPortalPermissionCheckRequest(
    string RequiredPermission);

public sealed record CrmPortalAuthorizationScenarioContract(
    string Scenario,
    string Description,
    string SampleUser,
    IReadOnlyCollection<string> GrantedPermissions,
    IReadOnlyCollection<string> DeniedPermissions,
    string ExpectedOutcome,
    string SimulationMode,
    string Warning);

public sealed record CrmPortalAuthorizationSimulationStatusResponse(
    string Module,
    string Status,
    string SimulationMode,
    bool PortalRuntimeConnected,
    string AuthOwnedBy,
    bool CrmOwnsAuth,
    [property: JsonPropertyName("tokenStorageEnabled")] bool CredentialStorageEnabled,
    bool ProductiveAuthorizationEnabled,
    string RuntimeMode,
    string NextGate,
    string Warning,
    bool FoundationMode,
    CrmPortalUserSimulationContract SampleUser,
    IReadOnlyCollection<CrmPortalAuthorizationScenarioContract> Scenarios,
    IReadOnlyCollection<string> Permissions);
