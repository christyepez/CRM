namespace CRM.Application.Foundation;

public sealed class CrmLockedStubRuntimeRegistrationTrialStatusService
{
    public const string WarningText = "Locked stub runtime registration trial only; no productive routes are registered by default";
    public const string NextGate = "Sprint6P6Sprint6GateDecision";
    public const string RuntimeRegistrationDecision = "DocumentOnlyPreferredWithNoRuntimeRegistration";

    public CrmLockedStubRuntimeRegistrationTrialStatusResponse GetStatus() =>
        new(
            "CRM",
            "LockedStubRuntimeRegistrationTrial",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            404,
            423,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            NextGate,
            WarningText,
            RuntimeRegistrationDecision,
            GetFutureRoutes(),
            GetSafetyGates(),
            GetObservability(),
            GetBlockedItems(),
            [
                "Runtime registration could make productive routes appear active if the default false flag is bypassed.",
                "Future locked stubs must return 423 Locked and never call domain services, stores, DB, Auth or Portal.",
                "Negative route checks must remain mandatory because /api/crm/leads, /api/crm/accounts and /api/crm/contacts should return 404 by default."
            ]);

    public IReadOnlyCollection<CrmLockedStubRuntimeRegistrationRouteContract> GetFutureRoutes() =>
    [
        new("/api/crm/leads", "GET/POST/PUT/PATCH", false, 404, 423, "Not registered in P5; future explicit non-production flag may map 423 Locked without domain execution."),
        new("/api/crm/accounts", "GET/POST/PUT/PATCH", false, 404, 423, "Not registered in P5; future explicit non-production flag may map 423 Locked without domain execution."),
        new("/api/crm/contacts", "GET/POST/PUT/PATCH", false, 404, 423, "Not registered in P5; future explicit non-production flag may map 423 Locked without domain execution.")
    ];

    public IReadOnlyCollection<CrmLockedStubRuntimeRegistrationSafetyGateContract> GetSafetyGates() =>
    [
        new("Runtime registration approval", true, false, "No runtime registration is approved in P5."),
        new("Default negative routes", true, true, "Productive routes remain unregistered and return 404."),
        new("No DELETE", true, true, "DELETE endpoints remain prohibited even for future locked stubs."),
        new("No domain or store access", true, true, "P5 does not use domain services or foundation stores."),
        new("No DB/Auth/Portal runtime", true, true, "P5 does not require database, authorization runtime, Portal calls or token/header reads.")
    ];

    public IReadOnlyCollection<CrmLockedStubRuntimeRegistrationObservabilityContract> GetObservability() =>
    [
        new("Foundation trial endpoint", true, true, "Reports the trial decision and default disabled state."),
        new("Negative route checks", true, true, "Leads, accounts and contacts productive routes must return 404."),
        new("Future locked status evidence", true, false, "423 Locked is documented for a future explicit enablement only."),
        new("Runtime registration flags", true, true, "registered/enabled/productive flags are all false.")
    ];

    public IReadOnlyCollection<CrmLockedStubRuntimeRegistrationBlockedItemContract> GetBlockedItems() =>
    [
        new("Runtime route registration", "Registration approval remains false.", "Future explicit runtime registration gate"),
        new("Productive CRUD", "Productive routes and CRUD remain disabled.", "Future productization approval"),
        new("DELETE endpoints", "DELETE remains No-Go.", "Future explicit DELETE approval"),
        new("Domain/store execution", "P5 is contract-only and does not call CRM domain services or stores.", "Future productive route implementation gate"),
        new("Sprint 6 closure", "P6 must review evidence and decide next path.", NextGate)
    ];
}
