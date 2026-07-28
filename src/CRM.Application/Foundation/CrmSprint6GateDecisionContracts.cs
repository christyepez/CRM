namespace CRM.Application.Foundation;

public sealed record CrmSprint6CapabilityDecisionContract(
    string Capability,
    string Decision,
    string Reason);

public sealed record CrmSprint6EvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmSprint7RoadmapRecommendationContract(
    string Package,
    string Objective,
    string Gate);

public sealed record CrmSprint6GateDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string OverallDecision,
    string RealActivationDecision,
    string SecretProviderRealRuntimeDecision,
    string CommonDbRealConnectionDecision,
    string PortalAuthRealRuntimeDecision,
    string LockedStubRuntimeRegistrationDecision,
    string ProductiveRoutesDecision,
    string ProductiveCrudDecision,
    string DeleteDecision,
    string ProductiveUiDecision,
    string ProductizationStatus,
    string Sprint7PlanningDecision,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint6CapabilityDecisionContract> CapabilityDecisions,
    IReadOnlyCollection<CrmSprint6EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint7RoadmapRecommendationContract> Sprint7Roadmap,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
