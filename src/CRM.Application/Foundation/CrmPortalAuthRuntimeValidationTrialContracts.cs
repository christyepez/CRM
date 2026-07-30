namespace CRM.Application.Foundation;

public sealed record CrmPortalAuthRuntimeValidationTrialProbeContract(
    string BaseUrlSecretName,
    string ClientIdSecretName,
    string ClientSecretName);

public sealed record CrmPortalAuthRuntimeValidationTrialGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmPortalAuthRuntimeValidationTrialObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmPortalAuthRuntimeValidationTrialBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmPortalAuthRuntimeValidationTrialStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalAuthRuntimeValidationTrialExists,
    bool PortalAuthRuntimeValidationTrialApproved,
    bool PortalAuthRuntimeValidationTrialEnabled,
    bool PortalAuthValidationAttempted,
    bool PortalAuthValidated,
    bool PortalHttpAttempted,
    bool PortalHttpConfigured,
    bool PortalAuthUrlResolved,
    bool PortalAuthUrlReturnedToApi,
    bool PortalClientSecretResolved,
    bool PortalClientSecretReturnedToApi,
    bool AuthHeaderRead,
    bool TokenRead,
    bool TokenStored,
    bool ClaimsMapped,
    bool ProductiveAuthEnabled,
    bool LoginEndpointCreated,
    bool LogoutEndpointCreated,
    bool IdentityRuntimeEnabled,
    bool AuthAttributeEnabled,
    bool SecretProviderMetadataDependencyValidated,
    bool CommonDbMetadataDependencyValidated,
    bool NonProductionOnly,
    bool ProductionBlocked,
    bool FailClosedByDefault,
    bool RollbackAvailable,
    bool ObservabilityMetadataOnly,
    string NextGate,
    string Warning,
    IReadOnlyCollection<string> ApprovedSecretNames,
    IReadOnlyCollection<CrmPortalAuthRuntimeValidationTrialGateContract> Gates,
    IReadOnlyCollection<CrmPortalAuthRuntimeValidationTrialObservationContract> Observations,
    IReadOnlyCollection<CrmPortalAuthRuntimeValidationTrialBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
