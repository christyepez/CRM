namespace CRM.Application.Foundation;

public sealed class CrmLockedProductiveRouteRuntimeRegistrationStatusService
{
    public const string StatusName = "LockedProductiveRouteRuntimeRegistrationWith423";
    public const string NextGate = "Sprint7P6Sprint7GateDecision";
    public const string WarningText = "Locked productive routes are not registered by default; explicit NonProduction flag returns 423 without side effects";

    private static readonly string[] LockedMethods = ["GET", "POST", "PUT", "PATCH"];
    private static readonly string[] PlannedRoutes = ["/api/crm/leads", "/api/crm/accounts", "/api/crm/contacts"];

    public CrmLockedProductiveRouteRuntimeRegistrationStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            LockedProductiveRouteRuntimeRegistrationExists: true,
            LockedProductiveRouteRuntimeRegistrationApprovalGranted: false,
            LockedProductiveRouteRuntimeRegistrationEnabled: false,
            ProductiveRoutesRegisteredByDefault: false,
            ProductiveRoutesRegisteredWhenExplicitlyEnabled: true,
            DefaultNegativeRouteStatus: 404,
            ExplicitlyEnabledLockedRouteStatus: 423,
            ProductiveCrudEnabled: false,
            ProductiveDomainExecutionEnabled: false,
            ProductivePersistenceEnabled: false,
            DeleteEndpointsEnabled: false,
            PortalAuthRuntimeRequired: false,
            PortalAuthRuntimeEnabled: false,
            TokenReadAttempted: false,
            HeaderReadAttempted: false,
            DbRuntimeEnabled: false,
            EfRuntimeEnabled: false,
            MigrationsCreated: false,
            SideEffectsAllowed: false,
            NonProductionOnly: true,
            RollbackRequired: true,
            ObservabilityRequired: true,
            NextGate: NextGate,
            Warning: WarningText,
            Routes: GetRoutes(),
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "Locked route registration must remain opt-in and NonProduction-only.",
                "423 stubs must not become a hidden productive CRUD implementation.",
                "Future activation still depends on Portal Auth, common DB, persistence, observability, rollback, security and QA approvals."
            ]);

    public IReadOnlyCollection<CrmLockedProductiveRouteContract> GetRoutes() =>
        PlannedRoutes
            .Select(route => new CrmLockedProductiveRouteContract(route, LockedMethods, 404, 423, false, false, false, false))
            .ToArray();

    public IReadOnlyCollection<CrmLockedProductiveRouteGateContract> GetGates() =>
    [
        new("Explicit NonProduction flag", true, false, "Default remains disabled."),
        new("Portal Auth runtime approval", true, false, "Portal Auth is not required or active in P5."),
        new("Common DB runtime approval", true, false, "No database runtime is active in P5."),
        new("Productive authorization policy", true, false, "Productive authorization remains deferred."),
        new("Security and QA sign-off", true, false, "Required before any route can leave 423 Locked.")
    ];

    public IReadOnlyCollection<CrmLockedProductiveRouteObservationContract> GetObservations() =>
    [
        new("Default routes stay negative", true, "Productive CRM routes return 404 unless the explicit NonProduction flag is enabled."),
        new("Locked responses are side-effect-free", true, "423 stubs do not call domain services, stores, databases or Portal runtime."),
        new("DELETE remains unavailable", true, "No DELETE route is registered by the P5 registrar."),
        new("Rollback is immediate", true, "Disable the flag and routes return to 404.")
    ];

    public IReadOnlyCollection<CrmLockedProductiveRouteBlockedItemContract> GetBlockedItems() =>
    [
        new("Productive CRUD", "CRUD remains disabled until Sprint 7 P6 gate decision.", NextGate),
        new("Productive persistence", "DB runtime and EF runtime approvals are not granted.", NextGate),
        new("Portal authorization runtime", "Portal Auth runtime approval is not granted.", NextGate),
        new("DELETE endpoints", "DELETE remains out of scope for foundation route locking.", NextGate)
    ];
}
