using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthRuntimeValidationProbeTests
{
    [Fact]
    public async Task DisabledProbe_ReturnsLockedWithoutExposure()
    {
        var probe = new DisabledPortalAuthRuntimeValidationProbe();

        var result = await probe.ProbeAsync(ApprovedRequest());

        Assert.False(result.ProbeAttempted);
        Assert.Equal("Locked", result.Status);
        Assert.True(result.ApprovedSecretNames);
        Assert.False(result.PortalUrlReturned);
        Assert.False(result.SecretValueReturned);
        Assert.False(result.TokenReturned);
        Assert.False(result.HeaderReadAttempted);
        Assert.False(result.AuthorizationHeaderReadAttempted);
    }

    private static PortalAuthRuntimeValidationProbeRequest ApprovedRequest() =>
        new(
            PortalAuthRuntimeValidationProbeOptions.BaseUrlSecretName,
            PortalAuthRuntimeValidationProbeOptions.ClientIdSecretName,
            PortalAuthRuntimeValidationProbeOptions.ClientSecretName);
}
