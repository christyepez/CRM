namespace CRM.Application.Foundation;

public sealed record CrmControlledRuntimeActivationCapabilityDecisionContract(
    string Capability,
    bool TrialApproved,
    bool EnabledNow,
    string Decision,
    string Reason);

public sealed record CrmControlledRuntimeActivationGateContract(
    string Gate,
    string Objective,
    string Status);

public sealed record CrmControlledRuntimeActivationEvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmControlledRuntimeActivationBlockedItemContract(
    string Item,
    string Reason);

public sealed record CrmSprint9RoadmapGateContract(
    string Package,
    string Objective,
    string Gate);

public sealed record CrmControlledRuntimeActivationDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool ControlledRuntimeActivationDecisionExists,
    string ControlledRuntimeActivationDecision,
    string ProductionActivationDecision,
    bool SecretProviderRuntimeEnablementTrialApproved,
    bool CommonDbRuntimeConnectivityTrialApproved,
    bool PortalAuthRuntimeValidationTrialApproved,
    bool ProductiveRouteDryRunTrialApproved,
    bool RuntimeTrialsEnabledNow,
    bool ProductionRuntimeEnabledNow,
    bool SecretProviderRuntimeEnabledNow,
    bool CommonDbRuntimeEnabledNow,
    bool PortalAuthRuntimeEnabledNow,
    bool ProductiveRoutesEnabledNow,
    bool ProductiveCrudEnabledNow,
    bool DeleteEnabledNow,
    bool ProductiveUiEnabledNow,
    bool DefaultFailClosedRequired,
    bool ExplicitNonProductionFlagsRequired,
    bool RollbackRequired,
    bool ObservabilityRequired,
    bool SecurityApprovalRequiredForEachTrial,
    bool ArchitectureApprovalRequiredForEachTrial,
    bool DevOpsApprovalRequiredForEachTrial,
    bool QaApprovalRequiredForEachTrial,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmControlledRuntimeActivationCapabilityDecisionContract> CapabilityDecisions,
    IReadOnlyCollection<CrmControlledRuntimeActivationGateContract> Gates,
    IReadOnlyCollection<CrmControlledRuntimeActivationEvidenceContract> Evidence,
    IReadOnlyCollection<CrmControlledRuntimeActivationBlockedItemContract> BlockedItems,
    IReadOnlyCollection<CrmSprint9RoadmapGateContract> Sprint9Roadmap,
    IReadOnlyCollection<string> Risks);
