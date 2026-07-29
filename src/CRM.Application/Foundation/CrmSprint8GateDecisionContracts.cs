namespace CRM.Application.Foundation;

public sealed record CrmSprint8CapabilityDecisionContract(
    string Capability,
    string Decision,
    string Reason);

public sealed record CrmSprint8EvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmSprint9RoadmapRecommendationContract(
    string Package,
    string Objective,
    string Gate);

public sealed record CrmSprint8GateDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string OverallDecision,
    string RealProductionActivationDecision,
    string SecretProviderControlledReadDecision,
    string CommonDbControlledConnectivityDecision,
    string PortalAuthControlledValidationDecision,
    string LockedRouteAuthorizationPolicyDecision,
    string ProductiveRoutesDefaultDecision,
    string ProductiveCrudDecision,
    string DeleteDecision,
    string ProductiveUiDecision,
    string ProductizationStatus,
    string Sprint9PlanningDecision,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint8CapabilityDecisionContract> CapabilityDecisions,
    IReadOnlyCollection<CrmSprint8EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint9RoadmapRecommendationContract> Sprint9Roadmap,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
