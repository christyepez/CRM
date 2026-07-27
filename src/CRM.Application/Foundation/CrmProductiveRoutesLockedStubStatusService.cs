namespace CRM.Application.Foundation;

public sealed class CrmProductiveRoutesLockedStubStatusService
{
    public const string WarningText = "Productive routes locked stub validation only; no productive routes are active";
    public const string NextGate = "Sprint4P5NonProductionE2EPilotReadiness";

    public CrmProductiveRoutesLockedStubStatusResponse GetStatus() =>
        new(
            "CRM",
            "ProductiveRoutesLockedStubValidation",
            true,
            "DocumentOnlyPreferred",
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
            GetFutureRoutes(),
            GetSafetyGates(),
            new("DocumentOnly", "DocumentOnlyPreferred", "Future productive route shapes are documented, but no runtime stubs are registered by default."),
            [
                "Future productive route stubs could be mistaken for active APIs if registered before DB and Portal Auth gates.",
                "Foundation CRUD must stay separate and must not become a hidden productive API.",
                "DELETE remains blocked until explicit productization approval.",
                "P5 must prove non-production E2E readiness without activating productive CRUD."
            ]);

    public IReadOnlyCollection<CrmProductiveRouteStubContract> GetFutureRoutes() =>
    [
        new("GET", "/api/crm/leads", "FutureDocumentedOnly", false, false),
        new("GET", "/api/crm/leads/{id}", "FutureDocumentedOnly", false, false),
        new("POST", "/api/crm/leads", "FutureDocumentedOnly", false, false),
        new("PUT", "/api/crm/leads/{id}", "FutureDocumentedOnly", false, false),
        new("GET", "/api/crm/accounts", "FutureDocumentedOnly", false, false),
        new("GET", "/api/crm/accounts/{id}", "FutureDocumentedOnly", false, false),
        new("POST", "/api/crm/accounts", "FutureDocumentedOnly", false, false),
        new("PUT", "/api/crm/accounts/{id}", "FutureDocumentedOnly", false, false),
        new("GET", "/api/crm/contacts", "FutureDocumentedOnly", false, false),
        new("GET", "/api/crm/contacts/{id}", "FutureDocumentedOnly", false, false),
        new("POST", "/api/crm/contacts", "FutureDocumentedOnly", false, false),
        new("PUT", "/api/crm/contacts/{id}", "FutureDocumentedOnly", false, false)
    ];

    public IReadOnlyCollection<CrmProductiveRouteStubSafetyGateContract> GetSafetyGates() =>
    [
        new("Portal Auth runtime approved", "NoGo", false, "Complete Portal Auth runtime probe and signed contract gates."),
        new("Common DB runtime approved", "NoGo", false, "Approve durable persistence, migrations and rollback plan."),
        new("Productive authorization approved", "NoGo", false, "Approve policy enforcement before any productive endpoint."),
        new("Route registration approved", "NoGo", false, "Explicitly approve route registration strategy before stubs or real routes."),
        new("DELETE approval", "NoGo", false, "DELETE remains blocked until later productization review.")
    ];
}
