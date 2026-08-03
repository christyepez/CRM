namespace CRM.Application.Foundation;

public sealed record CrmSprint10ProductizationDecisionContract(
    string Area,
    string Decision,
    string Reason);

public sealed record CrmSprint10ProductizationEvidenceContract(
    string Package,
    string Evidence,
    string Status);

public sealed record CrmSprint10ProductizationReadinessRiskContract(
    string Risk,
    string Impact,
    string Mitigation);

public sealed record CrmSprint10ProductizationReadinessDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool Sprint10P1ProductizationReadinessDecisionExists,
    bool Sprint10P1Approved,
    bool Sprint9GateReviewed,
    bool Sprint9ProductionNoGoPreserved,
    string Sprint10P1Decision,
    string ProductionActivationDecision,
    string ProductiveRuntimeActivationDecision,
    string CommonDbControlledActivationDecision,
    string PortalAuthControlledActivationDecision,
    string ProductiveRouteControlledActivationDecision,
    string ProductiveCrudPilotDecision,
    string ProductiveUiDecision,
    bool ProductionActivationApproved,
    bool ProductiveRuntimeActivationApprovedForProduction,
    bool CommonDbControlledPreparationApproved,
    bool PortalAuthControlledPreparationApproved,
    bool ProductiveRouteControlledPreparationApproved,
    bool ProductiveCrudPilotApproved,
    bool ProductiveUiApproved,
    bool NonProductionOnly,
    bool ExplicitFlagsRequired,
    bool FailClosedByDefault,
    bool ObservabilityMetadataOnly,
    bool RollbackAvailable,
    string ProductizationStatus,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint10ProductizationDecisionContract> Decisions,
    IReadOnlyCollection<CrmSprint10ProductizationEvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint10ProductizationReadinessRiskContract> Risks,
    IReadOnlyCollection<string> BlockedItems);
