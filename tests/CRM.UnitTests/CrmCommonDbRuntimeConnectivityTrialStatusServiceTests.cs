using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmCommonDbRuntimeConnectivityTrialStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDefaultDisabledTrialDecision()
    {
        var status = new CrmCommonDbRuntimeConnectivityTrialStatusService().GetStatus();

        Assert.Equal("CommonDbRuntimeConnectivityTrial", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CommonDbRuntimeConnectivityTrialExists);
        Assert.True(status.CommonDbRuntimeConnectivityTrialApproved);
        Assert.False(status.CommonDbRuntimeConnectivityTrialEnabled);
        Assert.False(status.CommonDbConnectionAttempted);
        Assert.False(status.CommonDbConnected);
        Assert.False(status.CommonDbConnectionStringResolved);
        Assert.False(status.CommonDbConnectionStringReturnedToApi);
        Assert.False(status.CommonDbConnectionStringLogged);
        Assert.False(status.CommonDbConnectionStringPersisted);
        Assert.False(status.CommonDbConnectionStringCached);
        Assert.True(status.SecretProviderMetadataDependencyValidated);
        Assert.False(status.SchemaCreated);
        Assert.False(status.MigrationExecuted);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.ProductivePersistenceEnabled);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.ProductionBlocked);
        Assert.True(status.FailClosedByDefault);
        Assert.True(status.ObservabilityMetadataOnly);
        Assert.Equal("Sprint9P4PortalAuthRuntimeValidationTrial", status.NextGate);
    }
}
