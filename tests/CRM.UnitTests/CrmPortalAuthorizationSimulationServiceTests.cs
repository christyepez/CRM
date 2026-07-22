using CRM.Application.Portal;
using CRM.Infrastructure.Portal.Simulation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthorizationSimulationServiceTests
{
    [Fact]
    public async Task GetStatusAsync_ReturnsFoundationSimulationWithoutPortalRuntime()
    {
        var service = CreateService();

        var response = await service.GetStatusAsync();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("PortalAuthorizationSimulationActive", response.Status);
        Assert.Equal("FoundationSimulation", response.SimulationMode);
        Assert.False(response.PortalRuntimeConnected);
        Assert.Equal("PortalCorporativo", response.AuthOwnedBy);
        Assert.False(response.CrmOwnsAuth);
        Assert.False(response.CredentialStorageEnabled);
        Assert.False(response.ProductiveAuthorizationEnabled);
        Assert.Equal("Sprint2P4ControlledCrudBehindFoundationFlag", response.NextGate);
        Assert.Contains("CrmAdminCanPreviewFoundationData", response.Scenarios.Select(scenario => scenario.Scenario));
        Assert.Contains("crm.foundation.preview.clear", response.Permissions);
    }

    [Fact]
    public async Task CheckPermissionAsync_ReturnsSimulatedPermissionResult()
    {
        var service = CreateService();

        var response = await service.CheckPermissionAsync("crm.foundation.read");

        Assert.True(response.Allowed);
        Assert.Equal("FoundationSimulation", response.SimulationMode);
        Assert.Equal("PortalCorporativo", response.AuthOwnedBy);
        Assert.False(response.CrmOwnsAuth);
        Assert.False(response.CredentialStorageEnabled);
    }

    private static CrmPortalAuthorizationSimulationService CreateService()
    {
        var permissionProvider = new SimulatedPortalPermissionProvider();
        return new CrmPortalAuthorizationSimulationService(
            new SimulatedPortalUserContextProvider(),
            new SimulatedPortalAuthorizationScenarioProvider(),
            new CrmFoundationPermissionGuard(permissionProvider));
    }
}
