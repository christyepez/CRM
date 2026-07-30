namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderRuntimeEnablementTrialStatusService
{
    public const string StatusName = "SecretProviderRuntimeEnablementTrial";
    public const string NextGate = "Sprint9P3CommonDbRuntimeConnectivityTrial";
    public const string WarningText = "Secret Provider runtime trial is disabled by default and never returns secret values";

    public static readonly IReadOnlyCollection<string> AllowedLogicalSecretNames =
    [
        "crm-common-db-connection",
        "crm-portal-auth-base-url",
        "crm-portal-auth-client-id",
        "crm-portal-auth-client-secret",
        "crm-observability-endpoint"
    ];

    public CrmSecretProviderRuntimeEnablementTrialStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            SecretProviderRuntimeEnablementTrialExists: true,
            SecretProviderRuntimeEnablementTrialApproved: true,
            SecretProviderRuntimeEnablementTrialEnabled: false,
            SecretProviderRuntimeTrialAttempted: false,
            SecretProviderRuntimeConnected: false,
            RealSecretReadAttempted: false,
            RealSecretValueMaterialized: false,
            RealSecretValueLogged: false,
            SecretValueReturnedToApi: false,
            SecretValuePersisted: false,
            SecretValueCached: false,
            AllowedLogicalSecretNamesEnforced: true,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            RollbackAvailable: true,
            ObservabilityMetadataOnly: true,
            NextGate: NextGate,
            Warning: WarningText,
            AllowedLogicalSecretNames: AllowedLogicalSecretNames,
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "The explicit NonProduction flag must not be enabled without Security, DevOps, Architecture and QA evidence.",
                "P3 may consume only sanitized availability metadata, never secret values.",
                "Production remains blocked for Secret Provider runtime trials."
            ]);

    public IReadOnlyCollection<CrmSecretProviderRuntimeEnablementTrialGateContract> GetGates() =>
    [
        new("Crm:RuntimeTrials:SecretProviderEnabled", true, false, "Flag is false by default."),
        new("NonProductionOnly", true, true, "Production is blocked."),
        new("AllowedLogicalSecretNamesEnforced", true, true, "Only approved logical names are accepted."),
        new("MetadataOnly", true, true, "API responses never include secret values."),
        new("RollbackAvailable", true, true, "Rollback is disabling the explicit flag.")
    ];

    public IReadOnlyCollection<CrmSecretProviderRuntimeEnablementTrialObservationContract> GetObservations() =>
    [
        new("RealSecretReadAttempted", false, "No secret read is attempted by default."),
        new("SecretValueReturnedToApi", false, "Public contracts expose sanitized metadata only."),
        new("SecretValueLogged", false, "Secret values must never be logged."),
        new("SecretValuePersisted", false, "Secret values must never be persisted."),
        new("SecretValueCached", false, "Secret values must never be cached.")
    ];

    public IReadOnlyCollection<CrmSecretProviderRuntimeEnablementTrialBlockedItemContract> GetBlockedItems() =>
    [
        new("Production Secret Provider runtime", "ProductionActivationDecision remains NoGo."),
        new("Secret values in API/logs/repo", "P2 permits metadata only."),
        new("DB/Auth/Portal runtime consumption", "Deferred to later gates and may consume metadata only."),
        new("Productive routes, CRUD and DELETE", "Out of scope for P2.")
    ];
}
