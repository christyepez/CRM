namespace CRM.Application.Foundation;

public sealed record CrmProductiveApiRouteDraftContract(
    string Method,
    string Route,
    string Resource,
    string Status,
    bool Registered,
    bool Enabled,
    string RequiredBeforeActivation);

public sealed record CrmProductiveApiActivationGateContract(
    string Gate,
    string Status,
    bool Ready,
    string RequiredBeforeActivation);

public sealed record CrmProductiveApiDisabledPolicyContract(
    string Policy,
    bool Allowed,
    string Reason);

public sealed record CrmProductiveApiRouteDraftStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool ProductiveApiRouteDraftExists,
    bool ProductiveRoutesRegistered,
    bool ProductiveCrudEnabled,
    bool ProductiveAuthorizationEnabled,
    bool AuthRuntimeEnabled,
    bool PortalRuntimeConnected,
    bool DurablePersistenceEnabled,
    bool RealDatabaseConfigured,
    bool EfRuntimeEnabled,
    bool DeleteEndpointsEnabled,
    bool FoundationCrudStillSeparate,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmProductiveApiRouteDraftContract> Routes,
    IReadOnlyCollection<CrmProductiveApiActivationGateContract> ActivationGates,
    IReadOnlyCollection<CrmProductiveApiDisabledPolicyContract> DisabledPolicies,
    IReadOnlyCollection<string> Risks);
