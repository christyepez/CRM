namespace CRM.Application.Portal;

public sealed class CrmPortalAuthRuntimeContractStatusService
{
    public const string WarningText = "Portal Auth runtime contract validation only; no real Auth runtime configured";

    public CrmPortalAuthRuntimeContractStatusResponse GetStatus() =>
        new(
            "CRM",
            "PortalAuthRuntimeContractValidation",
            true,
            false,
            false,
            false,
            "PortalCorporativo",
            false,
            false,
            false,
            false,
            true,
            false,
            "Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag",
            WarningText,
            GetCapabilities(),
            GetContextMappings(),
            GetGates(),
            GetNoGoPolicies(),
            [
                "Portal Auth runtime contract can be mistaken for active authentication if middleware is added too early.",
                "CRM must not persist roles or permissions that belong to PortalCorporativo.",
                "Productive routes remain blocked until Portal runtime context, tenant and policy result are validated."
            ]);

    public IReadOnlyCollection<CrmPortalAuthCapabilityContract> GetCapabilities() =>
    [
        new("crm.foundation.read", "Foundation", "Required", false, "PortalCorporativo"),
        new("crm.foundation.preview.write", "Foundation", "Required", false, "PortalCorporativo"),
        new("crm.foundation.preview.clear", "Foundation", "Required", false, "PortalCorporativo"),
        new("crm.readiness.read", "Foundation", "Required", false, "PortalCorporativo"),
        new("crm.leads.read", "FutureProductive", "Future", false, "PortalCorporativo"),
        new("crm.leads.write", "FutureProductive", "Future", false, "PortalCorporativo"),
        new("crm.accounts.read", "FutureProductive", "Future", false, "PortalCorporativo"),
        new("crm.accounts.write", "FutureProductive", "Future", false, "PortalCorporativo"),
        new("crm.contacts.read", "FutureProductive", "Future", false, "PortalCorporativo"),
        new("crm.contacts.write", "FutureProductive", "Future", false, "PortalCorporativo")
    ];

    public IReadOnlyCollection<CrmPortalAuthContextMappingContract> GetContextMappings() =>
    [
        new("Portal user id", "CrmUserContext.UserId", "ContractOnly", false, "Normalized from Portal runtime; not read in P4."),
        new("Portal tenant id", "CrmUserContext.TenantId", "ContractOnly", false, "Tenant comes from Portal runtime; CRM does not infer or persist tenant authority."),
        new("Portal permissions", "CrmPolicyResult.RequiredCapability", "ContractOnly", false, "Permissions are evaluated by Portal; CRM consumes the policy result."),
        new("Portal claims", "CrmUserContext.NormalizedClaims", "ContractOnly", false, "Claims must be normalized and redacted before application use."),
        new("Correlation id", "CrmCorrelationContext.CorrelationId", "ContractOnly", false, "Correlation id must flow from Portal/Gateway to CRM logs and audit.")
    ];

    public IReadOnlyCollection<CrmPortalAuthRuntimeGateContract> GetGates() =>
    [
        new("PortalRuntimeContract", "ContractOnly", false, "Approve runtime user, tenant, permission, claim and policy result schema."),
        new("AuthMiddleware", "NoGo", false, "Do not add middleware until Portal runtime adapter is approved."),
        new("TokenHandling", "NoGo", false, "CRM must not store or parse credentials; Portal/Gateway owns token handling."),
        new("ProductiveRoutes", "Blocked", false, "Keep productive CRM routes disabled until Portal policy checks are active."),
        new("Sprint3P5", "Next", false, "Draft productive API route shape behind disabled flag only.")
    ];

    public IReadOnlyCollection<CrmPortalAuthNoGoPolicyContract> GetNoGoPolicies() =>
    [
        new("CRM login/logout", false, "PortalCorporativo owns SSO and session lifecycle."),
        new("CRM Identity", false, "CRM must not create Identity or user stores."),
        new("Credential storage", false, "CRM must not store tokens, cookies or credentials."),
        new("Persisted roles/permissions", false, "PortalCorporativo owns roles and permissions."),
        new("Portal HTTP runtime calls", false, "P4 is contract-only and does not call Portal.")
    ];
}
