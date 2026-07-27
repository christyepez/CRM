using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class CommonDbProbeOptionalActivationPlaceholderTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyDisabledProbe()
    {
        var status = new CommonDbProbeOptionalActivationPlaceholder().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.Enabled);
        Assert.False(status.ActivationApproved);
        Assert.False(status.ConnectionAttempted);
        Assert.Equal("ContractOnly", status.Strategy);
        Assert.Equal(CommonDbProbeOptionalActivationPlaceholder.WarningText, status.Warning);
    }
}
