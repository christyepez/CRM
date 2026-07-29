namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed class DisabledPortalAuthRuntimeValidationProbe : IPortalAuthRuntimeValidationProbe
{
    public Task<PortalAuthRuntimeValidationProbeResult> ProbeAsync(
        PortalAuthRuntimeValidationProbeRequest request,
        CancellationToken cancellationToken = default) =>
        Task.FromResult(new PortalAuthRuntimeValidationProbeResult(
            ProbeAttempted: false,
            ProviderConfigured: false,
            PortalAuthMetadataAvailable: false,
            PortalAuthValidationAttempted: false,
            PortalAuthReachable: false,
            TimeoutApplied: true,
            ElapsedMs: 0,
            ErrorCategory: "Locked",
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
            ApprovedSecretNames: AreApproved(request),
            Status: "Locked",
            Warning: "Portal Auth controlled real runtime validation is disabled by default"));

    private static bool AreApproved(PortalAuthRuntimeValidationProbeRequest request) =>
        request.BaseUrlSecretName.Equals(PortalAuthRuntimeValidationProbeOptions.BaseUrlSecretName, StringComparison.OrdinalIgnoreCase)
        && request.ClientIdSecretName.Equals(PortalAuthRuntimeValidationProbeOptions.ClientIdSecretName, StringComparison.OrdinalIgnoreCase)
        && request.ClientSecretName.Equals(PortalAuthRuntimeValidationProbeOptions.ClientSecretName, StringComparison.OrdinalIgnoreCase);
}
