namespace CRM.Application.Foundation;

public sealed record CrmProductizationGateDecisionContract(
    string Area,
    string Decision,
    string Reason,
    string RequiredBeforeGo);

public sealed record CrmProductizationCapabilityDecisionContract(
    string Capability,
    string Owner,
    string CurrentStatus,
    string Evidence,
    string Decision,
    string RequiredBeforeGo,
    string TargetSprint,
    string RiskLevel);

public sealed record CrmSprint3RoadmapRecommendationContract(
    string Package,
    string Goal,
    string Gate,
    string Constraint);

public sealed record CrmSprint2ProductizationGateStatusResponse(
    string Module,
    string Status,
    string ProductizationStatus,
    string OverallDecision,
    string FoundationCrudDecision,
    string DurablePersistenceDecision,
    string RealDatabaseDecision,
    string PortalAuthRuntimeDecision,
    string ProductiveCrudApiDecision,
    string Sprint3PlanningDecision,
    string NextGate,
    string Warning,
    bool FoundationMode,
    bool DurablePersistence,
    bool DatabaseConfigured,
    bool ProductiveCrudEnabled,
    bool PortalRuntimeConnected,
    IReadOnlyCollection<string> Sprint2Evidence,
    IReadOnlyCollection<CrmProductizationGateDecisionContract> Decisions,
    IReadOnlyCollection<CrmProductizationCapabilityDecisionContract> Capabilities,
    IReadOnlyCollection<CrmSprint3RoadmapRecommendationContract> Sprint3Roadmap,
    IReadOnlyCollection<string> Risks);
