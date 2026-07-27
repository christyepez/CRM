namespace CRM.Application.Portal;

public sealed class CrmPortalAuthRuntimeProbeStatusService
{
    public const string WarningText = "Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted";
    public const string NextGate = "Sprint4P4ProductiveRoutesLockedStubValidation";

    public CrmPortalAuthRuntimeProbeStatusResponse GetStatus() =>
        new(
            "CRM",
            "PortalAuthRuntimeProbe",
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
            false,
            true,
            NextGate,
            WarningText,
            GetCapabilities(),
            GetSafetyGates(),
            GetBlockedItems(),
            [
                "A disabled Portal Auth runtime probe can be mistaken for active authentication if middleware is added early.",
                "CRM must not read or store credentials because PortalCorporativo owns Auth, SSO, user, tenant and permissions.",
                "Future runtime enablement requires a signed Portal contract, correlation strategy and observability approval.",
                "Productive CRM routes remain blocked until Portal authorization runtime is explicitly approved."
            ]);

    public IReadOnlyCollection<CrmPortalAuthRuntimeProbeCapabilityContract> GetCapabilities() =>
    [
        new("Portal Auth Runtime Probe", "Exists", true, "Contract and disabled placeholder exist."),
        new("Portal Auth Runtime Probe Enabled", "Disabled", false, "portalAuthRuntimeProbeEnabled=false."),
        new("Portal Runtime Connected", "False", false, "No runtime Portal call is attempted."),
        new("Token Read Attempted By Runtime", "False", false, "No token, header or credential is read."),
        new("Foundation Simulation Active", "True", true, "Existing foundation simulation remains the only authorization model.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRuntimeProbeSafetyGateContract> GetSafetyGates() =>
    [
        new("Portal endpoint approved", "NoGo", false, "Approve the runtime Portal endpoint outside this disabled probe."),
        new("Auth contract signed", "NoGo", false, "Approve user, tenant, capability and policy result schema."),
        new("Correlation id defined", "NoGo", false, "Define correlation propagation before runtime integration."),
        new("Token propagation strategy approved", "NoGo", false, "Confirm no local token storage and no credential persistence."),
        new("Audit and observability approved", "NoGo", false, "Define audit event, redaction and tracing requirements."),
        new("Rollback defined", "NoGo", false, "Document rollback before enabling any runtime call.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRuntimeProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Auth runtime", "Blocked", "No middleware, policy enforcement or productive authorization is enabled."),
        new("Token reads", "Blocked", "No runtime credential/header/token access is allowed in P3."),
        new("Portal runtime calls", "Blocked", "No Portal network call is attempted by runtime."),
        new("Login and Identity", "Blocked", "CRM must not implement login, logout, SSO, user store or Identity."),
        new("Productive CRM routes", "Blocked", "Productive routes remain locked until a later gate.")
    ];
}
