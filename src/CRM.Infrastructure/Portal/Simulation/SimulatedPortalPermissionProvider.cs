using CRM.Application.Portal;
using CRM.Application.Ports.Portal;

namespace CRM.Infrastructure.Portal.Simulation;

public sealed class SimulatedPortalPermissionProvider : IPortalPermissionProvider
{
    private static readonly HashSet<string> AllowedPermissions = new(StringComparer.OrdinalIgnoreCase)
    {
        "crm.foundation.read",
        "crm.foundation.preview.write",
        "crm.foundation.preview.clear",
        "crm.readiness.read"
    };

    public Task<PortalPermissionContract> GetPermissionStatusAsync(string permission, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var allowed = AllowedPermissions.Contains(permission);

        return Task.FromResult(new PortalPermissionContract(
            "CRM",
            allowed ? "Allowed" : "Denied",
            "FoundationSimulation",
            "NonProduction",
            "PortalCorporativo",
            false,
            CrmPortalAuthorizationSimulationService.WarningText,
            AllowedPermissions.ToArray()));
    }
}
