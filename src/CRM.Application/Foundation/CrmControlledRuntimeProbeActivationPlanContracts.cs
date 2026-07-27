namespace CRM.Application.Foundation;

public sealed record CrmRuntimeProbeActivationGateContract(
    string Probe,
    string Gate,
    bool Approved,
    string RequiredEvidence);

public sealed record CrmRuntimeProbeApprovalRequirementContract(
    string Requirement,
    string Owner,
    bool Required,
    bool Satisfied);

public sealed record CrmRuntimeProbeRollbackRequirementContract(
    string Requirement,
    string Trigger,
    bool Required);

public sealed record CrmRuntimeProbeObservabilityRequirementContract(
    string Requirement,
    string Evidence,
    bool Required);

public sealed record CrmControlledRuntimeProbeActivationPlanStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool RuntimeProbeActivationPlanExists,
    bool RuntimeProbeActivationApproved,
    bool CommonDbProbeActivationApproved,
    bool PortalAuthProbeActivationApproved,
    bool ProductiveRoutesActivationApproved,
    bool RealActivationApproved,
    bool NonProductionOnly,
    bool SyntheticDataRequired,
    bool RollbackPlanRequired,
    bool ObservabilityRequired,
    bool SecretProviderRequired,
    bool DeleteStillNoGo,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmRuntimeProbeActivationGateContract> ActivationGates,
    IReadOnlyCollection<CrmRuntimeProbeApprovalRequirementContract> ApprovalRequirements,
    IReadOnlyCollection<CrmRuntimeProbeRollbackRequirementContract> RollbackRequirements,
    IReadOnlyCollection<CrmRuntimeProbeObservabilityRequirementContract> ObservabilityRequirements,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
