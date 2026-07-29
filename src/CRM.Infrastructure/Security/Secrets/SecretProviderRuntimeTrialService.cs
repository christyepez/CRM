using System.Diagnostics;

namespace CRM.Infrastructure.Security.Secrets;

public sealed class SecretProviderRuntimeTrialService(
    SecretProviderRuntimeTrialOptions options,
    ISecretProviderRuntime runtime)
{
    public async Task<SecretProviderRuntimeTrialResult> ProbeAsync(
        string secretName,
        CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        if (IsProduction(options.RuntimeEnvironment))
        {
            return Locked(secretName, stopwatch.ElapsedMilliseconds, false, "ProductionBlocked", "Secret Provider runtime trial is blocked in Production");
        }

        var allowed = options.AllowedLogicalSecretNames.Contains(secretName, StringComparer.OrdinalIgnoreCase);
        if (!allowed)
        {
            return Locked(secretName, stopwatch.ElapsedMilliseconds, false, "SecretNameNotAllowed", "Secret name is not approved for the CRM trial");
        }

        if (!options.Enabled)
        {
            return Locked(secretName, stopwatch.ElapsedMilliseconds, true, "FlagDisabled", "Secret Provider runtime trial is disabled by default");
        }

        var result = await runtime.ReadAsync(new SecretProviderRuntimeReadRequest(secretName), cancellationToken);
        stopwatch.Stop();

        return new SecretProviderRuntimeTrialResult(
            SecretName: result.SecretName,
            ReadAttempted: result.ReadAttempted,
            ReadSucceeded: result.ReadSucceeded,
            ProviderConfigured: result.ProviderConfigured,
            ValueReturned: false,
            ValueLogged: false,
            ValuePersisted: false,
            ValueCached: false,
            RedactionApplied: true,
            ProductionBlocked: true,
            AllowedLogicalSecretName: result.AllowedSecretName,
            ElapsedMs: stopwatch.ElapsedMilliseconds,
            Status: result.Status,
            Warning: "Secret Provider runtime trial returned sanitized metadata only",
            ErrorCategory: result.ReadSucceeded ? null : result.Status);
    }

    private static SecretProviderRuntimeTrialResult Locked(
        string secretName,
        long elapsedMs,
        bool allowed,
        string category,
        string warning) =>
        new(
            SecretName: secretName,
            ReadAttempted: false,
            ReadSucceeded: false,
            ProviderConfigured: false,
            ValueReturned: false,
            ValueLogged: false,
            ValuePersisted: false,
            ValueCached: false,
            RedactionApplied: true,
            ProductionBlocked: true,
            AllowedLogicalSecretName: allowed,
            ElapsedMs: elapsedMs,
            Status: "Locked",
            Warning: warning,
            ErrorCategory: category);

    private static bool IsProduction(string value) =>
        value.Equals("Production", StringComparison.OrdinalIgnoreCase);
}
