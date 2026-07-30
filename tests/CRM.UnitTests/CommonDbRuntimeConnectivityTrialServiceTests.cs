using CRM.Infrastructure.Data.CommonDb;
using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class CommonDbRuntimeConnectivityTrialServiceTests
{
    [Fact]
    public async Task ProbeAsync_WhenFlagDisabled_ReturnsLockedWithoutConnection()
    {
        var service = new CommonDbRuntimeConnectivityTrialService(
            new CommonDbRuntimeConnectivityTrialOptions(false, "Development", "crm-common-db-connection"),
            new RecordingCommonDbConnectivityProbe());

        var result = await service.ProbeAsync("crm-common-db-connection");

        Assert.False(result.CommonDbConnectionAttempted);
        Assert.False(result.CommonDbConnected);
        Assert.False(result.CommonDbConnectionStringResolved);
        Assert.False(result.CommonDbConnectionStringReturnedToApi);
        Assert.False(result.CommonDbConnectionStringLogged);
        Assert.False(result.CommonDbConnectionStringPersisted);
        Assert.False(result.CommonDbConnectionStringCached);
        Assert.False(result.SchemaCreated);
        Assert.False(result.MigrationExecuted);
        Assert.False(result.EfRuntimeEnabled);
        Assert.Equal("FlagDisabled", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenSecretNameIsNotAllowed_ReturnsLocked()
    {
        var service = new CommonDbRuntimeConnectivityTrialService(
            new CommonDbRuntimeConnectivityTrialOptions(true, "Development", "crm-common-db-connection"),
            new RecordingCommonDbConnectivityProbe());

        var result = await service.ProbeAsync("not-approved");

        Assert.False(result.CommonDbConnectionAttempted);
        Assert.False(result.AllowedSecretName);
        Assert.Equal("SecretNameNotAllowed", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenProduction_ReturnsLocked()
    {
        var service = new CommonDbRuntimeConnectivityTrialService(
            new CommonDbRuntimeConnectivityTrialOptions(true, "Production", "crm-common-db-connection"),
            new RecordingCommonDbConnectivityProbe());

        var result = await service.ProbeAsync("crm-common-db-connection");

        Assert.False(result.CommonDbConnectionAttempted);
        Assert.True(result.ProductionBlocked);
        Assert.Equal("ProductionBlocked", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenEnabled_ReturnsSanitizedMetadataOnly()
    {
        var service = new CommonDbRuntimeConnectivityTrialService(
            new CommonDbRuntimeConnectivityTrialOptions(true, "Development", "crm-common-db-connection"),
            new RecordingCommonDbConnectivityProbe());

        var result = await service.ProbeAsync("crm-common-db-connection");

        Assert.True(result.CommonDbConnectionAttempted);
        Assert.True(result.CommonDbConnected);
        Assert.False(result.CommonDbConnectionStringResolved);
        Assert.False(result.CommonDbConnectionStringReturnedToApi);
        Assert.False(result.CommonDbConnectionStringLogged);
        Assert.False(result.CommonDbConnectionStringPersisted);
        Assert.False(result.CommonDbConnectionStringCached);
        Assert.False(result.SchemaCreated);
        Assert.False(result.MigrationExecuted);
        Assert.False(result.EfRuntimeEnabled);
        Assert.True(result.SecretProviderMetadataDependencyValidated);
    }

    private sealed class RecordingCommonDbConnectivityProbe : ICommonDbConnectivityProbe
    {
        public Task<CommonDbConnectivityProbeResult> ProbeAsync(
            CommonDbConnectivityProbeRequest request,
            CancellationToken cancellationToken = default) =>
            Task.FromResult(new CommonDbConnectivityProbeResult(
                SecretName: request.SecretName,
                ProbeAttempted: true,
                ProviderConfigured: true,
                SecretProviderAvailabilityMetadataUsed: true,
                ConnectionAttempted: true,
                Connected: true,
                TimeoutApplied: true,
                ElapsedMs: 1,
                ErrorCategory: "None",
                ConnectionStringReturned: false,
                ConnectionStringLogged: false,
                ConnectionStringPersisted: false,
                ConnectionStringCached: false,
                AllowedSecretName: true,
                Status: "Connected",
                Warning: "metadata-only"));
    }
}
