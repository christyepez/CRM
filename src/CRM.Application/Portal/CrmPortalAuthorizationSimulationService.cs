using CRM.Application.Ports.Portal;

namespace CRM.Application.Portal;

public sealed class CrmPortalAuthorizationSimulationService(
    IPortalUserContextProvider userContextProvider,
    IPortalAuthorizationScenarioProvider scenarioProvider,
    CrmFoundationPermissionGuard permissionGuard)
{
    public const string WarningText = "Portal authorization simulation only; no real Portal runtime configured";

    public async Task<CrmPortalAuthorizationSimulationStatusResponse> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        var userContext = await GetSampleUserContextAsync(cancellationToken);

        return new CrmPortalAuthorizationSimulationStatusResponse(
            "CRM",
            "PortalAuthorizationSimulationActive",
            "FoundationSimulation",
            false,
            "PortalCorporativo",
            false,
            false,
            false,
            "NonProduction",
            "Sprint2P4ControlledCrudBehindFoundationFlag",
            WarningText,
            true,
            userContext,
            scenarioProvider.GetScenarios(),
            scenarioProvider.GetPermissions());
    }

    public IReadOnlyCollection<CrmPortalAuthorizationScenarioContract> GetScenarios() => scenarioProvider.GetScenarios();

    public IReadOnlyCollection<string> GetPermissions() => scenarioProvider.GetPermissions();

    public async Task<CrmPortalUserSimulationContract> GetSampleUserContextAsync(CancellationToken cancellationToken = default)
    {
        var context = await userContextProvider.GetCurrentUserContextAsync(cancellationToken);

        return new CrmPortalUserSimulationContract(
            "foundation-user-001",
            "CRM Foundation User",
            context.RuntimeMode,
            "FoundationSimulation",
            false,
            false,
            new CrmPortalTenantSimulationContract(
                "foundation-tenant",
                "Foundation Tenant",
                "FoundationSimulation",
                false,
                WarningText),
            scenarioProvider.GetPermissions(),
            WarningText);
    }

    public Task<CrmPortalPermissionCheckContract> CheckPermissionAsync(string requiredPermission, CancellationToken cancellationToken = default) =>
        permissionGuard.CheckAsync(requiredPermission, cancellationToken);
}
