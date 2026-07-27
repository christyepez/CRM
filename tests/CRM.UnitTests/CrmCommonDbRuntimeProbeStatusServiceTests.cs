using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbRuntimeProbeStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledCommonDbProbe()
    {
        var status = new CrmCommonDbRuntimeProbeStatusService().GetStatus();

        Assert.Equal("CommonDbRuntimeProbe", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CommonDbRuntimeProbeExists);
        Assert.False(status.CommonDbRuntimeProbeEnabled);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.ConnectionStringsConfigured);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.False(status.DbConnectionAttemptedByRuntime);
        Assert.False(status.SqlServerOwnedByCrm);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.DbContextRuntimeActive);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.DurablePersistenceEnabled);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ApiRequiresDatabase);
        Assert.Equal(CrmCommonDbRuntimeProbeStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmCommonDbRuntimeProbeStatusService.WarningText, status.Warning);
    }
}
