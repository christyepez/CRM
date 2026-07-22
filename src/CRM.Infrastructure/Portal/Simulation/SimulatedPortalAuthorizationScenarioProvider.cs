using CRM.Application.Portal;
using CRM.Application.Ports.Portal;

namespace CRM.Infrastructure.Portal.Simulation;

public sealed class SimulatedPortalAuthorizationScenarioProvider : IPortalAuthorizationScenarioProvider
{
    private static readonly string[] Permissions =
    [
        "crm.foundation.read",
        "crm.foundation.preview.write",
        "crm.foundation.preview.clear",
        "crm.readiness.read"
    ];

    public IReadOnlyCollection<CrmPortalAuthorizationScenarioContract> GetScenarios() =>
        [
            new(
                "CrmAdminCanPreviewFoundationData",
                "Fictitious CRM admin can read readiness and write preview foundation data.",
                "foundation-admin",
                Permissions,
                [],
                "Allowed",
                "FoundationSimulation",
                CrmPortalAuthorizationSimulationService.WarningText),
            new(
                "CrmSalesCanPreviewAssignedLeads",
                "Fictitious CRM sales profile can read foundation data and write lead previews.",
                "foundation-sales",
                ["crm.foundation.read", "crm.foundation.preview.write", "crm.readiness.read"],
                ["crm.foundation.preview.clear"],
                "LimitedAllowed",
                "FoundationSimulation",
                CrmPortalAuthorizationSimulationService.WarningText),
            new(
                "CrmReadOnlyCanViewReadiness",
                "Fictitious read-only profile can only inspect readiness and foundation status.",
                "foundation-readonly",
                ["crm.foundation.read", "crm.readiness.read"],
                ["crm.foundation.preview.write", "crm.foundation.preview.clear"],
                "ReadOnly",
                "FoundationSimulation",
                CrmPortalAuthorizationSimulationService.WarningText),
            new(
                "CrmUnauthorizedCannotUseFoundationMutation",
                "Fictitious unauthorized profile cannot run foundation mutation endpoints.",
                "foundation-unauthorized",
                [],
                ["crm.foundation.read", "crm.foundation.preview.write", "crm.foundation.preview.clear", "crm.readiness.read"],
                "Denied",
                "FoundationSimulation",
                CrmPortalAuthorizationSimulationService.WarningText)
        ];

    public IReadOnlyCollection<string> GetPermissions() => Permissions;
}
