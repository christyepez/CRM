namespace CRM.Application.Foundation;

public sealed record CrmNonProductionRuntimeApprovalCapabilityContract(
    string Capability,
    bool ApprovalGranted,
    string RequiredEvidence,
    string Owner);

public sealed record CrmNonProductionRuntimeApprovalRequirementContract(
    string Requirement,
    bool Required,
    bool Completed,
    string Notes);

public sealed record CrmNonProductionRuntimeApprovalEvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmNonProductionRuntimeApprovalBlockedItemContract(
    string Item,
    string Reason,
    string RequiredDecision);

public sealed record CrmNonProductionRuntimeApprovalPackageStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool NonProductionRuntimeApprovalPackageExists,
    bool NonProductionRuntimeApprovalGranted,
    bool SecretProviderMockApprovalGranted,
    bool CommonDbDryRunApprovalGranted,
    bool PortalAuthDryRunApprovalGranted,
    bool LockedStubRuntimeTrialApprovalGranted,
    bool RealActivationApprovalGranted,
    bool ProductiveRoutesApprovalGranted,
    bool DeleteApprovalGranted,
    bool SyntheticDataApprovalRequired,
    bool RollbackApprovalRequired,
    bool ObservabilityApprovalRequired,
    bool SecurityReviewRequired,
    bool ArchitectureReviewRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmNonProductionRuntimeApprovalCapabilityContract> Capabilities,
    IReadOnlyCollection<CrmNonProductionRuntimeApprovalRequirementContract> Requirements,
    IReadOnlyCollection<CrmNonProductionRuntimeApprovalEvidenceContract> Evidence,
    IReadOnlyCollection<CrmNonProductionRuntimeApprovalBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
