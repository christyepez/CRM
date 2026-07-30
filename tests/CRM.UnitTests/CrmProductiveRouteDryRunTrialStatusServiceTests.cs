using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmProductiveRouteDryRunTrialStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledFailClosedMetadataOnlyContract()
    {
        var status = new CrmProductiveRouteDryRunTrialStatusService().GetStatus();

        Assert.Equal("ProductiveRouteDryRunTrial", status.Status);
        Assert.True(status.ProductiveRouteDryRunTrialExists);
        Assert.True(status.ProductiveRouteDryRunTrialApproved);
        Assert.False(status.ProductiveRouteDryRunTrialEnabled);
        Assert.False(status.ProductiveRoutesRegisteredByDefault);
        Assert.False(status.ProductiveRoutesDryRunRegistered);
        Assert.False(status.ProductiveRouteDryRunAttempted);
        Assert.Equal(423, status.ProductiveRouteDryRunStatusCode);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveDomainExecutionEnabled);
        Assert.False(status.ProductivePersistenceEnabled);
        Assert.False(status.DatabaseWriteAttempted);
        Assert.False(status.SideEffectsAllowed);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.False(status.DbRuntimeEnabled);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.MigrationsEnabled);
        Assert.False(status.SchemaChangeAllowed);
        Assert.True(status.PortalAuthMetadataDependencyValidated);
        Assert.True(status.CommonDbMetadataDependencyValidated);
        Assert.True(status.SecretProviderMetadataDependencyValidated);
        Assert.False(status.AuthHeaderRead);
        Assert.False(status.TokenRead);
        Assert.False(status.TokenStored);
        Assert.False(status.AuthAttributeEnabled);
        Assert.False(status.LoginEndpointCreated);
        Assert.False(status.LogoutEndpointCreated);
        Assert.False(status.IdentityRuntimeEnabled);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.ProductionBlocked);
        Assert.True(status.FailClosedByDefault);
        Assert.True(status.RollbackAvailable);
        Assert.True(status.ObservabilityMetadataOnly);
        Assert.Equal("Sprint9P6Sprint9GateDecision", status.NextGate);
    }
}
