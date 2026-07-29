using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbControlledRealConnectivityStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsFailClosedSprint8P3Defaults()
    {
        var status = new CrmCommonDbControlledRealConnectivityStatusService().GetStatus();

        Assert.Equal("CommonDbControlledRealConnectivity", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CommonDbControlledRealConnectivityExists);
        Assert.True(status.CommonDbControlledRealConnectivityApproved);
        Assert.False(status.CommonDbControlledRealConnectivityEnabled);
        Assert.False(status.CommonDbConnectivityAttempted);
        Assert.False(status.CommonDbConnected);
        Assert.True(status.SecretProviderAvailabilityMetadataUsed);
        Assert.False(status.SecretValueReturnedToApi);
        Assert.False(status.ConnectionStringResolved);
        Assert.False(status.ConnectionStringMaterializedInPublicContract);
        Assert.False(status.ConnectionStringLogged);
        Assert.False(status.ConnectionStringReturnedToApi);
        Assert.False(status.SqlConnectionCreated);
        Assert.False(status.DbConnectionCreated);
        Assert.False(status.DbConnectionOpened);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.AddDbContextRuntimeEnabled);
        Assert.False(status.UseSqlServerEnabled);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.DatabaseSchemaChanged);
        Assert.False(status.ProductivePersistenceEnabled);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ApiRequiresDatabase);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.FailClosedByDefault);
        Assert.Equal(CrmCommonDbControlledRealConnectivityStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmCommonDbControlledRealConnectivityStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetProbe_UsesOnlyApprovedLogicalSecretName()
    {
        var probe = new CrmCommonDbControlledRealConnectivityStatusService().GetProbe();

        Assert.Equal("crm-common-db-connection", probe.SecretName);
        Assert.False(probe.ProbeAttempted);
        Assert.False(probe.ProviderConfigured);
        Assert.False(probe.ConnectionAttempted);
        Assert.False(probe.Connected);
        Assert.True(probe.TimeoutApplied);
        Assert.False(probe.ConnectionStringReturned);
        Assert.False(probe.ConnectionStringLogged);
    }
}
