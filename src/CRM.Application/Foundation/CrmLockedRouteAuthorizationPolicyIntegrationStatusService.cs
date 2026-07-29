namespace CRM.Application.Foundation;

public sealed class CrmLockedRouteAuthorizationPolicyIntegrationStatusService
{
    public CrmLockedRouteAuthorizationPolicyIntegrationStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: "LockedRouteAuthorizationPolicyIntegration",
            FoundationMode: true,
            LockedRouteAuthorizationPolicyIntegrationExists: true,
            LockedRouteAuthorizationPolicyIntegrationApproved: true,
            LockedRouteAuthorizationPolicyIntegrationEnabled: false,
            AuthorizationPolicyEvaluated: false,
            AuthorizationPolicyDecision: "NotEvaluatedBecauseDisabled",
            PortalAuthMetadataUsed: true,
            PortalAuthRuntimeRequired: false,
            PortalAuthRuntimeConnected: false,
            TokenReadAttempted: false,
            HeaderReadAttempted: false,
            AuthorizationHeaderReadAttempted: false,
            PortalHttpCallAttempted: false,
            ProductiveRoutesRegisteredByDefault: false,
            DefaultNegativeRouteStatus: 404,
            LockedRoutesEnabledOnlyWithExplicitNonProductionFlag: true,
            LockedRouteStatus: 423,
            LockedRouteAuthorizationDecisionReturned: false,
            ProductiveCrudEnabled: false,
            ProductiveDomainExecutionEnabled: false,
            ProductivePersistenceEnabled: false,
            DeleteEndpointsEnabled: false,
            SideEffectsAllowed: false,
            DbRuntimeEnabled: false,
            EfRuntimeEnabled: false,
            NonProductionOnly: true,
            FailClosedByDefault: true,
            NextGate: "Sprint8P6Sprint8GateDecision",
            Warning: "Locked route authorization policy is disabled by default and never activates productive CRM routes",
            Routes:
            [
                new("/api/crm/leads", ["GET", "POST", "PUT", "PATCH"], DeleteEnabled: false, DefaultStatus: 404, LockedStatus: 423),
                new("/api/crm/accounts", ["GET", "POST", "PUT", "PATCH"], DeleteEnabled: false, DefaultStatus: 404, LockedStatus: 423),
                new("/api/crm/contacts", ["GET", "POST", "PUT", "PATCH"], DeleteEnabled: false, DefaultStatus: 404, LockedStatus: 423)
            ],
            Gates:
            [
                new("ExplicitNonProductionFlag", Required: true, Passed: false, "Locked routes are not registered by default."),
                new("PortalAuthOwnerApproval", Required: true, Passed: false, "Portal Auth runtime is not connected in P5."),
                new("ProductiveCrudApproval", Required: true, Passed: false, "CRUD execution remains blocked.")
            ],
            Observations:
            [
                new("PortalAuthMetadataUsed", true, "Only safe P4 metadata is consumed."),
                new("PolicyIsPureApplication", true, "No I/O, DB, Portal HTTP, token or header reads."),
                new("DeleteEndpointsEnabled", false, "DELETE remains NoGo.")
            ],
            BlockedItems:
            [
                new("Productive routes", "Routes remain 404 by default and 423 when explicitly locked.", "Sprint8P6Sprint8GateDecision"),
                new("Real authorization", "Portal Auth runtime and real permission policy are not connected.", "Sprint8P6Sprint8GateDecision"),
                new("Persistence", "Common DB runtime remains disabled for productive routes.", "Sprint8P6Sprint8GateDecision")
            ],
            Risks:
            [
                "A future sprint must explicitly approve Portal Auth runtime before productive authorization.",
                "Policy metadata must remain sanitized until Portal owner signs off.",
                "Locked routes must not be confused with productive CRUD availability."
            ]);
}
