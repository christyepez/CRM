namespace CRM.Application.Foundation;

public sealed class CrmLockedProductiveRouteStubTrialStatusService
{
    public const string WarningText = "Locked productive route stub trial only; no productive routes are registered by default";
    public const string NextGate = "Sprint5P6Sprint5GateDecision";
    public const string StubTrialDecision = "DocumentOnlyPreferredWithNoRuntimeRegistration";

    public CrmLockedProductiveRouteStubTrialStatusResponse GetStatus() =>
        new(
            "CRM",
            "LockedProductiveRouteStubTrial",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            423,
            404,
            true,
            false,
            false,
            false,
            NextGate,
            WarningText,
            GetFutureRoutes(),
            GetGates(),
            GetDecisions(),
            GetBlockedItems(),
            [
                "Future stub registration could be confused with productive CRUD if negative route checks are skipped.",
                "Any explicit non-production stub registration must return 423 Locked and avoid domain execution.",
                "Productive routes remain blocked until DB, Auth, Portal and security gates are approved."
            ]);

    public IReadOnlyCollection<CrmLockedProductiveRouteStubContract> GetFutureRoutes() =>
    [
        new("/api/crm/leads", "GET/POST/PUT", false, 423, "Future non-production stub only; return 423 Locked if explicitly enabled."),
        new("/api/crm/accounts", "GET/POST/PUT", false, 423, "Future non-production stub only; return 423 Locked if explicitly enabled."),
        new("/api/crm/contacts", "GET/POST/PUT", false, 423, "Future non-production stub only; return 423 Locked if explicitly enabled.")
    ];

    public IReadOnlyCollection<CrmLockedProductiveRouteStubTrialGateContract> GetGates() =>
    [
        new("Runtime flag approval", "Architecture Governance", true, false, "Explicit non-production approval with default false flag."),
        new("Security approval", "Security", true, false, "Auth remains disabled and no token/header reads occur."),
        new("Persistence approval", "Data Architect", true, false, "No DB, stores, migrations or productive CRUD execution."),
        new("Rollback approval", "DevOps", true, false, "Disable flag and verify routes return 404 again."),
        new("QA negative route evidence", "QA Lead", true, false, "Leads, accounts and contacts productive routes return 404 by default.")
    ];

    public IReadOnlyCollection<CrmLockedProductiveRouteStubTrialDecisionContract> GetDecisions() =>
    [
        new("Stub trial strategy", StubTrialDecision, "Most conservative option: document future locked stubs without runtime registration."),
        new("Default route behavior", "404", "Productive routes are not registered by default."),
        new("Future explicit locked response", "423", "If stubs are enabled later, they must not execute domain logic."),
        new("DELETE behavior", "NoGo", "DELETE endpoints remain prohibited.")
    ];

    public IReadOnlyCollection<CrmLockedProductiveRouteStubBlockedItemContract> GetBlockedItems() =>
    [
        new("Productive route registration", "Registration approval remains false."),
        new("Productive CRUD", "DB, Auth and Portal runtime gates are not approved."),
        new("DELETE endpoints", "DELETE remains NoGo."),
        new("Runtime stub file registration", "P5 uses document-only preferred strategy.")
    ];
}
