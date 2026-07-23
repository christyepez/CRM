namespace CRM.Application.Foundation;

public sealed record CrmSprint3CapabilityDecisionContract(
    string Capability,
    string Decision,
    string Status,
    bool ReadyForRealActivation,
    string Evidence,
    string RequiredBeforeGo);

public sealed record CrmSprint3EvidenceContract(
    string Package,
    string Status,
    string Evidence,
    string Guardrail);

public sealed record CrmSprint4RoadmapRecommendationContract(
    string Package,
    string Goal,
    string Decision,
    string Gate);

public sealed record CrmSprint3ProductizationReviewStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string OverallDecision,
    string ProductizationStatus,
    string DurablePersistenceDecision,
    string RealDatabaseDecision,
    string EfRuntimeDecision,
    string PortalAuthRuntimeDecision,
    string ProductiveApiRoutesDecision,
    string ProductiveCrmUiDecision,
    string FoundationCapabilitiesDecision,
    string Sprint4PlanningDecision,
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
    IReadOnlyCollection<CrmSprint3CapabilityDecisionContract> Decisions,
    IReadOnlyCollection<CrmSprint3EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint4RoadmapRecommendationContract> Sprint4Roadmap,
    IReadOnlyCollection<string> Risks);
