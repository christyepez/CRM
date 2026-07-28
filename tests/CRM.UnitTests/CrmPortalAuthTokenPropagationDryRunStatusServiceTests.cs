using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthTokenPropagationDryRunStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyDryRunWithoutTokenOrHeaderRead()
    {
        var service = new CrmPortalAuthTokenPropagationDryRunStatusService();

        var status = service.GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal("PortalAuthTokenPropagationDryRunContract", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.PortalAuthTokenPropagationDryRunContractExists);
        Assert.False(status.PortalAuthDryRunApprovalGranted);
        Assert.False(status.PortalAuthDryRunEnabled);
        Assert.False(status.PortalAuthRuntimeConnected);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.False(status.PortalHttpAttempted);
        Assert.True(status.UsesSyntheticTokenMetadata);
        Assert.Equal("mock://crm/portal-auth-token", status.SyntheticTokenReference);
        Assert.Equal("mock://crm/portal-user", status.SyntheticUserReference);
        Assert.False(status.RealTokenUsed);
        Assert.False(status.RealHeadersRead);
        Assert.False(status.LoginImplementedByCrm);
        Assert.False(status.IdentityImplementedByCrm);
        Assert.False(status.PermissionsPersistedInCrm);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.RollbackRequired);
        Assert.True(status.ObservabilityRequired);
        Assert.Equal("Sprint6P5LockedStubRuntimeRegistrationTrial", status.NextGate);
        Assert.Equal("Portal Auth token propagation dry-run contract only; no real tokens or headers are read", status.Warning);
    }
}
