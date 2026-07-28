namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderRealNonProductionApprovalStatusService
{
    public const string WarningText = "Secret Provider real NonProduction approval package only; no real secrets are read";
    public const string NextGate = "Sprint7P2SecretProviderRealNonProductionRuntimeProbe";

    public CrmSecretProviderRealNonProductionApprovalStatusResponse GetStatus() =>
        new(
            "CRM",
            "SecretProviderRealNonProductionApproval",
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
            false,
            false,
            true,
            true,
            true,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetLogicalSecretNames(),
            GetApprovalGates(),
            GetEvidenceRequired(),
            GetBlockedItems(),
            [
                "Approval package existence must not be treated as approval to read real secrets.",
                "P2 must receive explicit NonProduction approval before any runtime probe is enabled.",
                "Secret values must remain outside committed files and must never be logged."
            ]);

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionSecretNameContract> GetLogicalSecretNames() =>
    [
        new("crm-common-db-connection", "Future CRM logical database connection reference for NonProduction probes.", false, false),
        new("crm-portal-auth-base-url", "Future Portal Auth base endpoint reference for NonProduction probes.", false, false),
        new("crm-portal-auth-client-id", "Future Portal Auth client identifier reference for NonProduction probes.", false, false),
        new("crm-portal-auth-client-secret", "Future Portal Auth client secret reference for NonProduction probes.", false, false),
        new("crm-observability-endpoint", "Future observability endpoint reference for NonProduction probes.", false, false)
    ];

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionApprovalGateContract> GetApprovalGates() =>
    [
        new("Security review", true, false, "Approve scope, least privilege, owner and sanitization controls."),
        new("Architecture review", true, false, "Approve boundary that keeps CRM from owning secrets infrastructure."),
        new("DevOps review", true, false, "Approve external secret scope, access policy and rollout plan outside the repo."),
        new("Rollback", true, false, "Approve disable switch and rollback procedure before runtime probing."),
        new("Observability", true, false, "Approve sanitized metrics and logs with no secret values.")
    ];

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionEvidenceContract> GetEvidenceRequired() =>
    [
        new("External secret scope defined outside committed files", true, false, "Secret location and ACL must be approved without committing values."),
        new("Least privilege access model", true, false, "P2 must prove read access is limited to approved logical names."),
        new("Rotation owner and cadence", true, false, "Owner and rotation evidence must exist before probe."),
        new("Rollback plan", true, true, "Runbook exists; approval remains false."),
        new("Sanitized logging policy", true, true, "Logging must record names/status only, never values.")
    ];

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionBlockedItemContract> GetBlockedItems() =>
    [
        new("Real secret reads", "Approval is not granted.", NextGate),
        new("Runtime secret provider connection", "Runtime provider remains disabled.", NextGate),
        new("Common DB real connection", "DB runtime is outside P1 and remains NoGo.", "Sprint7P3CommonDbRealConnectivityNonProductionProbe"),
        new("Portal Auth runtime", "Portal Auth runtime is outside P1 and remains NoGo.", "Sprint7P4PortalAuthRealRuntimeProbe"),
        new("Productive routes and DELETE", "Productization remains NotReady.", "Future productization gate")
    ];
}
