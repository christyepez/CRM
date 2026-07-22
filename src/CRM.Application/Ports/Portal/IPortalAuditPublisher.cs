using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for publishing audit events to PortalCorporativo.
/// </summary>
public interface IPortalAuditPublisher
{
    Task<PortalAuditEventContract> PublishPlannedAuditEventAsync(PortalAuditEventContract auditEvent, CancellationToken cancellationToken = default);
}
