using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbProbeOptionalActivationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsOptionalActivationDisabledWithoutDatabaseAccess()
    {
        var status = new CrmCommonDbProbeOptionalActivationStatusService().GetStatus();

        Assert.Equal("CommonDbProbeOptionalActivation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CommonDbProbeOptionalActivationExists);
        Assert.False(status.CommonDbProbeActivationApproved);
        Assert.False(status.CommonDbProbeEnabled);
        Assert.False(status.CommonDbConnectionAttempted);
        Assert.True(status.SecretProviderRuntimeRequired);
        Assert.False(status.SecretProviderRuntimeConnected);
        Assert.True(status.SecretReadsRequiredBeforeActivation);
        Assert.False(status.SecretReadsEnabled);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.ConnectionStringsConfigured);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.MigrationsCreated);
        Assert.False(status.DurablePersistenceEnabled);
        Assert.False(status.ApiRequiresDatabase);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.SyntheticDataRequired);
        Assert.True(status.RollbackRequired);
        Assert.Equal(CrmCommonDbProbeOptionalActivationStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmCommonDbProbeOptionalActivationStatusService.WarningText, status.Warning);
        Assert.All(status.ActivationGates, gate =>
        {
            Assert.True(gate.Required);
            Assert.False(gate.Approved);
        });
        Assert.Contains(status.Dependencies, dependency => dependency.Dependency == "Secret Provider Runtime" && dependency.Required && !dependency.Available);
        Assert.Contains(status.BlockedItems, item => item.Item == "Database connection attempts");
    }
}
