using CRM.Application.Portal;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthRuntimeProbeStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledPortalAuthProbe()
    {
        var status = new CrmPortalAuthRuntimeProbeStatusService().GetStatus();

        Assert.Equal("PortalAuthRuntimeProbe", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.PortalAuthRuntimeProbeExists);
        Assert.False(status.PortalAuthRuntimeProbeEnabled);
        Assert.False(status.PortalRuntimeConnected);
        Assert.False(status.AuthRuntimeEnabled);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.False(status.CredentialReadAttemptedByRuntime);
        Assert.False(status.PortalHttpAttemptedByRuntime);
        Assert.False(status.LoginImplementedByCrm);
        Assert.False(status.IdentityImplementedByCrm);
        Assert.False(status.PermissionsPersistedInCrm);
        Assert.True(status.FoundationSimulationActive);
        Assert.Equal(CrmPortalAuthRuntimeProbeStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmPortalAuthRuntimeProbeStatusService.WarningText, status.Warning);
    }
}
