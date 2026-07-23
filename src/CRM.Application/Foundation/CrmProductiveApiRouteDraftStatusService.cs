namespace CRM.Application.Foundation;

public sealed class CrmProductiveApiRouteDraftStatusService
{
    public const string WarningText = "Productive API route draft only; routes are not active";

    public CrmProductiveApiRouteDraftStatusResponse GetStatus() =>
        new(
            "CRM",
            "ProductiveApiRouteDraft",
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
            true,
            "Sprint3P6Sprint3ProductizationReview",
            WarningText,
            GetRoutes(),
            GetActivationGates(),
            GetDisabledPolicies(),
            [
                "Draft routes can be mistaken for active API if registered before authorization and persistence gates.",
                "Foundation CRUD must not be treated as productive CRM behavior.",
                "DELETE remains a separate no-go decision until audit, retention and data policy are approved."
            ]);

    public IReadOnlyCollection<CrmProductiveApiRouteDraftContract> GetRoutes() =>
    [
        Draft("GET", "/api/crm/leads", "Lead"),
        Draft("GET", "/api/crm/leads/{id}", "Lead"),
        Draft("POST", "/api/crm/leads", "Lead"),
        Draft("PUT", "/api/crm/leads/{id}", "Lead"),
        Draft("GET", "/api/crm/accounts", "Account"),
        Draft("GET", "/api/crm/accounts/{id}", "Account"),
        Draft("POST", "/api/crm/accounts", "Account"),
        Draft("PUT", "/api/crm/accounts/{id}", "Account"),
        Draft("GET", "/api/crm/contacts", "Contact"),
        Draft("GET", "/api/crm/contacts/{id}", "Contact"),
        Draft("POST", "/api/crm/contacts", "Contact"),
        Draft("PUT", "/api/crm/contacts/{id}", "Contact")
    ];

    public IReadOnlyCollection<CrmProductiveApiActivationGateContract> GetActivationGates() =>
    [
        new("PortalAuthRuntime", "NoGo", false, "Portal Auth runtime must be approved and connected."),
        new("ProductiveAuthorization", "NoGo", false, "Productive permission policy checks must be active."),
        new("DurablePersistence", "NoGo", false, "Durable persistence must be approved; foundation stores are not productive."),
        new("RealDatabase", "NoGo", false, "Real logical CRM database and secret strategy must be approved."),
        new("AuditObservability", "NoGo", false, "Audit, correlation and observability gates must be approved."),
        new("RollbackBackup", "NoGo", false, "Rollback, backup and restore plan must be approved."),
        new("Sprint3P6", "Next", false, "Run Sprint 3 productization review before any activation.")
    ];

    public IReadOnlyCollection<CrmProductiveApiDisabledPolicyContract> GetDisabledPolicies() =>
    [
        new("Productive routes registered", false, "Productive API routes remain documented only in P5."),
        new("Productive CRUD", false, "Productive CRUD remains disabled."),
        new("DELETE endpoints", false, "DELETE remains NO-GO."),
        new("Foundation stores as productive", false, "Foundation CRUD is separate and non-production."),
        new("Auth runtime bypass", false, "Productive routes require Portal Auth runtime approval.")
    ];

    private static CrmProductiveApiRouteDraftContract Draft(string method, string route, string resource) =>
        new(method, route, resource, "DraftOnly", false, false, "Sprint3P6Sprint3ProductizationReview");
}
