using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmDurablePersistenceSetupStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDesignOnlyDurablePersistenceSetup()
    {
        var status = new CrmDurablePersistenceSetupStatusService().GetStatus();

        Assert.Equal("DurablePersistenceSetupDesign", status.Status);
        Assert.Equal("DesignOnly", status.DurablePersistenceMode);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.DbContextConfigured);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.ConnectionStringsConfigured);
        Assert.False(status.SqlServerOwnedByCrm);
        Assert.Equal("PlannedOnly", status.SecretStrategy);
        Assert.Equal("PlannedOnly", status.MigrationStrategy);
        Assert.Equal("NoGo", status.ProductiveActivation);
        Assert.Equal("Sprint3P2CommonDbConnectionContractAndSecretStrategy", status.NextGate);
        Assert.Equal(CrmDurablePersistenceSetupStatusService.WarningText, status.Warning);
        Assert.True(status.FoundationMode);
        Assert.Contains(status.Capabilities, capability => capability.Capability == "Durable Persistence" && capability.Status == "DesignOnly");
        Assert.Contains(status.RequiredGates, gate => gate.Contains("common DB", StringComparison.OrdinalIgnoreCase));
    }
}
