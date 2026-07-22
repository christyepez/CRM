using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmEfPrototypeStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledEfPrototype()
    {
        var status = new CrmEfPrototypeStatusService().GetStatus();

        Assert.Equal("EfDbContextPrototypeDisabled", status.Status);
        Assert.True(status.EfPrototypeExists);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.DbContextRuntimeActive);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.ConnectionStringsConfigured);
        Assert.False(status.ProviderConfigured);
        Assert.False(status.UseSqlServerConfigured);
        Assert.True(status.FoundationStoresRemainActive);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.Equal("Sprint3P4PortalAuthRuntimeContractValidation", status.NextGate);
        Assert.Equal(CrmEfPrototypeStatusService.WarningText, status.Warning);
        Assert.True(status.FoundationMode);
    }
}
