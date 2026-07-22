using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for permission checks owned by PortalCorporativo.
/// </summary>
public interface IPortalPermissionProvider
{
    Task<PortalPermissionContract> GetPermissionStatusAsync(string permission, CancellationToken cancellationToken = default);
}
