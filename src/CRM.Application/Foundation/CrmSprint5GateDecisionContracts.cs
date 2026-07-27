namespace CRM.Application.Foundation;

public sealed record CrmSprint5CapabilityDecisionContract(
    string Capability,
    string Decision,
    string Reason);

public sealed record CrmSprint5EvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmSprint6RoadmapRecommendationContract(
    string Package,
    string Objective,
    string Gate);

public sealed record CrmSprint5GateDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string OverallDecision,
    string RealActivationDecision,
    string ProductizationStatus,
    string SecretProviderRuntimeDecision,
    string CommonDbRuntimeDecision,
    string PortalAuthRuntimeDecision,
    string ProductiveRoutesDecision,
    string LockedStubRuntimeDecision,
    string ProductiveCrudDecision,
    string DeleteDecision,
    string ProductiveUiDecision,
    string Sprint6PlanningDecision,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint5CapabilityDecisionContract> CapabilityDecisions,
    IReadOnlyCollection<CrmSprint5EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint6RoadmapRecommendationContract> Sprint6Roadmap,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
