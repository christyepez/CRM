namespace CRM.Application.Foundation;

public sealed record CrmPortalAuthProbeActivationGateContract(
    string Gate,
    string Owner,
    bool Required,
    bool Approved,
    string RequiredEvidence);

public sealed record CrmPortalAuthProbeDependencyContract(
    string Dependency,
    bool Required,
    bool Available,
    string Status);

public sealed record CrmPortalAuthProbeRollbackContract(
    string Requirement,
    bool Required,
    string Trigger);

public sealed record CrmPortalAuthProbeBlockedItemContract(
    string Item,
    string Reason);

public sealed record CrmPortalAuthProbeOptionalActivationStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalAuthProbeOptionalActivationExists,
    bool PortalAuthProbeActivationApproved,
    bool PortalAuthProbeEnabled,
    bool PortalAuthRuntimeConnected,
    bool PortalHttpAttempted,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool SecretProviderRuntimeRequired,
    bool SecretProviderRuntimeConnected,
    bool SecretReadsEnabled,
    bool LoginImplementedByCrm,
    bool IdentityImplementedByCrm,
    bool PermissionsPersistedInCrm,
    bool ProductiveAuthorizationEnabled,
    bool NonProductionOnly,
    bool RollbackRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmPortalAuthProbeActivationGateContract> ActivationGates,
    IReadOnlyCollection<CrmPortalAuthProbeDependencyContract> Dependencies,
    IReadOnlyCollection<CrmPortalAuthProbeRollbackContract> RollbackRequirements,
    IReadOnlyCollection<CrmPortalAuthProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
