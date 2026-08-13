using CRM.Application.Ports.Portal;

namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed class DisabledPortalRuntimeClient : IPortalRuntimeClient
{
    public Task<PortalRuntimeClientResult> GetStatusAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(new PortalRuntimeClientResult(
            Attempted: false,
            Enabled: false,
            ExternalCallAttempted: false,
            PortalCouplingEnabled: false,
            PortalRoutesEnabled: false,
            PortalNavigationEnabled: false,
            Status: "Locked",
            Warning: "Portal runtime client scaffold is disabled by default and performs no external calls"));
}
