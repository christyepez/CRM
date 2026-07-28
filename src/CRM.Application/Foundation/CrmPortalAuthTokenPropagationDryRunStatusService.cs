namespace CRM.Application.Foundation;

public sealed class CrmPortalAuthTokenPropagationDryRunStatusService
{
    public const string SyntheticTokenReference = "mock://crm/portal-auth-token";
    public const string SyntheticUserReference = "mock://crm/portal-user";
    public const string WarningText = "Portal Auth token propagation dry-run contract only; no real tokens or headers are read";
    public const string NextGate = "Sprint6P5LockedStubRuntimeRegistrationTrial";

    public CrmPortalAuthTokenPropagationDryRunStatusResponse GetStatus() =>
        new(
            "CRM",
            "PortalAuthTokenPropagationDryRunContract",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            SyntheticTokenReference,
            SyntheticUserReference,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetDependencies(),
            GetSafetyGates(),
            GetObservability(),
            GetBlockedItems(),
            [
                "Synthetic token metadata must not be exchanged for a real token without a future approval gate.",
                "CRM must not become the owner of login, identity, tenant, roles or permission persistence.",
                "Future token propagation still requires PortalCorporativo contract evidence, rollback and observability approval."
            ]);

    public IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunDependencyContract> GetDependencies() =>
    [
        new("PortalCorporativo Auth ownership", true, true, "Portal remains the owner of Auth, SSO, user, tenant and permissions."),
        new("Secret Provider Safe Mock metadata", true, true, "Uses only synthetic references mock://crm/portal-auth-token and mock://crm/portal-user."),
        new("Portal Auth runtime", true, false, "Not contacted in P4; no Portal HTTP is attempted."),
        new("Token propagation approval", true, false, "Required before any future token/header propagation trial.")
    ];

    public IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunSafetyGateContract> GetSafetyGates() =>
    [
        new("No token reads", true, true, "TokenReadAttempted remains false."),
        new("No header reads", true, true, "HeaderReadAttempted remains false."),
        new("No Portal HTTP", true, true, "PortalHttpAttempted remains false."),
        new("No Auth middleware", true, true, "CRM does not activate productive authorization runtime."),
        new("No identity or permission persistence", true, true, "CRM does not store users, roles or permissions.")
    ];

    public IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunObservabilityContract> GetObservability() =>
    [
        new("Dry-run status endpoint", true, true, "Reports contract state through foundation API."),
        new("Token/header read flags", true, true, "Always false in P4."),
        new("Synthetic token/user markers", true, true, "Reports mock://crm/portal-auth-token and mock://crm/portal-user only."),
        new("Negative route checks", true, true, "Productive CRM routes must remain inactive.")
    ];

    public IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunBlockedItemContract> GetBlockedItems() =>
    [
        new("Real token propagation", "Portal Auth dry-run approval is not granted.", "Future explicit Portal Auth propagation gate"),
        new("Authorization header access", "P4 forbids token/header reads.", "Future security-reviewed token propagation gate"),
        new("Portal HTTP client", "P4 is contract-only and must not call Portal.", "Future Portal integration runtime gate"),
        new("CRM-owned login or identity", "PortalCorporativo owns identity and permissions.", "Not planned for CRM"),
        new("Locked stub runtime registration", "Locked stubs remain blocked until next gate.", NextGate)
    ];
}
