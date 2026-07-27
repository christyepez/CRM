using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class CommonDbRuntimeProbePlaceholderTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledPlaceholderWithoutConnectionAttempt()
    {
        var status = new CommonDbRuntimeProbePlaceholder().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.Enabled);
        Assert.False(status.ConnectionAttempted);
        Assert.Equal("ContractOnlyRuntimeProbeDisabled", status.Mode);
        Assert.Equal(CommonDbRuntimeProbePlaceholder.WarningText, status.Warning);
    }
}
