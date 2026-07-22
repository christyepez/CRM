using CRM.Application.Portal;
using CRM.Application.Ports.Portal;

namespace CRM.Infrastructure.Portal.Simulation;

public sealed class SimulatedPortalUserContextProvider : IPortalUserContextProvider
{
    public Task<PortalUserContextContract> GetCurrentUserContextAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(new PortalUserContextContract(
            "CRM",
            "FoundationSimulation",
            "FoundationSimulation",
            "NonProduction",
            "PortalCorporativo",
            false,
            CrmPortalAuthorizationSimulationService.WarningText,
            [
                "Security/Auth",
                "Permissions",
                "TenantContext"
            ]));
    }
}
