using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthRuntimeProbePlaceholderTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledPlaceholderWithoutTokenOrPortalAttempt()
    {
        var status = new PortalAuthRuntimeProbePlaceholder().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.Enabled);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.PortalHttpAttempted);
        Assert.Equal("ContractOnlyRuntimeProbeDisabled", status.Mode);
        Assert.Equal(PortalAuthRuntimeProbePlaceholder.WarningText, status.Warning);
    }
}
