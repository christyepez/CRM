using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthTokenPropagationDryRunTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledContractOnlyPlaceholder()
    {
        var dryRun = new PortalAuthTokenPropagationDryRun();

        var status = dryRun.GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.DryRunEnabled);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.False(status.PortalHttpAttempted);
        Assert.Equal("mock://crm/portal-auth-token", status.SyntheticTokenReference);
        Assert.Equal("mock://crm/portal-user", status.SyntheticUserReference);
        Assert.False(status.RuntimeUsable);
        Assert.Equal("Portal Auth token propagation dry-run contract only; no real tokens or headers are read", status.Warning);
    }
}
