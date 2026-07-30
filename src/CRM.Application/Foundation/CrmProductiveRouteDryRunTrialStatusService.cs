namespace CRM.Application.Foundation;

public sealed class CrmProductiveRouteDryRunTrialStatusService
{
    public const string StatusName = "ProductiveRouteDryRunTrial";
    public const string WarningText = "Productive route dry-run trial is disabled by default and never registers productive CRM routes";
    public const string NextGate = "Sprint9P6Sprint9GateDecision";

    public CrmProductiveRouteDryRunTrialStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            ProductiveRouteDryRunTrialExists: true,
            ProductiveRouteDryRunTrialApproved: true,
            ProductiveRouteDryRunTrialEnabled: false,
            ProductiveRoutesRegisteredByDefault: false,
            ProductiveRoutesDryRunRegistered: false,
            ProductiveRouteDryRunAttempted: false,
            ProductiveRouteDryRunAllowed: false,
            ProductiveRouteDryRunDecisionReturned: false,
            ProductiveRouteDryRunStatusCode: 423,
            ProductiveCrudEnabled: false,
            ProductiveDomainExecutionEnabled: false,
            ProductivePersistenceEnabled: false,
            DatabaseWriteAttempted: false,
            SideEffectsAllowed: false,
            DeleteEndpointsEnabled: false,
            DbRuntimeEnabled: false,
            EfRuntimeEnabled: false,
            MigrationsEnabled: false,
            SchemaChangeAllowed: false,
            PortalAuthMetadataDependencyValidated: true,
            CommonDbMetadataDependencyValidated: true,
            SecretProviderMetadataDependencyValidated: true,
            AuthHeaderRead: false,
            TokenRead: false,
            TokenStored: false,
            AuthAttributeEnabled: false,
            LoginEndpointCreated: false,
            LogoutEndpointCreated: false,
            IdentityRuntimeEnabled: false,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            RollbackAvailable: true,
            ObservabilityMetadataOnly: true,
            NextGate: NextGate,
            Warning: WarningText,
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "Future route registration must remain explicit and NonProduction-only until Sprint 9 gate decision.",
                "P5 consumes only P2/P3/P4 metadata and must not call Secret Provider, DB or Portal Auth runtime directly.",
                "Productive CRM routes must continue returning 404 by default."
            ]);

    public IReadOnlyCollection<CrmProductiveRouteDryRunTrialGateContract> GetGates() =>
    [
        new("Secret Provider P2 metadata", true, true, "P5 consumes only sanitized Secret Provider metadata."),
        new("Common DB P3 metadata", true, true, "P5 consumes only sanitized Common DB metadata."),
        new("Portal Auth P4 metadata", true, true, "P5 consumes only sanitized Portal Auth metadata."),
        new("Crm:RuntimeTrials:ProductiveRouteDryRunEnabled", true, false, "Flag is false by default."),
        new("Productive route registration", true, false, "Productive routes remain unregistered by default."),
        new("No side effects", true, true, "Dry-run does not execute domain, persistence, DB, Auth enforcement or DELETE.")
    ];

    public IReadOnlyCollection<CrmProductiveRouteDryRunTrialObservationContract> GetObservations() =>
    [
        new("Default disabled", true, "ProductiveRouteDryRunTrialEnabled=false."),
        new("Default route status", true, "Productive routes remain 404 when no explicit NonProduction registration is present."),
        new("Default probe status", true, "Probe returns 423 Locked when disabled or blocked."),
        new("No persistence", true, "DatabaseWriteAttempted=false and ProductivePersistenceEnabled=false."),
        new("No Auth enforcement", true, "AuthHeaderRead=false, TokenRead=false and AuthAttributeEnabled=false.")
    ];

    public IReadOnlyCollection<CrmProductiveRouteDryRunTrialBlockedItemContract> GetBlockedItems() =>
    [
        new("Default productive route dry-run", "The explicit NonProduction flag is disabled.", NextGate),
        new("Production route dry-run", "Production remains blocked.", NextGate),
        new("Productive CRUD", "P5 does not execute CRM domain operations.", NextGate),
        new("DELETE routes", "DELETE remains unavailable.", NextGate),
        new("DB writes", "P5 does not write to the common database.", NextGate),
        new("Portal Auth enforcement", "P5 consumes P4 metadata only and does not enforce Auth.", NextGate)
    ];
}
