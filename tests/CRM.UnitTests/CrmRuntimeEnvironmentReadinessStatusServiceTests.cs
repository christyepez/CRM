using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmRuntimeEnvironmentReadinessStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsRuntimeReadinessWithoutRealActivation()
    {
        var status = new CrmRuntimeEnvironmentReadinessStatusService().GetStatus();

        Assert.Equal("RuntimeEnvironmentReadiness", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.DockerComposeExpected);
        Assert.Equal(8093, status.CrmApiPort);
        Assert.False(status.SqlServerOwnedByCrm);
        Assert.False(status.NodePathRequiredForFrontendVerifier);
        Assert.False(status.ProductiveRoutesActive);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.AuthRuntimeEnabled);
        Assert.False(status.PortalRuntimeConnected);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal(CrmRuntimeEnvironmentReadinessStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmRuntimeEnvironmentReadinessStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetStatus_ListsExpectedHealthAndBlockedItems()
    {
        var status = new CrmRuntimeEnvironmentReadinessStatusService().GetStatus();

        Assert.Contains(status.HealthChecks, check => check.Endpoint == "/api/crm/foundation/sprint-4/runtime-readiness");
        Assert.Contains(status.ToolingChecks, check => check.Tool == "Node PATH" && check.Status == "WarnOnly");
        Assert.Contains(status.BlockedItems, item => item.Item == "Real database" && item.Status == "Blocked");
        Assert.Contains(status.BlockedItems, item => item.Item == "Productive API routes" && item.Status == "Blocked");
    }
}
