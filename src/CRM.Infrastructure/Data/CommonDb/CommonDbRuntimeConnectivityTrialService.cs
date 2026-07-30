using System.Diagnostics;
using CRM.Infrastructure.Persistence.RuntimeProbe;

namespace CRM.Infrastructure.Data.CommonDb;

public sealed class CommonDbRuntimeConnectivityTrialService(
    CommonDbRuntimeConnectivityTrialOptions options,
    ICommonDbConnectivityProbe probe)
{
    public async Task<CommonDbRuntimeConnectivityTrialResult> ProbeAsync(
        string secretName,
        CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        if (IsProduction(options.RuntimeEnvironment))
        {
            return Locked(secretName, stopwatch.ElapsedMilliseconds, false, "ProductionBlocked", "Common DB runtime connectivity trial is blocked in Production");
        }

        var allowed = secretName.Equals(options.SecretName, StringComparison.OrdinalIgnoreCase);
        if (!allowed)
        {
            return Locked(secretName, stopwatch.ElapsedMilliseconds, false, "SecretNameNotAllowed", "Secret name is not approved for the Common DB trial");
        }

        if (!options.Enabled)
        {
            return Locked(secretName, stopwatch.ElapsedMilliseconds, true, "FlagDisabled", "Common DB runtime connectivity trial is disabled by default");
        }

        var result = await probe.ProbeAsync(new CommonDbConnectivityProbeRequest(secretName), cancellationToken);
        stopwatch.Stop();

        return new CommonDbRuntimeConnectivityTrialResult(
            SecretName: result.SecretName,
            CommonDbConnectionAttempted: result.ConnectionAttempted,
            CommonDbConnected: result.Connected,
            CommonDbConnectionStringResolved: false,
            CommonDbConnectionStringReturnedToApi: false,
            CommonDbConnectionStringLogged: false,
            CommonDbConnectionStringPersisted: false,
            CommonDbConnectionStringCached: false,
            SecretProviderMetadataDependencyValidated: result.SecretProviderAvailabilityMetadataUsed,
            SchemaCreated: false,
            MigrationExecuted: false,
            EfRuntimeEnabled: false,
            ProductivePersistenceEnabled: false,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            ObservabilityMetadataOnly: true,
            AllowedSecretName: result.AllowedSecretName,
            ElapsedMs: stopwatch.ElapsedMilliseconds,
            Status: result.Status,
            Warning: "Common DB runtime connectivity trial returned sanitized metadata only",
            ErrorCategory: result.Connected ? null : result.ErrorCategory);
    }

    private static CommonDbRuntimeConnectivityTrialResult Locked(
        string secretName,
        long elapsedMs,
        bool allowed,
        string category,
        string warning) =>
        new(
            SecretName: secretName,
            CommonDbConnectionAttempted: false,
            CommonDbConnected: false,
            CommonDbConnectionStringResolved: false,
            CommonDbConnectionStringReturnedToApi: false,
            CommonDbConnectionStringLogged: false,
            CommonDbConnectionStringPersisted: false,
            CommonDbConnectionStringCached: false,
            SecretProviderMetadataDependencyValidated: true,
            SchemaCreated: false,
            MigrationExecuted: false,
            EfRuntimeEnabled: false,
            ProductivePersistenceEnabled: false,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            ObservabilityMetadataOnly: true,
            AllowedSecretName: allowed,
            ElapsedMs: elapsedMs,
            Status: "Locked",
            Warning: warning,
            ErrorCategory: category);

    private static bool IsProduction(string value) =>
        value.Equals("Production", StringComparison.OrdinalIgnoreCase);
}
