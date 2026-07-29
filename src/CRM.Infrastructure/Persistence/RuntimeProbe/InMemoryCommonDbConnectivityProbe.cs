namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed class InMemoryCommonDbConnectivityProbe(bool connected)
{
    public Task<bool> CheckAsync(string secretName, CancellationToken cancellationToken = default) =>
        Task.FromResult(
            connected
            && secretName.Equals(CommonDbConnectivityProbeOptions.ApprovedSecretName, StringComparison.OrdinalIgnoreCase));
}
