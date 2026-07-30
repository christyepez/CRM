using System.Diagnostics;
using CRM.Infrastructure.Portal.RuntimeProbe;

namespace CRM.Infrastructure.Portal.Auth;

public sealed class PortalAuthRuntimeValidationTrialService(
    PortalAuthRuntimeValidationTrialOptions options,
    IPortalAuthRuntimeValidationProbe probe)
{
    public async Task<PortalAuthRuntimeValidationTrialResult> ProbeAsync(
        string baseUrlSecretName,
        string clientIdSecretName,
        string clientSecretName,
        CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        if (IsProduction(options.RuntimeEnvironment))
        {
            return Locked(stopwatch.ElapsedMilliseconds, false, "ProductionBlocked", "Portal Auth runtime validation trial is blocked in Production");
        }

        var allowed = IsApproved(baseUrlSecretName, clientIdSecretName, clientSecretName);
        if (!allowed)
        {
            return Locked(stopwatch.ElapsedMilliseconds, false, "SecretNameNotAllowed", "Portal Auth logical secret names are not approved for the CRM trial");
        }

        if (!options.Enabled)
        {
            return Locked(stopwatch.ElapsedMilliseconds, true, "FlagDisabled", "Portal Auth runtime validation trial is disabled by default");
        }

        var result = await probe.ProbeAsync(
            new PortalAuthRuntimeValidationProbeRequest(baseUrlSecretName, clientIdSecretName, clientSecretName),
            cancellationToken);
        stopwatch.Stop();

        return new PortalAuthRuntimeValidationTrialResult(
            PortalAuthValidationAttempted: result.PortalAuthValidationAttempted,
            PortalAuthValidated: result.PortalAuthReachable,
            PortalHttpAttempted: result.ProbeAttempted,
            PortalHttpConfigured: result.ProviderConfigured,
            PortalAuthUrlResolved: false,
            PortalAuthUrlReturnedToApi: false,
            PortalClientSecretResolved: false,
            PortalClientSecretReturnedToApi: false,
            AuthHeaderRead: false,
            TokenRead: false,
            TokenStored: false,
            ClaimsMapped: false,
            ProductiveAuthEnabled: false,
            LoginEndpointCreated: false,
            LogoutEndpointCreated: false,
            IdentityRuntimeEnabled: false,
            AuthAttributeEnabled: false,
            SecretProviderMetadataDependencyValidated: result.PortalAuthMetadataAvailable,
            CommonDbMetadataDependencyValidated: true,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            ObservabilityMetadataOnly: true,
            ApprovedSecretNames: result.ApprovedSecretNames,
            ElapsedMs: stopwatch.ElapsedMilliseconds,
            Status: result.Status,
            Warning: "Portal Auth runtime validation trial returned sanitized metadata only",
            ErrorCategory: result.PortalAuthReachable ? null : result.ErrorCategory);
    }

    private PortalAuthRuntimeValidationTrialResult Locked(
        long elapsedMs,
        bool approvedSecretNames,
        string category,
        string warning) =>
        new(
            PortalAuthValidationAttempted: false,
            PortalAuthValidated: false,
            PortalHttpAttempted: false,
            PortalHttpConfigured: false,
            PortalAuthUrlResolved: false,
            PortalAuthUrlReturnedToApi: false,
            PortalClientSecretResolved: false,
            PortalClientSecretReturnedToApi: false,
            AuthHeaderRead: false,
            TokenRead: false,
            TokenStored: false,
            ClaimsMapped: false,
            ProductiveAuthEnabled: false,
            LoginEndpointCreated: false,
            LogoutEndpointCreated: false,
            IdentityRuntimeEnabled: false,
            AuthAttributeEnabled: false,
            SecretProviderMetadataDependencyValidated: true,
            CommonDbMetadataDependencyValidated: true,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            ObservabilityMetadataOnly: true,
            ApprovedSecretNames: approvedSecretNames,
            ElapsedMs: elapsedMs,
            Status: "Locked",
            Warning: warning,
            ErrorCategory: category);

    private bool IsApproved(string baseUrlSecretName, string clientIdSecretName, string clientSecretName) =>
        string.Equals(baseUrlSecretName, options.BaseUrlSecretName, StringComparison.OrdinalIgnoreCase)
        && string.Equals(clientIdSecretName, options.ClientIdSecretName, StringComparison.OrdinalIgnoreCase)
        && string.Equals(clientSecretName, options.ClientSecretName, StringComparison.OrdinalIgnoreCase);

    private static bool IsProduction(string value) =>
        value.Equals("Production", StringComparison.OrdinalIgnoreCase);
}
