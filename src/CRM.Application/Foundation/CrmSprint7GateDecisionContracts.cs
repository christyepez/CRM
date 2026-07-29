namespace CRM.Application.Foundation;

public sealed record CrmSprint7CapabilityDecisionContract(
    string Capability,
    string Decision,
    string Reason);

public sealed record CrmSprint7EvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmSprint8RoadmapRecommendationContract(
    string Package,
    string Objective,
    string Gate);

public sealed record CrmSprint7GateDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string OverallDecision,
    string RealActivationDecision,
    string SecretProviderRealRuntimeDecision,
    string CommonDbRealConnectionDecision,
    string PortalAuthRealRuntimeDecision,
    string LockedProductiveRouteRegistrationDecision,
    string ProductiveRoutesDefaultDecision,
    string ProductiveCrudDecision,
    string DeleteDecision,
    string ProductiveUiDecision,
    string ProductizationStatus,
    string Sprint8PlanningDecision,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint7CapabilityDecisionContract> CapabilityDecisions,
    IReadOnlyCollection<CrmSprint7EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint8RoadmapRecommendationContract> Sprint8Roadmap,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
