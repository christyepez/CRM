namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderControlledRealReadStatusService
{
    public const string StatusName = "SecretProviderControlledRealNonProductionRead";
    public const string WarningText = "Controlled real secret read is disabled by default and never returns secret values";
    public const string NextGate = "Sprint8P3CommonDbControlledRealConnectivity";

    private static readonly string[] ApprovedNames =
    [
        "crm-common-db-connection",
        "crm-portal-auth-base-url",
        "crm-portal-auth-client-id",
        "crm-portal-auth-client-secret",
        "crm-observability-endpoint"
    ];

    public CrmSecretProviderControlledRealReadStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            SecretProviderControlledRealNonProductionReadExists: true,
            SecretProviderControlledRealNonProductionReadApproved: true,
            SecretProviderControlledRealNonProductionReadEnabled: false,
            SecretProviderControlledRealNonProductionReadAttempted: false,
            RealSecretReadAttempted: false,
            RealSecretValueMaterialized: false,
            RealSecretValueLogged: false,
            SecretValueReturnedToApi: false,
            SecretValuePersisted: false,
            SecretValueCached: false,
            KeyVaultRuntimeClientCreated: false,
            KeyVaultRuntimeCallAttempted: false,
            AzureSecretSdkRuntimeEnabled: false,
            UsesApprovedSecretNamesOnly: true,
            NonProductionOnly: true,
            FailClosedByDefault: true,
            NextGate: NextGate,
            Warning: WarningText,
            Secrets: GetSecrets(),
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "A future non-production provider must prove redaction without exposing values.",
                "Production activation remains NoGo until a separate approval gate.",
                "P3 may consume only sanitized availability metadata, never secret values."
            ]);

    public IReadOnlyCollection<CrmSecretProviderControlledRealReadSecretContract> GetSecrets() =>
        ApprovedNames
            .Select(name => new CrmSecretProviderControlledRealReadSecretContract(name, true, false, false))
            .ToArray();

    public IReadOnlyCollection<CrmSecretProviderControlledRealReadGateContract> GetGates() =>
    [
        new("NonProduction environment", true, true, "Controlled read is scoped to NonProduction only."),
        new("Explicit enable flag", true, false, "Default runtime remains disabled and fail-closed."),
        new("Approved logical name", true, true, "Only the Sprint 8 approved allow-list is accepted."),
        new("Redaction enabled", true, true, "Only sanitized metadata can leave the runtime boundary."),
        new("No API value return", true, true, "The public contract contains no value field.")
    ];

    public IReadOnlyCollection<CrmSecretProviderControlledRealReadObservationContract> GetObservations() =>
    [
        new("Default disabled", true, "SecretProviderControlledRealNonProductionReadEnabled=false."),
        new("No value materialized", true, "RealSecretValueMaterialized=false."),
        new("No value returned", true, "SecretValueReturnedToApi=false."),
        new("No persistence", true, "SecretValuePersisted=false."),
        new("No cache", true, "SecretValueCached=false."),
        new("No DB/Auth/Portal runtime", true, "P2 exposes foundation metadata only.")
    ];

    public IReadOnlyCollection<CrmSecretProviderControlledRealReadBlockedItemContract> GetBlockedItems() =>
    [
        new("Default real read", "The explicit NonProduction flag is off.", NextGate),
        new("Production secret provider", "Production remains NoGo.", NextGate),
        new("Secret value exposure", "Values cannot be returned, logged, cached or persisted.", NextGate),
        new("DB/Auth/Portal runtime activation", "Separate Sprint 8 gates are required.", NextGate)
    ];
}
