using CRM.Infrastructure.Portal.RuntimeProbe;
using Xunit;

namespace CRM.UnitTests;

public sealed class PortalAuthRealRuntimeProbeTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlySkippedProbe()
    {
        var status = new PortalAuthRealRuntimeProbe().GetStatus();

        Assert.True(status.Exists);
        Assert.False(status.ApprovalGranted);
        Assert.False(status.ProbeEnabled);
        Assert.False(status.ProbeAttempted);
        Assert.False(status.PortalAuthRuntimeConnected);
        Assert.False(status.PortalHttpClientCreated);
        Assert.False(status.PortalHttpCallAttempted);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.True(status.UsesSyntheticFallback);
        Assert.Equal(PortalAuthRealRuntimeProbe.SyntheticPortalAuthReference, status.SyntheticPortalAuthReference);
        Assert.Equal(PortalAuthRealRuntimeProbe.SyntheticUserReference, status.SyntheticUserReference);
        Assert.True(status.ProbeSkippedBecausePortalAuthApprovalNotGranted);
        Assert.Equal(PortalAuthRealRuntimeProbe.WarningText, status.Warning);
    }
}
