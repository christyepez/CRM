namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed class DisabledCommonDbConnectivityProbe : ICommonDbConnectivityProbe
{
    public Task<CommonDbConnectivityProbeResult> ProbeAsync(
        CommonDbConnectivityProbeRequest request,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(new CommonDbConnectivityProbeResult(
            SecretName: request.SecretName,
            ProbeAttempted: false,
            ProviderConfigured: false,
            SecretProviderAvailabilityMetadataUsed: true,
            ConnectionAttempted: false,
            Connected: false,
            TimeoutApplied: true,
            ElapsedMs: 0,
            ErrorCategory: "Locked",
            ConnectionStringReturned: false,
            ConnectionStringLogged: false,
            ConnectionStringPersisted: false,
            ConnectionStringCached: false,
            AllowedSecretName: false,
            Status: "Locked",
            Warning: "Common DB controlled real connectivity is disabled by default"));
}
