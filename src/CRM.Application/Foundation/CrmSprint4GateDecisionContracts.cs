namespace CRM.Application.Foundation;

public sealed record CrmSprint4GateCapabilityDecisionContract(
    string Capability,
    string Decision,
    string Evidence,
    bool RealActivationAllowed);

public sealed record CrmSprint4EvidenceContract(
    string Area,
    string Result,
    string Evidence,
    bool Passed);

public sealed record CrmSprint5RoadmapRecommendationContract(
    string Package,
    string Recommendation,
    string Gate,
    bool ImplementNow);

public sealed record CrmSprint4GateDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string OverallDecision,
    string RealActivationDecision,
    string ProductizationStatus,
    string DurablePersistenceDecision,
    string CommonDbRuntimeDecision,
    string PortalAuthRuntimeDecision,
    string ProductiveRoutesDecision,
    string ProductiveCrudDecision,
    string DeleteDecision,
    string ProductiveUiDecision,
    string NonProductionE2EPilotDecision,
    string Sprint5PlanningDecision,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint4GateCapabilityDecisionContract> CapabilityDecisions,
    IReadOnlyCollection<CrmSprint4EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint5RoadmapRecommendationContract> Sprint5Roadmap,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
