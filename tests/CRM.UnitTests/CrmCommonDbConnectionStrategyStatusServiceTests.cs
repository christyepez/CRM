using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbConnectionStrategyStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsContractOnlyCommonDbStrategy()
    {
        var status = new CrmCommonDbConnectionStrategyStatusService().GetStatus();

        Assert.Equal("CommonDbConnectionStrategy", status.Status);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.ConnectionStringsConfigured);
        Assert.False(status.SecretProviderConfigured);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.False(status.SqlServerOwnedByCrm);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.DbContextConfigured);
        Assert.False(status.MigrationsCreated);
        Assert.Equal("CrmDb", status.LogicalDatabaseName);
        Assert.True(status.LogicalDatabaseNameIsPlaceholder);
        Assert.Equal("ContractOnly", status.SecretStrategy);
        Assert.Equal("NoRealValuesInRepository", status.ConnectionStringPolicy);
        Assert.Equal("Sprint3P3EfDbContextPrototypeBehindDisabledFlag", status.NextGate);
        Assert.Equal(CrmCommonDbConnectionStrategyStatusService.WarningText, status.Warning);
        Assert.True(status.FoundationMode);
    }
}
