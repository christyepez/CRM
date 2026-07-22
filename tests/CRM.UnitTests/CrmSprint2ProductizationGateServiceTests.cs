using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint2ProductizationGateServiceTests
{
    [Fact]
    public void GetStatus_ClosesSprint2WithoutProductiveActivation()
    {
        var status = new CrmSprint2ProductizationGateService().GetStatus();

        Assert.Equal("Sprint2Closed", status.Status);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("NoGoForProductiveActivation", status.OverallDecision);
        Assert.Equal("GoFoundationOnly", status.FoundationCrudDecision);
        Assert.Equal("NoGo", status.DurablePersistenceDecision);
        Assert.Equal("NoGo", status.RealDatabaseDecision);
        Assert.Equal("NoGo", status.PortalAuthRuntimeDecision);
        Assert.Equal("NoGo", status.ProductiveCrudApiDecision);
        Assert.Equal("Go", status.Sprint3PlanningDecision);
        Assert.Equal("Sprint3P1DurablePersistenceSetupDesign", status.NextGate);
        Assert.Equal(CrmSprint2ProductizationGateService.WarningText, status.Warning);
        Assert.True(status.FoundationMode);
        Assert.False(status.DurablePersistence);
        Assert.False(status.DatabaseConfigured);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.PortalRuntimeConnected);
        Assert.Contains(status.Capabilities, capability => capability.Capability == "Productive CRUD API" && capability.Decision == "NoGo");
        Assert.Contains(status.Sprint3Roadmap, item => item.Package == "Sprint3P1");
    }
}
