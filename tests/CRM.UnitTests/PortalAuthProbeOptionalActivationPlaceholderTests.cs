using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthProbeOptionalActivationPlaceholderTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyDisabledPlaceholder()
    {
        var status = new PortalAuthProbeOptionalActivationPlaceholder().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.Enabled);
        Assert.False(status.ActivationApproved);
        Assert.False(status.PortalHttpAttempted);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.Equal("ContractOnly", status.Strategy);
        Assert.Equal("Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted", status.Warning);
    }
}
