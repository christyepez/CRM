using System.Text.Json.Serialization;

namespace CRM.Application.Portal;

public sealed record CrmPortalAuthRuntimeProbeCapabilityContract(
    string Capability,
    string Status,
    bool Ready,
    string Evidence);

public sealed record CrmPortalAuthRuntimeProbeSafetyGateContract(
    string Gate,
    string Decision,
    bool Approved,
    string RequiredBeforeEnablement);

public sealed record CrmPortalAuthRuntimeProbeBlockedItemContract(
    string Item,
    string Status,
    string Reason);

public sealed record CrmPortalAuthRuntimeProbeStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalAuthRuntimeProbeExists,
    bool PortalAuthRuntimeProbeEnabled,
    bool PortalRuntimeConnected,
    bool AuthRuntimeEnabled,
    bool ProductiveAuthorizationEnabled,
    [property: JsonPropertyName("tokenReadAttemptedByRuntime")] bool CredentialReadAttemptedByRuntime,
    bool PortalHttpAttemptedByRuntime,
    bool LoginImplementedByCrm,
    bool IdentityImplementedByCrm,
    bool PermissionsPersistedInCrm,
    bool FoundationSimulationActive,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmPortalAuthRuntimeProbeCapabilityContract> Capabilities,
    IReadOnlyCollection<CrmPortalAuthRuntimeProbeSafetyGateContract> SafetyGates,
    IReadOnlyCollection<CrmPortalAuthRuntimeProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
