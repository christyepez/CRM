using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class ControlledNonProductionPortalAuthRuntimeValidationProbeTests
{
    [Fact]
    public async Task ProbeAsync_BlocksNamesOutsideAllowList()
    {
        var probe = new ControlledNonProductionPortalAuthRuntimeValidationProbe(EnabledOptions());

        var result = await probe.ProbeAsync(new PortalAuthRuntimeValidationProbeRequest("crm-portal-url", "crm-client-id", "crm-client-secret"));

        Assert.False(result.ProbeAttempted);
        Assert.False(result.ApprovedSecretNames);
        Assert.Equal("Blocked", result.Status);
        Assert.False(result.PortalUrlReturned);
    }

    [Fact]
    public async Task ProbeAsync_RequiresNonProduction()
    {
        var probe = new ControlledNonProductionPortalAuthRuntimeValidationProbe(EnabledOptions(environment: "Production"));

        var result = await probe.ProbeAsync(ApprovedRequest());

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.False(result.TokenReturned);
    }

    [Fact]
    public async Task ProbeAsync_RequiresExplicitFlag()
    {
        var probe = new ControlledNonProductionPortalAuthRuntimeValidationProbe(EnabledOptions(enabled: false));

        var result = await probe.ProbeAsync(ApprovedRequest());

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.False(result.HeaderReadAttempted);
    }

    [Fact]
    public async Task ProbeAsync_SkipsWhenProviderIsNotConfigured()
    {
        var probe = new ControlledNonProductionPortalAuthRuntimeValidationProbe(EnabledOptions(providerConfigured: false));

        var result = await probe.ProbeAsync(ApprovedRequest());

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Skipped", result.Status);
        Assert.False(result.PortalUrlReturned);
    }

    [Fact]
    public async Task ProbeAsync_ReturnsOnlySanitizedMetadataWhenAvailable()
    {
        var probe = new ControlledNonProductionPortalAuthRuntimeValidationProbe(EnabledOptions(), (_, _) => Task.FromResult(true));

        var result = await probe.ProbeAsync(ApprovedRequest());

        Assert.True(result.ProbeAttempted);
        Assert.True(result.ProviderConfigured);
        Assert.True(result.PortalAuthValidationAttempted);
        Assert.True(result.PortalAuthReachable);
        Assert.Equal("None", result.ErrorCategory);
        Assert.False(result.PortalUrlReturned);
        Assert.False(result.PortalUrlLogged);
        Assert.False(result.SecretValueReturned);
        Assert.False(result.TokenReturned);
        Assert.False(result.TokenLogged);
        Assert.False(result.HeaderReadAttempted);
        Assert.False(result.AuthorizationHeaderReadAttempted);
    }

    private static PortalAuthRuntimeValidationProbeRequest ApprovedRequest() =>
        new(
            PortalAuthRuntimeValidationProbeOptions.BaseUrlSecretName,
            PortalAuthRuntimeValidationProbeOptions.ClientIdSecretName,
            PortalAuthRuntimeValidationProbeOptions.ClientSecretName);

    private static PortalAuthRuntimeValidationProbeOptions EnabledOptions(
        string environment = "NonProduction",
        bool enabled = true,
        bool providerConfigured = true) =>
        new(
            Enabled: enabled,
            RuntimeEnvironment: environment,
            SecretProviderControlledReadApproved: true,
            ProviderConfigured: providerConfigured,
            TimeoutSeconds: 3);
}
