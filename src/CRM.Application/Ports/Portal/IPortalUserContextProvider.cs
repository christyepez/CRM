using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for reading the PortalCorporativo user context without CRM owning identity.
/// </summary>
public interface IPortalUserContextProvider
{
    Task<PortalUserContextContract> GetCurrentUserContextAsync(CancellationToken cancellationToken = default);
}
