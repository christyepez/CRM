using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthRealRuntimeProbeStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSkippedPortalAuthProbeWithoutRuntimeAccess()
    {
        var status = new CrmPortalAuthRealRuntimeProbeStatusService().GetStatus();

        Assert.Equal("PortalAuthRealRuntimeProbe", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.PortalAuthRealRuntimeProbeExists);
        Assert.False(status.PortalAuthRealRuntimeApprovalGranted);
        Assert.False(status.SecretProviderRealNonProductionApprovalGranted);
        Assert.False(status.PortalAuthRealRuntimeProbeEnabled);
        Assert.False(status.PortalAuthRealRuntimeProbeAttempted);
        Assert.False(status.PortalAuthRuntimeConnected);
        Assert.False(status.PortalAuthBaseUrlResolved);
        Assert.False(status.PortalAuthBaseUrlMaterialized);
        Assert.False(status.PortalAuthBaseUrlLogged);
        Assert.False(status.PortalAuthBaseUrlReturnedToApi);
        Assert.False(status.PortalHttpClientCreated);
        Assert.False(status.PortalHttpCallAttempted);
        Assert.False(status.PortalAuthTokenValidationAttempted);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.False(status.AuthorizationHeaderReadAttempted);
        Assert.False(status.RealTokenMaterialized);
        Assert.False(status.RealTokenLogged);
        Assert.False(status.TokenReturnedToApi);
        Assert.False(status.LoginImplementedByCrm);
        Assert.False(status.LogoutImplementedByCrm);
        Assert.False(status.IdentityImplementedByCrm);
        Assert.False(status.RolesPersistedInCrm);
        Assert.False(status.PermissionsPersistedInCrm);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.False(status.ApiRequiresPortalAuth);
        Assert.True(status.UsesSyntheticFallback);
        Assert.Equal(CrmPortalAuthRealRuntimeProbeStatusService.SyntheticPortalAuthReference, status.SyntheticPortalAuthReference);
        Assert.Equal(CrmPortalAuthRealRuntimeProbeStatusService.SyntheticUserReference, status.SyntheticUserReference);
        Assert.True(status.ProbeSkippedBecausePortalAuthApprovalNotGranted);
        Assert.Equal(CrmPortalAuthRealRuntimeProbeStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmPortalAuthRealRuntimeProbeStatusService.WarningText, status.Warning);
    }
}
