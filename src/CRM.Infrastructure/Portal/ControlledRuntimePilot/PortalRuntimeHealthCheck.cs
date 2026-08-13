using CRM.Application.Ports.Portal;

namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed class PortalRuntimeHealthCheck
{
    private readonly IPortalRuntimeClient _client;

    public PortalRuntimeHealthCheck(IPortalRuntimeClient client)
    {
        _client = client;
    }

    public async Task<PortalRuntimeClientResult> CheckAsync(CancellationToken cancellationToken = default) =>
        await _client.GetStatusAsync(cancellationToken);
}
