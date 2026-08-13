using CRM.Infrastructure.Portal.ControlledRuntimePilot;
using Xunit;

namespace CRM.UnitTests;

public sealed class DisabledPortalRuntimeClientTests
{
    [Fact]
    public async Task GetStatusAsync_IsFailClosedAndDoesNotAttemptExternalCalls()
    {
        var result = await new DisabledPortalRuntimeClient().GetStatusAsync();

        Assert.False(result.Attempted);
        Assert.False(result.Enabled);
        Assert.False(result.ExternalCallAttempted);
        Assert.False(result.PortalCouplingEnabled);
        Assert.False(result.PortalRoutesEnabled);
        Assert.False(result.PortalNavigationEnabled);
        Assert.Equal("Locked", result.Status);
    }

    [Fact]
    public void Options_DefaultsAreSafePlaceholders()
    {
        var options = PortalRuntimeOptions.Disabled();
        var flags = PortalRuntimeFeatureFlags.Disabled();

        Assert.False(options.Enabled);
        Assert.False(options.PortalClientEnabled);
        Assert.DoesNotContain("http", options.PortalBaseLogicalName, StringComparison.OrdinalIgnoreCase);
        Assert.False(flags.FirstSliceEnabled);
        Assert.False(flags.PortalClientEnabled);
        Assert.False(flags.GatewayRoutesEnabled);
        Assert.False(flags.PortalNavigationEnabled);
    }
}
