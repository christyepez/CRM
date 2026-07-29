namespace CRM.Infrastructure.Portal.RuntimeProbe;

public interface IPortalAuthRuntimeValidationProbe
{
    Task<PortalAuthRuntimeValidationProbeResult> ProbeAsync(PortalAuthRuntimeValidationProbeRequest request, CancellationToken cancellationToken = default);
}
