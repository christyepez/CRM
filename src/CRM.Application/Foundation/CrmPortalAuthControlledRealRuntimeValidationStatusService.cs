namespace CRM.Application.Foundation;

public sealed class CrmPortalAuthControlledRealRuntimeValidationStatusService
{
    public const string StatusName = "PortalAuthControlledRealRuntimeValidation";
    public const string BaseUrlSecretName = "crm-portal-auth-base-url";
    public const string ClientIdSecretName = "crm-portal-auth-client-id";
    public const string ClientSecretName = "crm-portal-auth-client-secret";
    public const string WarningText = "Portal Auth controlled real runtime validation is disabled by default and never reads request tokens";
    public const string NextGate = "Sprint8P5LockedRouteAuthorizationPolicyIntegration";

    public CrmPortalAuthControlledRealRuntimeValidationStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            PortalAuthControlledRealRuntimeValidationExists: true,
            PortalAuthControlledRealRuntimeValidationApproved: true,
            PortalAuthControlledRealRuntimeValidationEnabled: false,
            PortalAuthRuntimeValidationAttempted: false,
            PortalAuthRuntimeConnected: false,
            SecretProviderAvailabilityMetadataUsed: true,
            PortalAuthBaseUrlResolved: false,
            PortalAuthBaseUrlMaterializedInPublicContract: false,
            PortalAuthBaseUrlLogged: false,
            PortalAuthBaseUrlReturnedToApi: false,
            PortalHttpClientCreated: false,
            PortalHttpCallAttempted: false,
            TokenReadAttempted: false,
            HeaderReadAttempted: false,
            AuthorizationHeaderReadAttempted: false,
            RealTokenMaterialized: false,
            RealTokenLogged: false,
            TokenReturnedToApi: false,
            LoginImplementedByCrm: false,
            LogoutImplementedByCrm: false,
            IdentityImplementedByCrm: false,
            RolesPersistedInCrm: false,
            PermissionsPersistedInCrm: false,
            ProductiveAuthorizationEnabled: false,
            ApiRequiresPortalAuth: false,
            NonProductionOnly: true,
            FailClosedByDefault: true,
            NextGate: NextGate,
            Warning: WarningText,
            Probe: GetProbe(),
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "A future explicit NonProduction validation must keep Portal URLs and credentials inside the infrastructure boundary.",
                "CRM must not read Authorization headers or user tokens during this validation.",
                "P5 locked-route authorization policy integration must not activate productive CRUD by default."
            ]);

    public CrmPortalAuthControlledRuntimeValidationProbeContract GetProbe() =>
        new(
            ProbeAttempted: false,
            ProviderConfigured: false,
            PortalAuthMetadataAvailable: false,
            PortalAuthValidationAttempted: false,
            PortalAuthReachable: false,
            TimeoutApplied: true,
            TimeoutSeconds: 3,
            PortalUrlReturned: false,
            SecretValueReturned: false,
            TokenReturned: false,
            HeaderReadAttempted: false);

    public IReadOnlyCollection<CrmPortalAuthControlledRuntimeValidationGateContract> GetGates() =>
    [
        new("Secret Provider P2 metadata", true, true, "P4 depends on approved sanitized Secret Provider availability metadata."),
        new("Approved logical secret names", true, true, $"{BaseUrlSecretName}, {ClientIdSecretName} and {ClientSecretName} are the only valid names."),
        new("Explicit NonProduction enable flag", true, false, "Default remains disabled and fail-closed."),
        new("No request token/header reads", true, true, "The foundation endpoint does not inspect request headers or user tokens."),
        new("No CRM-owned auth", true, true, "No login, logout, Identity, roles or permissions persistence are added.")
    ];

    public IReadOnlyCollection<CrmPortalAuthControlledRuntimeValidationObservationContract> GetObservations() =>
    [
        new("Default disabled", true, "PortalAuthControlledRealRuntimeValidationEnabled=false."),
        new("No Portal HTTP by default", true, "PortalHttpClientCreated=false and PortalHttpCallAttempted=false."),
        new("No URL exposure", true, "PortalAuthBaseUrlReturnedToApi=false and PortalAuthBaseUrlLogged=false."),
        new("No token exposure", true, "TokenReturnedToApi=false and RealTokenLogged=false."),
        new("No CRM auth runtime", true, "ProductiveAuthorizationEnabled=false and ApiRequiresPortalAuth=false."),
        new("No persisted permissions", true, "RolesPersistedInCrm=false and PermissionsPersistedInCrm=false.")
    ];

    public IReadOnlyCollection<CrmPortalAuthControlledRuntimeValidationBlockedItemContract> GetBlockedItems() =>
    [
        new("Default Portal Auth validation", "The explicit NonProduction flag is off.", NextGate),
        new("Production validation", "Production remains NoGo.", NextGate),
        new("Token/header reads", "CRM cannot read user request tokens in P4.", NextGate),
        new("CRM-owned Identity", "PortalCorporativo remains the owner of Security/Auth capabilities.", NextGate)
    ];
}
