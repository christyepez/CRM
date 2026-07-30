using CRM.Infrastructure.Portal.Auth;
using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthRuntimeValidationTrialServiceTests
{
    [Fact]
    public async Task ProbeAsync_WhenFlagDisabled_ReturnsLockedWithoutPortalValidation()
    {
        var service = new PortalAuthRuntimeValidationTrialService(
            Options(enabled: false),
            new RecordingPortalAuthRuntimeValidationProbe());

        var result = await service.ProbeAsync("crm-portal-auth-base-url", "crm-portal-auth-client-id", "crm-portal-auth-client-secret");

        Assert.False(result.PortalAuthValidationAttempted);
        Assert.False(result.PortalHttpAttempted);
        Assert.False(result.AuthHeaderRead);
        Assert.False(result.TokenRead);
        Assert.False(result.TokenStored);
        Assert.False(result.PortalAuthUrlReturnedToApi);
        Assert.False(result.PortalClientSecretReturnedToApi);
        Assert.Equal("FlagDisabled", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenSecretNamesAreNotAllowed_ReturnsLocked()
    {
        var service = new PortalAuthRuntimeValidationTrialService(
            Options(enabled: true),
            new RecordingPortalAuthRuntimeValidationProbe());

        var result = await service.ProbeAsync("not-approved", "crm-portal-auth-client-id", "crm-portal-auth-client-secret");

        Assert.False(result.PortalAuthValidationAttempted);
        Assert.False(result.ApprovedSecretNames);
        Assert.Equal("SecretNameNotAllowed", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenProduction_ReturnsLocked()
    {
        var service = new PortalAuthRuntimeValidationTrialService(
            Options(enabled: true, environment: "Production"),
            new RecordingPortalAuthRuntimeValidationProbe());

        var result = await service.ProbeAsync("crm-portal-auth-base-url", "crm-portal-auth-client-id", "crm-portal-auth-client-secret");

        Assert.False(result.PortalAuthValidationAttempted);
        Assert.True(result.ProductionBlocked);
        Assert.Equal("ProductionBlocked", result.ErrorCategory);
    }

    [Fact]
    public async Task ProbeAsync_WhenEnabled_ReturnsSanitizedMetadataOnly()
    {
        var service = new PortalAuthRuntimeValidationTrialService(
            Options(enabled: true),
            new RecordingPortalAuthRuntimeValidationProbe());

        var result = await service.ProbeAsync("crm-portal-auth-base-url", "crm-portal-auth-client-id", "crm-portal-auth-client-secret");

        Assert.True(result.PortalAuthValidationAttempted);
        Assert.True(result.PortalAuthValidated);
        Assert.True(result.PortalHttpAttempted);
        Assert.True(result.PortalHttpConfigured);
        Assert.False(result.PortalAuthUrlResolved);
        Assert.False(result.PortalAuthUrlReturnedToApi);
        Assert.False(result.PortalClientSecretResolved);
        Assert.False(result.PortalClientSecretReturnedToApi);
        Assert.False(result.AuthHeaderRead);
        Assert.False(result.TokenRead);
        Assert.False(result.TokenStored);
        Assert.False(result.ClaimsMapped);
        Assert.False(result.ProductiveAuthEnabled);
        Assert.False(result.IdentityRuntimeEnabled);
        Assert.True(result.SecretProviderMetadataDependencyValidated);
        Assert.True(result.CommonDbMetadataDependencyValidated);
    }

    private static PortalAuthRuntimeValidationTrialOptions Options(bool enabled, string environment = "Development") =>
        new(
            Enabled: enabled,
            RuntimeEnvironment: environment,
            BaseUrlSecretName: "crm-portal-auth-base-url",
            ClientIdSecretName: "crm-portal-auth-client-id",
            ClientSecretName: "crm-portal-auth-client-secret");

    private sealed class RecordingPortalAuthRuntimeValidationProbe : IPortalAuthRuntimeValidationProbe
    {
        public Task<PortalAuthRuntimeValidationProbeResult> ProbeAsync(
            PortalAuthRuntimeValidationProbeRequest request,
            CancellationToken cancellationToken = default) =>
            Task.FromResult(new PortalAuthRuntimeValidationProbeResult(
                ProbeAttempted: true,
                ProviderConfigured: true,
                PortalAuthMetadataAvailable: true,
                PortalAuthValidationAttempted: true,
                PortalAuthReachable: true,
                TimeoutApplied: true,
                ElapsedMs: 1,
                ErrorCategory: "None",
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
                Status: "Validated",
                Warning: "metadata-only"));
    }
}
