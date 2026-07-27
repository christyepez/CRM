namespace CRM.Application.Foundation;

public sealed class CrmNonProductionRuntimeApprovalPackageStatusService
{
    public const string WarningText = "NonProduction runtime approval package only; no runtime approval is granted";
    public const string NextGate = "Sprint6P2SecretProviderSafeMockActivation";

    public CrmNonProductionRuntimeApprovalPackageStatusResponse GetStatus() =>
        new(
            "CRM",
            "NonProductionRuntimeApprovalPackage",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetCapabilities(),
            GetRequirements(),
            GetEvidence(),
            GetBlockedItems(),
            [
                "Approval package existence must not be interpreted as runtime approval.",
                "Sprint 6 P2 must keep secret provider activity in safe mock mode until explicitly approved.",
                "DB dry-runs, Portal Auth dry-runs and locked stub runtime trials remain blocked until their own approval gates."
            ]);

    public IReadOnlyCollection<CrmNonProductionRuntimeApprovalCapabilityContract> GetCapabilities() =>
    [
        new("Secret Provider Safe Mock", false, "Approved mock-only provider plan, no real secret reads, rollback and logs.", "Security Agent"),
        new("Common DB Dry-Run Contract", false, "Synthetic data, common SQL ownership validation, no connection attempt before approval.", "Data Architect Agent"),
        new("Portal Auth Token Propagation Dry-Run Contract", false, "Portal contract, token/header boundary, no Portal HTTP before approval.", "Portal Integration Agent"),
        new("Locked Stub Runtime Trial", false, "Runtime flag, locked response contract, negative routes and rollback.", "Backend Agent")
    ];

    public IReadOnlyCollection<CrmNonProductionRuntimeApprovalRequirementContract> GetRequirements() =>
    [
        new("Synthetic data approval", true, false, "Required before any dry-run touches runtime-like flows."),
        new("Rollback approval", true, false, "Required before enabling any non-production runtime trial."),
        new("Observability approval", true, false, "Required for traceability, health and negative route checks."),
        new("Security review", true, false, "Required before secret mock, token propagation or authorization-adjacent work."),
        new("Architecture review", true, false, "Required before DB, Portal Auth or locked stub runtime trials.")
    ];

    public IReadOnlyCollection<CrmNonProductionRuntimeApprovalEvidenceContract> GetEvidence() =>
    [
        new("Sprint 5 closure", "Sprint 5 P6 gate decision merged and real activation remains NoGo.", "Available"),
        new("Approval matrix", "Sprint 6 P1 documents required approvals and keeps all approvals false.", "Created"),
        new("Runtime state", "No secret reads, DB connections, Portal HTTP, token/header reads or productive routes are activated.", "NotGranted"),
        new("Next gate", "Sprint 6 P2 Secret Provider Safe Mock Activation must request its own approval.", "Pending")
    ];

    public IReadOnlyCollection<CrmNonProductionRuntimeApprovalBlockedItemContract> GetBlockedItems() =>
    [
        new("Secret Provider runtime", "Mock approval is not granted.", "Sprint6P2SecretProviderSafeMockActivation"),
        new("Common DB dry-run", "DB dry-run approval is not granted.", "Sprint6P3CommonDbConnectivityDryRunContract"),
        new("Portal Auth dry-run", "Portal Auth dry-run approval is not granted.", "Sprint6P4PortalAuthTokenPropagationDryRunContract"),
        new("Locked stub runtime trial", "Locked stub trial approval is not granted.", "Sprint6P5LockedStubRuntimeRegistrationTrial"),
        new("Real activation", "Productization and real activation approvals are not granted.", "Future productization gate")
    ];
}
