using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthControlledRealRuntimeValidationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsFailClosedSprint8P4Defaults()
    {
        var status = new CrmPortalAuthControlledRealRuntimeValidationStatusService().GetStatus();

        Assert.Equal("PortalAuthControlledRealRuntimeValidation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.PortalAuthControlledRealRuntimeValidationExists);
        Assert.True(status.PortalAuthControlledRealRuntimeValidationApproved);
        Assert.False(status.PortalAuthControlledRealRuntimeValidationEnabled);
        Assert.False(status.PortalAuthRuntimeValidationAttempted);
        Assert.False(status.PortalAuthRuntimeConnected);
        Assert.True(status.SecretProviderAvailabilityMetadataUsed);
        Assert.False(status.PortalAuthBaseUrlResolved);
        Assert.False(status.PortalAuthBaseUrlMaterializedInPublicContract);
        Assert.False(status.PortalAuthBaseUrlLogged);
        Assert.False(status.PortalAuthBaseUrlReturnedToApi);
        Assert.False(status.PortalHttpClientCreated);
        Assert.False(status.PortalHttpCallAttempted);
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
        Assert.True(status.NonProductionOnly);
        Assert.True(status.FailClosedByDefault);
        Assert.Equal(CrmPortalAuthControlledRealRuntimeValidationStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmPortalAuthControlledRealRuntimeValidationStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetProbe_ReturnsSanitizedDefaults()
    {
        var probe = new CrmPortalAuthControlledRealRuntimeValidationStatusService().GetProbe();

        Assert.False(probe.ProbeAttempted);
        Assert.False(probe.ProviderConfigured);
        Assert.False(probe.PortalAuthMetadataAvailable);
        Assert.False(probe.PortalAuthValidationAttempted);
        Assert.False(probe.PortalAuthReachable);
        Assert.True(probe.TimeoutApplied);
        Assert.False(probe.PortalUrlReturned);
        Assert.False(probe.SecretValueReturned);
        Assert.False(probe.TokenReturned);
        Assert.False(probe.HeaderReadAttempted);
    }
}
