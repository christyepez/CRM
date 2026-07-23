using System.Text.Json.Serialization;

namespace CRM.Application.Portal;

public sealed record CrmPortalAuthCapabilityContract(
    string Capability,
    string Scope,
    string Status,
    bool PersistedInCrm,
    string Owner);

public sealed record CrmPortalAuthContextMappingContract(
    string Source,
    string Target,
    string Status,
    bool RuntimeMapped,
    string Notes);

public sealed record CrmPortalAuthRuntimeGateContract(
    string Gate,
    string Status,
    bool Ready,
    string RequiredBeforeActivation);

public sealed record CrmPortalAuthNoGoPolicyContract(
    string Policy,
    bool Allowed,
    string Reason);

public sealed record CrmPortalAuthRuntimeContractStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalRuntimeConnected,
    bool AuthRuntimeEnabled,
    bool CrmOwnsAuth,
    string AuthOwnedBy,
    [property: JsonPropertyName("tokenStorageEnabled")] bool CredentialStorageEnabled,
    bool LoginImplementedByCrm,
    bool IdentityImplementedByCrm,
    bool PermissionsPersistedInCrm,
    bool FoundationSimulationActive,
    bool ProductiveAuthorizationEnabled,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmPortalAuthCapabilityContract> Capabilities,
    IReadOnlyCollection<CrmPortalAuthContextMappingContract> ContextMappings,
    IReadOnlyCollection<CrmPortalAuthRuntimeGateContract> Gates,
    IReadOnlyCollection<CrmPortalAuthNoGoPolicyContract> NoGoPolicies,
    IReadOnlyCollection<string> Risks);
