using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class CommonDbConnectivityDryRunTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledContractOnlyPlaceholder()
    {
        var dryRun = new CommonDbConnectivityDryRun();

        var status = dryRun.GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.DryRunEnabled);
        Assert.False(status.ConnectionAttempted);
        Assert.Equal("mock://crm/common-db", status.SyntheticConnectionReference);
        Assert.False(status.RuntimeUsable);
        Assert.Equal("Common DB connectivity dry-run contract only; no database connection is attempted", status.Warning);
    }
}
