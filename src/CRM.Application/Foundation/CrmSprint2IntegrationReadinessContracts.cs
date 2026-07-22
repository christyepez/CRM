namespace CRM.Application.Foundation;

public sealed record CrmActivationGateContract(
    string Gate,
    string Status,
    bool Ready,
    string RequiredBeforeActivation);

public sealed record CrmProductizationRiskContract(
    string Risk,
    string Severity,
    string Mitigation,
    bool BlocksProductization);

public sealed record CrmReadinessDecisionContract(
    string Area,
    string RecommendedDecision,
    string Reason);

public sealed record CrmSprint2IntegrationReadinessStatusResponse(
    string Module,
    string Status,
    string ProductizationStatus,
    bool DatabaseReady,
    bool AuthReady,
    bool ProductiveCrudReady,
    bool FoundationCrudEnabled,
    bool DurablePersistence,
    bool PortalRuntimeConnected,
    string RecommendedDecision,
    string NextGate,
    string Warning,
    bool FoundationMode,
    IReadOnlyCollection<string> Sprint2Evidence,
    IReadOnlyCollection<CrmActivationGateContract> Gates,
    IReadOnlyCollection<CrmProductizationRiskContract> Risks,
    IReadOnlyCollection<CrmReadinessDecisionContract> Decisions);
