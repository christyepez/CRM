namespace CRM.Application.Ports.Portal;

/// FuturePortalAdapter port for the disabled controlled runtime pilot scaffold.
public interface IPortalRuntimeClient
{
    Task<PortalRuntimeClientResult> GetStatusAsync(CancellationToken cancellationToken = default);
}

public sealed record PortalRuntimeClientResult(
    bool Attempted,
    bool Enabled,
    bool ExternalCallAttempted,
    bool PortalCouplingEnabled,
    bool PortalRoutesEnabled,
    bool PortalNavigationEnabled,
    string Status,
    string Warning);
