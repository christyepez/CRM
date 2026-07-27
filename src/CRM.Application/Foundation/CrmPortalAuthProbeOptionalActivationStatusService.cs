namespace CRM.Application.Foundation;

public sealed class CrmPortalAuthProbeOptionalActivationStatusService
{
    public const string WarningText = "Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted";
    public const string NextGate = "Sprint5P5LockedProductiveRouteStubTrialInNonProduction";

    public CrmPortalAuthProbeOptionalActivationStatusResponse GetStatus() =>
        new(
            "CRM",
            "PortalAuthProbeOptionalActivation",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            NextGate,
            WarningText,
            GetActivationGates(),
            GetDependencies(),
            GetRollbackRequirements(),
            GetBlockedItems(),
            [
                "Optional activation wording could be confused with approval if disabled flags are ignored.",
                "Future non-production activation must approve token propagation before any Portal runtime contact.",
                "CRM must continue to avoid login, Identity, token storage and persisted permissions."
            ]);

    public IReadOnlyCollection<CrmPortalAuthProbeActivationGateContract> GetActivationGates() =>
    [
        new("Portal authorization runtime approval", "Security", true, false, "Token propagation strategy, Portal ownership and runtime boundaries approved."),
        new("Secret provider runtime approval", "Security", true, false, "Secret provider is connected without values in files and secret reads explicitly approved."),
        new("Non-production only approval", "Architecture Governance", true, false, "Probe can run only in non-production with synthetic users and no productive CRM routes."),
        new("Rollback approval", "DevOps", true, false, "Feature flag rollback and health regression procedure approved.")
    ];

    public IReadOnlyCollection<CrmPortalAuthProbeDependencyContract> GetDependencies() =>
    [
        new("PortalCorporativo Auth ownership", true, false, "Portal remains owner of SSO, users, tenants, roles and permissions."),
        new("Token propagation strategy", true, false, "Required before activation; no tokens or headers are read in P4."),
        new("Secret Provider Runtime", true, false, "Required before activation; not connected in P4."),
        new("Correlation ID strategy", true, true, "Required for future traceability without logging secrets.")
    ];

    public IReadOnlyCollection<CrmPortalAuthProbeRollbackContract> GetRollbackRequirements() =>
    [
        new("Keep probe disabled by default", true, "Any unexpected Portal HTTP, token or header read attempt."),
        new("Return to foundation-only endpoints", true, "Health/readiness regression."),
        new("Preserve negative route checks", true, "Any productive CRM route returns success.")
    ];

    public IReadOnlyCollection<CrmPortalAuthProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Portal Auth probe activation", "Approval gates remain false."),
        new("Portal HTTP calls", "P4 is contract-only and cannot call Portal runtime."),
        new("Token/header reads", "Token propagation strategy is not approved."),
        new("Productive authorization", "CRM cannot enable authorization middleware or [Authorize] yet.")
    ];
}
