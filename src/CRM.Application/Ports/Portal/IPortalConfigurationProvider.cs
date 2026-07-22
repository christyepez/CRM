using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for reading configuration owned by PortalCorporativo.
/// </summary>
public interface IPortalConfigurationProvider
{
    Task<PortalConfigurationContract> GetConfigurationStatusAsync(string key, CancellationToken cancellationToken = default);
}
