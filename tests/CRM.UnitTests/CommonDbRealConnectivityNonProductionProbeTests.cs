using CRM.Infrastructure.Persistence.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class CommonDbRealConnectivityNonProductionProbeTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlySkippedProbe()
    {
        var status = new CommonDbRealConnectivityNonProductionProbe().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.ApprovalGranted);
        Assert.False(status.ProbeEnabled);
        Assert.False(status.ProbeAttempted);
        Assert.False(status.CommonDbConnected);
        Assert.False(status.ConnectionStringResolved);
        Assert.False(status.SqlConnectionCreated);
        Assert.False(status.DbConnectionCreated);
        Assert.True(status.UsesSyntheticFallback);
        Assert.Equal(CommonDbRealConnectivityNonProductionProbe.SyntheticReference, status.SyntheticConnectionReference);
        Assert.True(status.ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted);
        Assert.Equal(CommonDbRealConnectivityNonProductionProbe.WarningText, status.Warning);
    }
}
