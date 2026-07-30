namespace CRM.Application.Foundation;

public sealed class CrmPortalAuthRuntimeValidationTrialStatusService
{
    public const string StatusName = "PortalAuthRuntimeValidationTrial";
    public const string BaseUrlSecretName = "crm-portal-auth-base-url";
    public const string ClientIdSecretName = "crm-portal-auth-client-id";
    public const string ClientSecretName = "crm-portal-auth-client-secret";
    public const string WarningText = "Portal Auth runtime validation trial is disabled by default and never reads authorization headers or tokens";
    public const string NextGate = "Sprint9P5ProductiveRouteDryRunTrial";

    public static readonly IReadOnlyCollection<string> ApprovedSecretNames =
    [
        BaseUrlSecretName,
        ClientIdSecretName,
        ClientSecretName
    ];

    public CrmPortalAuthRuntimeValidationTrialStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            PortalAuthRuntimeValidationTrialExists: true,
            PortalAuthRuntimeValidationTrialApproved: true,
            PortalAuthRuntimeValidationTrialEnabled: false,
            PortalAuthValidationAttempted: false,
            PortalAuthValidated: false,
            PortalHttpAttempted: false,
            PortalHttpConfigured: false,
            PortalAuthUrlResolved: false,
            PortalAuthUrlReturnedToApi: false,
            PortalClientSecretResolved: false,
            PortalClientSecretReturnedToApi: false,
            AuthHeaderRead: false,
            TokenRead: false,
            TokenStored: false,
            ClaimsMapped: false,
            ProductiveAuthEnabled: false,
            LoginEndpointCreated: false,
            LogoutEndpointCreated: false,
            IdentityRuntimeEnabled: false,
            AuthAttributeEnabled: false,
            SecretProviderMetadataDependencyValidated: true,
            CommonDbMetadataDependencyValidated: true,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            RollbackAvailable: true,
            ObservabilityMetadataOnly: true,
            NextGate: NextGate,
            Warning: WarningText,
            ApprovedSecretNames: ApprovedSecretNames,
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "A future explicit NonProduction probe must keep Portal URLs, client secrets and tokens inside infrastructure boundaries.",
                "CRM must not read Authorization headers or user tokens by default.",
                "P5 productive route dry-run must remain locked and must not activate productive CRUD by default."
            ]);

    public IReadOnlyCollection<CrmPortalAuthRuntimeValidationTrialGateContract> GetGates() =>
    [
        new("Secret Provider P2 metadata", true, true, "P4 consumes only sanitized Secret Provider metadata."),
        new("Common DB P3 metadata", true, true, "P4 consumes only sanitized Common DB trial metadata."),
        new("Crm:RuntimeTrials:PortalAuthValidationEnabled", true, false, "Flag is false by default."),
        new("NonProductionOnly", true, true, "Production is blocked."),
        new("No authorization header or token reads", true, true, "Default status and probe do not inspect request headers or tokens."),
        new("No CRM-owned Identity", true, true, "PortalCorporativo remains the owner of Auth and Security.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRuntimeValidationTrialObservationContract> GetObservations() =>
    [
        new("Default disabled", true, "PortalAuthRuntimeValidationTrialEnabled=false."),
        new("No Portal HTTP by default", true, "PortalHttpAttempted=false and PortalHttpConfigured=false."),
        new("No Portal URL exposure", true, "PortalAuthUrlReturnedToApi=false."),
        new("No client secret exposure", true, "PortalClientSecretReturnedToApi=false."),
        new("No token/header reads", true, "AuthHeaderRead=false and TokenRead=false."),
        new("No CRM auth runtime", true, "ProductiveAuthEnabled=false and IdentityRuntimeEnabled=false.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRuntimeValidationTrialBlockedItemContract> GetBlockedItems() =>
    [
        new("Default Portal Auth validation", "The explicit NonProduction flag is disabled.", NextGate),
        new("Production Auth validation", "Production remains blocked.", NextGate),
        new("Authorization header and token reads", "P4 does not read user request tokens by default.", NextGate),
        new("Login/logout and Identity", "CRM must not duplicate Portal Security/Auth capabilities.", NextGate)
    ];
}
