namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderRealNonProductionRuntimeProbeStatusService
{
    public const string WarningText = "Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted";
    public const string NextGate = "Sprint7P3CommonDbRealConnectivityNonProductionProbe";

    public CrmSecretProviderRealNonProductionRuntimeProbeStatusResponse GetStatus() =>
        new(
            "CRM",
            "SecretProviderRealNonProductionRuntimeProbe",
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
            false,
            false,
            true,
            false,
            true,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetLogicalSecretNames(),
            GetGates(),
            GetObservations(),
            GetBlockedItems(),
            [
                "Runtime probe existence must not be treated as approval to read real secrets.",
                "The default path validates logical names only and skips all provider calls.",
                "Real secret reads remain blocked until Security, Architecture and DevOps gates are granted."
            ]);

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeSecretContract> GetLogicalSecretNames() =>
    [
        new("crm-common-db-connection", "Future CRM logical database connection reference for NonProduction probes.", true, false, false),
        new("crm-portal-auth-base-url", "Future Portal Auth base endpoint reference for NonProduction probes.", true, false, false),
        new("crm-portal-auth-client-id", "Future Portal Auth client identifier reference for NonProduction probes.", true, false, false),
        new("crm-portal-auth-client-secret", "Future Portal Auth client secret reference for NonProduction probes.", true, false, false),
        new("crm-observability-endpoint", "Future observability endpoint reference for NonProduction probes.", true, false, false)
    ];

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeGateContract> GetGates() =>
    [
        new("Security approval", true, false, "Approval remains false; no value reads are allowed."),
        new("Architecture approval", true, false, "CRM must not own secret infrastructure or duplicate Portal capabilities."),
        new("DevOps approval", true, false, "External secret scope, access policy and rollout remain pending."),
        new("Rollback validation", true, false, "Rollback procedure must be validated before runtime probing."),
        new("Observability validation", true, false, "Logs and metrics must prove no values are exposed.")
    ];

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeObservationContract> GetObservations() =>
    [
        new("Logical secret names validated", true, "Only approved logical names are included."),
        new("Probe skipped because approval not granted", true, "Default behavior does not attempt provider I/O."),
        new("No secret values materialized", true, "Response contains names, booleans and gate metadata only."),
        new("No provider client created", true, "Runtime client creation remains disabled.")
    ];

    public IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Real secret value reads", "Approval is not granted.", NextGate),
        new("Runtime provider connection", "Probe enabled flag remains false.", NextGate),
        new("Common DB real connectivity", "Depends on safe secret provider gate.", NextGate),
        new("Portal Auth runtime", "Portal runtime is outside P2 and remains NoGo.", "Sprint7P4PortalAuthRealRuntimeProbe"),
        new("Productive CRM routes", "Productization remains NotReady.", "Future productization gate")
    ];
}
