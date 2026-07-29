using Microsoft.AspNetCore.Http.HttpResults;

namespace CRM.Api.ProductiveRoutes;

public static class LockedProductiveRouteRuntimeRegistration
{
    public const string FlagPath = "Crm:ProductiveRoutes:LockedRegistrationEnabled";
    public const string LockedCode = "CRM_PRODUCTIVE_ROUTE_LOCKED";
    public const string NextGate = "Sprint7P6Sprint7GateDecision";
    public const string LockedMessage = "Productive CRM route is registered only as a locked NonProduction stub";

    private static readonly string[] Routes =
    [
        "/api/crm/leads",
        "/api/crm/accounts",
        "/api/crm/contacts"
    ];

    public static bool TryMapLockedProductiveRoutes(this WebApplication app)
    {
        var enabled = app.Configuration.GetValue<bool>(FlagPath);
        if (!enabled || app.Environment.IsProduction())
        {
            return false;
        }

        foreach (var route in Routes)
        {
            app.MapGet(route, () => Locked(route, "GET"));
            app.MapPost(route, () => Locked(route, "POST"));
            app.MapPut(route, () => Locked(route, "PUT"));
            app.MapPatch(route, () => Locked(route, "PATCH"));
        }

        return true;
    }

    private static JsonHttpResult<LockedProductiveRouteResponse> Locked(string route, string method) =>
        TypedResults.Json(
            new LockedProductiveRouteResponse(
                Status: "Locked",
                Code: LockedCode,
                Route: route,
                Method: method,
                Message: LockedMessage,
                SideEffectsAllowed: false,
                ProductiveCrudEnabled: false,
                DomainExecutionEnabled: false,
                PersistenceEnabled: false,
                PortalAuthRuntimeEnabled: false,
                NextGate: NextGate),
            statusCode: StatusCodes.Status423Locked);
}

public sealed record LockedProductiveRouteResponse(
    string Status,
    string Code,
    string Route,
    string Method,
    string Message,
    bool SideEffectsAllowed,
    bool ProductiveCrudEnabled,
    bool DomainExecutionEnabled,
    bool PersistenceEnabled,
    bool PortalAuthRuntimeEnabled,
    string NextGate);
