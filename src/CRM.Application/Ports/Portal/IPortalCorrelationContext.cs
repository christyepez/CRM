using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter port for carrying PortalCorporativo correlation metadata through CRM requests.
/// </summary>
public interface IPortalCorrelationContext
{
    PortalCorrelationContract GetCorrelationStatus();
}
