namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed class InMemoryPortalAuthRuntimeValidationProbe(bool reachable = true) : IPortalAuthRuntimeValidationProbe
{
    public Task<PortalAuthRuntimeValidationProbeResult> ProbeAsync(
        PortalAuthRuntimeValidationProbeRequest request,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(new PortalAuthRuntimeValidationProbeResult(
            ProbeAttempted: true,
            ProviderConfigured: true,
            PortalAuthMetadataAvailable: true,
            PortalAuthValidationAttempted: true,
            PortalAuthReachable: reachable,
            TimeoutApplied: true,
            ElapsedMs: 0,
            ErrorCategory: reachable ? "None" : "PortalAuthValidationFailure",
            PortalUrlReturned: false,
            PortalUrlLogged: false,
            PortalUrlPersisted: false,
            PortalUrlCached: false,
            SecretValueReturned: false,
            SecretValueLogged: false,
            TokenReturned: false,
            TokenLogged: false,
            TokenPersisted: false,
            TokenCached: false,
            HeaderReadAttempted: false,
            AuthorizationHeaderReadAttempted: false,
            ApprovedSecretNames: true,
            Status: reachable ? "Reachable" : "Error",
            Warning: "In-memory test probe uses sanitized metadata only"));
}
