using System.Diagnostics;

namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed class ControlledNonProductionCommonDbConnectivityProbe(
    CommonDbConnectivityProbeOptions options,
    Func<string, CancellationToken, Task<bool>>? safeConnectivityCheck = null) : ICommonDbConnectivityProbe
{
    public async Task<CommonDbConnectivityProbeResult> ProbeAsync(
        CommonDbConnectivityProbeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!request.SecretName.Equals(CommonDbConnectivityProbeOptions.ApprovedSecretName, StringComparison.OrdinalIgnoreCase))
        {
            return Result(request.SecretName, false, false, false, 0, "Blocked", false, "Secret name is not approved");
        }

        if (!options.Enabled || !IsNonProduction(options.RuntimeEnvironment) || !options.SecretProviderControlledReadApproved)
        {
            return Result(request.SecretName, false, false, false, 0, "Locked", true, "Common DB controlled connectivity is fail-closed");
        }

        if (!options.ProviderConfigured || safeConnectivityCheck is null)
        {
            return Result(request.SecretName, false, false, false, 0, "Skipped", true, "External provider is not configured");
        }

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(TimeSpan.FromSeconds(Math.Max(1, options.TimeoutSeconds)));

        var stopwatch = Stopwatch.StartNew();
        var connected = false;
        var errorCategory = "None";
        try
        {
            connected = await safeConnectivityCheck(request.SecretName, timeout.Token);
        }
        catch (OperationCanceledException)
        {
            errorCategory = "Timeout";
        }
        catch
        {
            errorCategory = "ConnectivityFailure";
        }
        stopwatch.Stop();

        return Result(request.SecretName, true, true, connected, stopwatch.ElapsedMilliseconds, errorCategory, true, connected ? "Connectivity metadata verified without exposing connection string" : "Connectivity probe failed safely");
    }

    private static CommonDbConnectivityProbeResult Result(
        string secretName,
        bool attempted,
        bool providerConfigured,
        bool connected,
        long elapsedMs,
        string status,
        bool allowed,
        string warning) =>
        new(
            SecretName: secretName,
            ProbeAttempted: attempted,
            ProviderConfigured: providerConfigured,
            SecretProviderAvailabilityMetadataUsed: true,
            ConnectionAttempted: attempted,
            Connected: connected,
            TimeoutApplied: true,
            ElapsedMs: elapsedMs,
            ErrorCategory: status,
            ConnectionStringReturned: false,
            ConnectionStringLogged: false,
            ConnectionStringPersisted: false,
            ConnectionStringCached: false,
            AllowedSecretName: allowed,
            Status: status,
            Warning: warning);

    private static bool IsNonProduction(string value) =>
        value.Equals("NonProduction", StringComparison.OrdinalIgnoreCase)
        || value.Equals("Development", StringComparison.OrdinalIgnoreCase)
        || value.Equals("Staging", StringComparison.OrdinalIgnoreCase);
}
