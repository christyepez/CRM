using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for CRM menu metadata registration owned by PortalCorporativo.
/// </summary>
public interface IPortalMenuRegistrationProvider
{
    Task<PortalMenuItemContract> GetMenuRegistrationStatusAsync(CancellationToken cancellationToken = default);
}
