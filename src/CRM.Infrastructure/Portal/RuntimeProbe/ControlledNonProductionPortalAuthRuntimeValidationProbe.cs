using System.Diagnostics;

namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed class ControlledNonProductionPortalAuthRuntimeValidationProbe(
    PortalAuthRuntimeValidationProbeOptions options,
    Func<PortalAuthRuntimeValidationProbeRequest, CancellationToken, Task<bool>>? safeAvailabilityCheck = null) : IPortalAuthRuntimeValidationProbe
{
    public async Task<PortalAuthRuntimeValidationProbeResult> ProbeAsync(
        PortalAuthRuntimeValidationProbeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!AreApproved(request))
        {
            return Result(false, false, false, 0, "Blocked", "Logical secret names are not approved", false);
        }

        if (!options.Enabled || !IsNonProduction(options.RuntimeEnvironment) || !options.SecretProviderControlledReadApproved)
        {
            return Result(false, false, false, 0, "Locked", "Portal Auth controlled runtime validation is fail-closed", true);
        }

        if (!options.ProviderConfigured || safeAvailabilityCheck is null)
        {
            return Result(false, false, false, 0, "Skipped", "External Portal Auth provider is not configured", true);
        }

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(Math.Max(1, options.TimeoutSeconds)));

        var stopwatch = Stopwatch.StartNew();
        var reachable = false;
        var errorCategory = "None";
        try
        {
            reachable = await safeAvailabilityCheck(request, timeout.Token);
        }
        catch (OperationCanceledException)
        {
            errorCategory = "Timeout";
        }
        catch
        {
            errorCategory = "PortalAuthValidationFailure";
        }
        stopwatch.Stop();

        return Result(true, true, reachable, stopwatch.ElapsedMilliseconds, errorCategory, reachable ? "Portal Auth metadata validated without exposing URL, secrets or tokens" : "Portal Auth validation failed safely", true);
    }

    private static PortalAuthRuntimeValidationProbeResult Result(
        bool attempted,
        bool providerConfigured,
        bool reachable,
        long elapsedMs,
        string status,
        string warning,
        bool approvedSecretNames) =>
        new(
            ProbeAttempted: attempted,
            ProviderConfigured: providerConfigured,
            PortalAuthMetadataAvailable: providerConfigured,
            PortalAuthValidationAttempted: attempted,
            PortalAuthReachable: reachable,
            TimeoutApplied: true,
            ElapsedMs: elapsedMs,
            ErrorCategory: status,
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
            ApprovedSecretNames: approvedSecretNames,
            Status: status,
            Warning: warning);

    private static bool AreApproved(PortalAuthRuntimeValidationProbeRequest request) =>
        request.BaseUrlSecretName.Equals(PortalAuthRuntimeValidationProbeOptions.BaseUrlSecretName, StringComparison.OrdinalIgnoreCase)
        && request.ClientIdSecretName.Equals(PortalAuthRuntimeValidationProbeOptions.ClientIdSecretName, StringComparison.OrdinalIgnoreCase)
        && request.ClientSecretName.Equals(PortalAuthRuntimeValidationProbeOptions.ClientSecretName, StringComparison.OrdinalIgnoreCase);

    private static bool IsNonProduction(string value) =>
        value.Equals("NonProduction", StringComparison.OrdinalIgnoreCase)
        || value.Equals("Development", StringComparison.OrdinalIgnoreCase)
        || value.Equals("Staging", StringComparison.OrdinalIgnoreCase);
}
