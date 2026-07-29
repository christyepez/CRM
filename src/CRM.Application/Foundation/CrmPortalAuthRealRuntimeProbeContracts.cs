namespace CRM.Application.Foundation;

public sealed record CrmPortalAuthRealRuntimeProbeGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Reason);

public sealed record CrmPortalAuthRealRuntimeProbeObservationContract(
    string Observation,
    bool Passed,
    string Notes);

public sealed record CrmPortalAuthRealRuntimeProbeDependencyContract(
    string Dependency,
    bool Required,
    bool Available,
    string Status);

public sealed record CrmPortalAuthRealRuntimeProbeBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmPortalAuthRealRuntimeProbeStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalAuthRealRuntimeProbeExists,
    bool PortalAuthRealRuntimeApprovalGranted,
    bool SecretProviderRealNonProductionApprovalGranted,
    bool PortalAuthRealRuntimeProbeEnabled,
    bool PortalAuthRealRuntimeProbeAttempted,
    bool PortalAuthRuntimeConnected,
    bool PortalAuthBaseUrlResolved,
    bool PortalAuthBaseUrlMaterialized,
    bool PortalAuthBaseUrlLogged,
    bool PortalAuthBaseUrlReturnedToApi,
    bool PortalHttpClientCreated,
    bool PortalHttpCallAttempted,
    bool PortalAuthTokenValidationAttempted,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool AuthorizationHeaderReadAttempted,
    bool RealTokenMaterialized,
    bool RealTokenLogged,
    bool TokenReturnedToApi,
    bool LoginImplementedByCrm,
    bool LogoutImplementedByCrm,
    bool IdentityImplementedByCrm,
    bool RolesPersistedInCrm,
    bool PermissionsPersistedInCrm,
    bool ProductiveAuthorizationEnabled,
    bool ApiRequiresPortalAuth,
    bool UsesSyntheticFallback,
    string SyntheticPortalAuthReference,
    string SyntheticUserReference,
    bool ProbeSkippedBecausePortalAuthApprovalNotGranted,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeDependencyContract> Dependencies,
    IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeGateContract> Gates,
    IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeObservationContract> Observations,
    IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
