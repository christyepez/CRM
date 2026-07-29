namespace CRM.Application.Foundation;

public sealed record CrmPortalAuthControlledRuntimeValidationProbeContract(
    bool ProbeAttempted,
    bool ProviderConfigured,
    bool PortalAuthMetadataAvailable,
    bool PortalAuthValidationAttempted,
    bool PortalAuthReachable,
    bool TimeoutApplied,
    int TimeoutSeconds,
    bool PortalUrlReturned,
    bool SecretValueReturned,
    bool TokenReturned,
    bool HeaderReadAttempted);

public sealed record CrmPortalAuthControlledRuntimeValidationGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmPortalAuthControlledRuntimeValidationObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmPortalAuthControlledRuntimeValidationBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmPortalAuthControlledRuntimeValidationProbeRequest(string BaseUrlSecretName, string ClientIdSecretName, string ClientSecretName);

public sealed record CrmPortalAuthControlledRealRuntimeValidationStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalAuthControlledRealRuntimeValidationExists,
    bool PortalAuthControlledRealRuntimeValidationApproved,
    bool PortalAuthControlledRealRuntimeValidationEnabled,
    bool PortalAuthRuntimeValidationAttempted,
    bool PortalAuthRuntimeConnected,
    bool SecretProviderAvailabilityMetadataUsed,
    bool PortalAuthBaseUrlResolved,
    bool PortalAuthBaseUrlMaterializedInPublicContract,
    bool PortalAuthBaseUrlLogged,
    bool PortalAuthBaseUrlReturnedToApi,
    bool PortalHttpClientCreated,
    bool PortalHttpCallAttempted,
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
    bool NonProductionOnly,
    bool FailClosedByDefault,
    string NextGate,
    string Warning,
    CrmPortalAuthControlledRuntimeValidationProbeContract Probe,
    IReadOnlyCollection<CrmPortalAuthControlledRuntimeValidationGateContract> Gates,
    IReadOnlyCollection<CrmPortalAuthControlledRuntimeValidationObservationContract> Observations,
    IReadOnlyCollection<CrmPortalAuthControlledRuntimeValidationBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
