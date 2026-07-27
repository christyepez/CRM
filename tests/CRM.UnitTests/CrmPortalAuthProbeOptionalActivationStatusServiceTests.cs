using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthProbeOptionalActivationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledOptionalActivationContract()
    {
        var status = new CrmPortalAuthProbeOptionalActivationStatusService().GetStatus();

        Assert.Equal("PortalAuthProbeOptionalActivation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.PortalAuthProbeOptionalActivationExists);
        Assert.False(status.PortalAuthProbeActivationApproved);
        Assert.False(status.PortalAuthProbeEnabled);
        Assert.False(status.PortalAuthRuntimeConnected);
        Assert.False(status.PortalHttpAttempted);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.True(status.SecretProviderRuntimeRequired);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.False(status.SecretReadsEnabled);
        Assert.False(status.LoginImplementedByCrm);
        Assert.False(status.IdentityImplementedByCrm);
        Assert.False(status.PermissionsPersistedInCrm);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.RollbackRequired);
        Assert.Equal("Sprint5P5LockedProductiveRouteStubTrialInNonProduction", status.NextGate);
        Assert.Equal("Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted", status.Warning);
        Assert.All(status.ActivationGates, gate =>
        {
            Assert.True(gate.Required);
            Assert.False(gate.Approved);
        });
        Assert.Contains(status.Dependencies, dependency => dependency.Dependency == "Token propagation strategy" && dependency.Required && !dependency.Available);
        Assert.Contains(status.BlockedItems, item => item.Item == "Token/header reads");
    }
}
