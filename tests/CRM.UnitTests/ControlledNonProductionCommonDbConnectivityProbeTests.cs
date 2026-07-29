using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class ControlledNonProductionCommonDbConnectivityProbeTests
{
    [Fact]
    public async Task ProbeAsync_BlocksNamesOutsideAllowList()
    {
        var probe = new ControlledNonProductionCommonDbConnectivityProbe(EnabledOptions());

        var result = await probe.ProbeAsync(new CommonDbConnectivityProbeRequest("crm-other-db"));

        Assert.False(result.ProbeAttempted);
        Assert.False(result.AllowedSecretName);
        Assert.Equal("Blocked", result.Status);
        Assert.False(result.ConnectionStringReturned);
    }

    [Fact]
    public async Task ProbeAsync_RequiresNonProduction()
    {
        var probe = new ControlledNonProductionCommonDbConnectivityProbe(EnabledOptions(environment: "Production"));

        var result = await probe.ProbeAsync(new CommonDbConnectivityProbeRequest("crm-common-db-connection"));

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.False(result.ConnectionStringReturned);
    }

    [Fact]
    public async Task ProbeAsync_RequiresExplicitFlag()
    {
        var probe = new ControlledNonProductionCommonDbConnectivityProbe(EnabledOptions(enabled: false));

        var result = await probe.ProbeAsync(new CommonDbConnectivityProbeRequest("crm-common-db-connection"));

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.False(result.ConnectionStringReturned);
    }

    [Fact]
    public async Task ProbeAsync_SkipsWhenProviderIsNotConfigured()
    {
        var probe = new ControlledNonProductionCommonDbConnectivityProbe(EnabledOptions(providerConfigured: false));

        var result = await probe.ProbeAsync(new CommonDbConnectivityProbeRequest("crm-common-db-connection"));

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Skipped", result.Status);
        Assert.False(result.ConnectionStringReturned);
    }

    [Fact]
    public async Task ProbeAsync_ReturnsOnlySanitizedMetadataWhenConnectivitySucceeds()
    {
        var fake = new InMemoryCommonDbConnectivityProbe(connected: true);
        var probe = new ControlledNonProductionCommonDbConnectivityProbe(EnabledOptions(), fake.CheckAsync);

        var result = await probe.ProbeAsync(new CommonDbConnectivityProbeRequest("crm-common-db-connection"));

        Assert.True(result.ProbeAttempted);
        Assert.True(result.ProviderConfigured);
        Assert.True(result.ConnectionAttempted);
        Assert.True(result.Connected);
        Assert.True(result.TimeoutApplied);
        Assert.Equal("None", result.ErrorCategory);
        Assert.False(result.ConnectionStringReturned);
        Assert.False(result.ConnectionStringLogged);
        Assert.False(result.ConnectionStringPersisted);
        Assert.False(result.ConnectionStringCached);
    }

    private static CommonDbConnectivityProbeOptions EnabledOptions(
        string environment = "NonProduction",
        bool enabled = true,
        bool providerConfigured = true) =>
        new(
            Enabled: enabled,
            RuntimeEnvironment: environment,
            SecretProviderControlledReadApproved: true,
            ProviderConfigured: providerConfigured,
            SecretName: CommonDbConnectivityProbeOptions.ApprovedSecretName,
            TimeoutSeconds: 3);
}
