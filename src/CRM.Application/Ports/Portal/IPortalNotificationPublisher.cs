using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for delegating notifications to PortalCorporativo.
/// </summary>
public interface IPortalNotificationPublisher
{
    Task<PortalNotificationContract> PublishPlannedNotificationAsync(PortalNotificationContract notification, CancellationToken cancellationToken = default);
}
