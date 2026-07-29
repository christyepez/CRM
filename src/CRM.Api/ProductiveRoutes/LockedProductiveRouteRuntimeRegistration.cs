using Microsoft.AspNetCore.Http.HttpResults;
using CRM.Application.Foundation;

namespace CRM.Api.ProductiveRoutes;

public static class LockedProductiveRouteRuntimeRegistration
{
    public const string FlagPath = "Crm:ProductiveRoutes:LockedRegistrationEnabled";
    public const string AuthorizationPolicyFlagPath = "Crm:ProductiveRoutes:LockedAuthorizationPolicyEnabled";
    public const string LockedCode = "CRM_PRODUCTIVE_ROUTE_LOCKED";
    public const string NextGate = "Sprint8P6Sprint8GateDecision";
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
        var policyEnabled = app.Configuration.GetValue<bool>(AuthorizationPolicyFlagPath);
        if (!enabled || app.Environment.IsProduction())
        {
            return false;
        }

        var evaluator = new CrmLockedRouteAuthorizationPolicyEvaluator();
        var nonProduction = !app.Environment.IsProduction();
        foreach (var route in Routes)
        {
            app.MapGet(route, () => Locked(route, "GET", policyEnabled, nonProduction, evaluator));
            app.MapPost(route, () => Locked(route, "POST", policyEnabled, nonProduction, evaluator));
            app.MapPut(route, () => Locked(route, "PUT", policyEnabled, nonProduction, evaluator));
            app.MapPatch(route, () => Locked(route, "PATCH", policyEnabled, nonProduction, evaluator));
        }

        return true;
    }

    private static JsonHttpResult<LockedProductiveRouteResponse> Locked(
        string route,
        string method,
        bool policyEnabled,
        bool nonProduction,
        CrmLockedRouteAuthorizationPolicyEvaluator evaluator)
    {
        var decision = evaluator.Evaluate(new CrmLockedRouteAuthorizationPolicyEvaluationRequest(
            Route: route,
            Method: method,
            LockedRegistrationEnabled: true,
            LockedAuthorizationPolicyEnabled: policyEnabled,
            NonProduction: nonProduction));

        return TypedResults.Json(
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
                AuthorizationPolicyEvaluated: decision.PolicyEvaluated,
                AuthorizationDecision: decision.Decision,
                PortalAuthRuntimeConnected: false,
                TokenReadAttempted: false,
                HeaderReadAttempted: false,
                AuthorizationHeaderReadAttempted: false,
                PortalHttpCallAttempted: false,
                ProductiveDomainExecutionEnabled: false,
                ProductivePersistenceEnabled: false,
                DeleteEndpointsEnabled: false,
                NextGate: NextGate),
            statusCode: StatusCodes.Status423Locked);
    }
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
    bool AuthorizationPolicyEvaluated,
    string AuthorizationDecision,
    bool PortalAuthRuntimeConnected,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool AuthorizationHeaderReadAttempted,
    bool PortalHttpCallAttempted,
    bool ProductiveDomainExecutionEnabled,
    bool ProductivePersistenceEnabled,
    bool DeleteEndpointsEnabled,
    string NextGate);
