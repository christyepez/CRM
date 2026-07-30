using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPortalAuthRuntimeValidationTrialStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDefaultDisabledTrialDecision()
    {
        var status = new CrmPortalAuthRuntimeValidationTrialStatusService().GetStatus();

        Assert.Equal("PortalAuthRuntimeValidationTrial", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.PortalAuthRuntimeValidationTrialExists);
        Assert.True(status.PortalAuthRuntimeValidationTrialApproved);
        Assert.False(status.PortalAuthRuntimeValidationTrialEnabled);
        Assert.False(status.PortalAuthValidationAttempted);
        Assert.False(status.PortalAuthValidated);
        Assert.False(status.PortalHttpAttempted);
        Assert.False(status.PortalHttpConfigured);
        Assert.False(status.PortalAuthUrlResolved);
        Assert.False(status.PortalAuthUrlReturnedToApi);
        Assert.False(status.PortalClientSecretResolved);
        Assert.False(status.PortalClientSecretReturnedToApi);
        Assert.False(status.AuthHeaderRead);
        Assert.False(status.TokenRead);
        Assert.False(status.TokenStored);
        Assert.False(status.ClaimsMapped);
        Assert.False(status.ProductiveAuthEnabled);
        Assert.False(status.LoginEndpointCreated);
        Assert.False(status.LogoutEndpointCreated);
        Assert.False(status.IdentityRuntimeEnabled);
        Assert.False(status.AuthAttributeEnabled);
        Assert.True(status.SecretProviderMetadataDependencyValidated);
        Assert.True(status.CommonDbMetadataDependencyValidated);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.ProductionBlocked);
        Assert.True(status.FailClosedByDefault);
        Assert.True(status.ObservabilityMetadataOnly);
        Assert.Equal("Sprint9P5ProductiveRouteDryRunTrial", status.NextGate);
    }
}
