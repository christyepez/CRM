namespace CRM.Application.Foundation;

public sealed record CrmSprint9GateDecisionContract(
    string Area,
    string Decision,
    string Reason);

public sealed record CrmSprint9EvidenceContract(
    string Package,
    string Evidence,
    string Status);

public sealed record CrmSprint10GateContract(
    string Package,
    string Objective,
    string Gate);

public sealed record CrmSprint9GateDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool Sprint9GateDecisionExists,
    bool Sprint9GateDecisionApproved,
    bool Sprint9Closed,
    bool Sprint9EvidenceComplete,
    bool Sprint9P1Complete,
    bool Sprint9P2Complete,
    bool Sprint9P3Complete,
    bool Sprint9P4Complete,
    bool Sprint9P5Complete,
    string OverallSprint9Decision,
    string ProductionActivationDecision,
    string SecretProviderRuntimeTrialDecision,
    string CommonDbRuntimeConnectivityTrialDecision,
    string PortalAuthRuntimeValidationTrialDecision,
    string ProductiveRouteDryRunTrialDecision,
    string ProductiveRouteRegistrationDecision,
    string ProductiveCrudDecision,
    string DeleteDecision,
    string DbRuntimeDecision,
    string PortalAuthEnforcementDecision,
    bool ProductionActivationApproved,
    bool RuntimeActivationApprovedForProduction,
    bool ProductiveRoutesApprovedByDefault,
    bool ProductiveCrudApproved,
    bool DeleteApproved,
    bool DatabaseWritesApproved,
    bool EfRuntimeApproved,
    bool MigrationsApproved,
    bool SchemaChangesApproved,
    bool PortalAuthEnforcementApproved,
    bool TokenHeaderReadsApproved,
    bool LoginLogoutApproved,
    bool IdentityRuntimeApproved,
    bool ProductiveUiApproved,
    bool NonProductionTrialsRemainAllowedOnlyWithExplicitFlags,
    bool AllTrialsFailClosedByDefault,
    bool AllObservabilityMetadataOnly,
    bool RollbackAvailable,
    string ProductizationStatus,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSprint9GateDecisionContract> Decisions,
    IReadOnlyCollection<CrmSprint9EvidenceContract> Evidence,
    IReadOnlyCollection<CrmSprint10GateContract> Sprint10Roadmap,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
