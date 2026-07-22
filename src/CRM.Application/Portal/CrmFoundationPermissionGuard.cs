using CRM.Application.Ports.Portal;

namespace CRM.Application.Portal;

public sealed class CrmFoundationPermissionGuard(IPortalPermissionProvider permissionProvider)
{
    public async Task<CrmPortalPermissionCheckContract> CheckAsync(string requiredPermission, CancellationToken cancellationToken = default)
    {
        var permission = await permissionProvider.GetPermissionStatusAsync(requiredPermission, cancellationToken);
        var allowed = string.Equals(permission.Status, "Allowed", StringComparison.OrdinalIgnoreCase);

        return new CrmPortalPermissionCheckContract(
            requiredPermission,
            allowed,
            allowed ? "Permission allowed by FoundationSimulation provider." : "Permission denied by FoundationSimulation provider.",
            "FoundationSimulation",
            "PortalCorporativo",
            false,
            false,
            CrmPortalAuthorizationSimulationService.WarningText);
    }
}
