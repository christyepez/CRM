namespace CRM.Application.Foundation;

public sealed record CrmProductiveRouteDryRunTrialProbeContract(
    string? Route,
    string? Method);

public sealed record CrmProductiveRouteDryRunTrialGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmProductiveRouteDryRunTrialObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmProductiveRouteDryRunTrialBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmProductiveRouteDryRunTrialEvaluationRequest(
    string? Route,
    string? Method,
    bool TrialEnabled,
    string RuntimeEnvironment);

public sealed record CrmProductiveRouteDryRunTrialEvaluationResult(
    bool ProductiveRouteDryRunAttempted,
    bool ProductiveRouteDryRunAllowed,
    bool ProductiveRouteDryRunDecisionReturned,
    int ProductiveRouteDryRunStatusCode,
    bool ProductiveCrudEnabled,
    bool ProductiveDomainExecutionEnabled,
    bool ProductivePersistenceEnabled,
    bool DatabaseWriteAttempted,
    bool SideEffectsAllowed,
    bool DeleteEndpointsEnabled,
    bool DbRuntimeEnabled,
    bool EfRuntimeEnabled,
    bool MigrationsEnabled,
    bool SchemaChangeAllowed,
    bool AuthHeaderRead,
    bool TokenRead,
    bool TokenStored,
    bool AuthAttributeEnabled,
    bool LoginEndpointCreated,
    bool LogoutEndpointCreated,
    bool IdentityRuntimeEnabled,
    bool NonProductionOnly,
    bool ProductionBlocked,
    bool FailClosedByDefault,
    string Status,
    string Warning,
    string? ErrorCategory);

public sealed record CrmProductiveRouteDryRunTrialStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool ProductiveRouteDryRunTrialExists,
    bool ProductiveRouteDryRunTrialApproved,
    bool ProductiveRouteDryRunTrialEnabled,
    bool ProductiveRoutesRegisteredByDefault,
    bool ProductiveRoutesDryRunRegistered,
    bool ProductiveRouteDryRunAttempted,
    bool ProductiveRouteDryRunAllowed,
    bool ProductiveRouteDryRunDecisionReturned,
    int ProductiveRouteDryRunStatusCode,
    bool ProductiveCrudEnabled,
    bool ProductiveDomainExecutionEnabled,
    bool ProductivePersistenceEnabled,
    bool DatabaseWriteAttempted,
    bool SideEffectsAllowed,
    bool DeleteEndpointsEnabled,
    bool DbRuntimeEnabled,
    bool EfRuntimeEnabled,
    bool MigrationsEnabled,
    bool SchemaChangeAllowed,
    bool PortalAuthMetadataDependencyValidated,
    bool CommonDbMetadataDependencyValidated,
    bool SecretProviderMetadataDependencyValidated,
    bool AuthHeaderRead,
    bool TokenRead,
    bool TokenStored,
    bool AuthAttributeEnabled,
    bool LoginEndpointCreated,
    bool LogoutEndpointCreated,
    bool IdentityRuntimeEnabled,
    bool NonProductionOnly,
    bool ProductionBlocked,
    bool FailClosedByDefault,
    bool RollbackAvailable,
    bool ObservabilityMetadataOnly,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmProductiveRouteDryRunTrialGateContract> Gates,
    IReadOnlyCollection<CrmProductiveRouteDryRunTrialObservationContract> Observations,
    IReadOnlyCollection<CrmProductiveRouteDryRunTrialBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
