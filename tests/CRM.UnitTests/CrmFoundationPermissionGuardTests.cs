using CRM.Application.Portal;
using CRM.Infrastructure.Portal.Simulation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmFoundationPermissionGuardTests
{
    [Fact]
    public async Task CheckAsync_AllowsKnownFoundationPermission()
    {
        var guard = new CrmFoundationPermissionGuard(new SimulatedPortalPermissionProvider());

        var result = await guard.CheckAsync("crm.foundation.preview.clear");

        Assert.True(result.Allowed);
        Assert.Equal("crm.foundation.preview.clear", result.Permission);
        Assert.Equal("FoundationSimulation", result.SimulationMode);
        Assert.Equal(CrmPortalAuthorizationSimulationService.WarningText, result.Warning);
    }

    [Fact]
    public async Task CheckAsync_DeniesUnknownPermissionWithoutThrowingProductiveAuthError()
    {
        var guard = new CrmFoundationPermissionGuard(new SimulatedPortalPermissionProvider());

        var result = await guard.CheckAsync("crm.productive.write");

        Assert.False(result.Allowed);
        Assert.Contains("denied", result.Reason, StringComparison.OrdinalIgnoreCase);
        Assert.False(result.CrmOwnsAuth);
    }
}
