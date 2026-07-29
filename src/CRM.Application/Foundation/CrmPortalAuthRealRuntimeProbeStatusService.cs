namespace CRM.Application.Foundation;

public sealed class CrmPortalAuthRealRuntimeProbeStatusService
{
    public const string SyntheticPortalAuthReference = "mock://crm/portal-auth";
    public const string SyntheticUserReference = "mock://crm/portal-user";
    public const string WarningText = "Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted";
    public const string NextGate = "Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423";

    public CrmPortalAuthRealRuntimeProbeStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: "PortalAuthRealRuntimeProbe",
            FoundationMode: true,
            PortalAuthRealRuntimeProbeExists: true,
            PortalAuthRealRuntimeApprovalGranted: false,
            SecretProviderRealNonProductionApprovalGranted: false,
            PortalAuthRealRuntimeProbeEnabled: false,
            PortalAuthRealRuntimeProbeAttempted: false,
            PortalAuthRuntimeConnected: false,
            PortalAuthBaseUrlResolved: false,
            PortalAuthBaseUrlMaterialized: false,
            PortalAuthBaseUrlLogged: false,
            PortalAuthBaseUrlReturnedToApi: false,
            PortalHttpClientCreated: false,
            PortalHttpCallAttempted: false,
            PortalAuthTokenValidationAttempted: false,
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
            UsesSyntheticFallback: true,
            SyntheticPortalAuthReference: SyntheticPortalAuthReference,
            SyntheticUserReference: SyntheticUserReference,
            ProbeSkippedBecausePortalAuthApprovalNotGranted: true,
            NonProductionOnly: true,
            RollbackRequired: true,
            ObservabilityRequired: true,
            NextGate: NextGate,
            Warning: WarningText,
            Dependencies: GetDependencies(),
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "Portal Auth runtime must not run until Portal owner, security, architecture and DevOps approvals are granted.",
                "CRM must not duplicate PortalCorporativo Auth, Identity, roles, permissions, login or token storage.",
                "Token and header reads remain forbidden until a future approved runtime gate."
            ]);

    public IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeDependencyContract> GetDependencies() =>
    [
        new("Portal owner approval", true, false, "Approval not granted."),
        new("Secret Provider real NonProduction approval", true, false, "Approval not granted."),
        new("Logical secret name crm-portal-auth-base-url", true, true, "Name only; value not resolved."),
        new("Redacted observability", true, false, "Required before any future runtime attempt."),
        new("Rollback plan", true, true, "Documented; runtime still disabled.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeGateContract> GetGates() =>
    [
        new("Security approval", true, false, "No token/header read has been approved."),
        new("Architecture approval", true, false, "Auth/Identity ownership remains in PortalCorporativo."),
        new("DevOps approval", true, false, "Portal URL, network and timeout policy remain pending."),
        new("Portal owner approval", true, false, "Portal Auth runtime contract is not activated."),
        new("Observability validation", true, false, "Logs must prove no tokens or base URLs are exposed.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeObservationContract> GetObservations() =>
    [
        new("Portal Auth probe skipped", true, "Portal Auth approval is not granted."),
        new("Synthetic fallback used", true, SyntheticPortalAuthReference),
        new("Synthetic user only", true, SyntheticUserReference),
        new("No token/header/runtime access", true, "Only safe metadata is returned.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRealRuntimeProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Portal Auth base URL resolution", "Portal Auth approval is not granted.", NextGate),
        new("Portal HTTP runtime probe", "HTTP client and HTTP calls remain disabled.", NextGate),
        new("Token/header reads", "Security approval is not granted.", NextGate),
        new("Productive authorization", "CRM must not enable Auth middleware yet.", NextGate),
        new("Productive CRM routes", "Productization remains NotReady.", NextGate)
    ];
}
