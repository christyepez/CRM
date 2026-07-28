namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderSafeMockActivationStatusService
{
    public const string WarningText = "Secret Provider safe mock only; no real secrets are read";
    public const string NextGate = "Sprint6P3CommonDbConnectivityDryRunContract";

    public CrmSecretProviderSafeMockActivationStatusResponse GetStatus() =>
        new(
            "CRM",
            "SecretProviderSafeMockActivation",
            true,
            true,
            true,
            false,
            false,
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
            NextGate,
            WarningText,
            GetLogicalSecrets(),
            GetSyntheticValues(),
            GetSafetyGates(),
            GetBlockedItems(),
            [
                "Synthetic values must never be copied into real configuration.",
                "P3 must not treat mock common DB values as connection strings.",
                "P4 must not treat mock Portal Auth values as a real Portal base URL or credential."
            ]);

    public IReadOnlyCollection<CrmSecretProviderSafeMockLogicalSecretContract> GetLogicalSecrets() =>
    [
        new("crm.common-db", "Future common DB dry-run contract placeholder.", true, false),
        new("crm.portal-auth-base-url", "Future Portal Auth dry-run contract placeholder.", true, false),
        new("crm.client-id", "Future client identifier placeholder.", true, false),
        new("crm.client-secret", "Future client secret placeholder; synthetic and not sensitive.", true, false),
        new("crm.observability", "Future observability integration placeholder.", true, false)
    ];

    public IReadOnlyCollection<CrmSecretProviderSafeMockValueContract> GetSyntheticValues() =>
    [
        new("crm.common-db", "mock://crm/common-db", true, false, false),
        new("crm.portal-auth-base-url", "mock://crm/portal-auth-base-url", true, false, false),
        new("crm.client-id", "mock-client-id", true, false, false),
        new("crm.client-secret", "mock-client-secret-not-real", true, false, false),
        new("crm.observability", "mock://crm/observability", true, false, false)
    ];

    public IReadOnlyCollection<CrmSecretProviderSafeMockSafetyGateContract> GetSafetyGates() =>
    [
        new("No real secret reads", true, true, "Mock uses deterministic in-memory synthetic values only."),
        new("No .env or file reads", true, true, "No file or environment access is required."),
        new("No Key Vault or Azure SDK", true, true, "No external secret manager client is configured."),
        new("No DB/Auth/Portal runtime", true, true, "Mock values are not runtime usable."),
        new("No value logging", true, true, "Values are contract metadata only and must not be logged as real secrets.")
    ];

    public IReadOnlyCollection<CrmSecretProviderSafeMockBlockedItemContract> GetBlockedItems() =>
    [
        new("Real secret provider", "No Key Vault, Azure SDK or secret manager client is configured.", "Future security approval"),
        new("Common DB dry-run", "Common DB dry-run approval remains false.", NextGate),
        new("Portal Auth dry-run", "Portal Auth dry-run approval remains false.", "Sprint6P4PortalAuthTokenPropagationDryRunContract"),
        new("Real activation", "Real activation approval remains false.", "Future productization gate")
    ];
}
