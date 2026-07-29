namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public interface ICommonDbConnectivityProbe
{
    Task<CommonDbConnectivityProbeResult> ProbeAsync(
        CommonDbConnectivityProbeRequest request,
        CancellationToken cancellationToken = default);
}
