using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint1ClosureStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsFoundationClosedAndNotReady()
    {
        var response = new CrmSprint1ClosureStatusService().GetStatus();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("Sprint 1", response.Sprint);
        Assert.Equal("FoundationClosed", response.Status);
        Assert.Equal("NonProduction", response.RuntimeMode);
        Assert.Equal("None", response.Persistence);
        Assert.Equal("Planned", response.PortalIntegration);
        Assert.Equal("Planned", response.FinancialIntegration);
        Assert.Equal("Planned", response.ReportingIntegration);
        Assert.Equal("NotReady", response.ProductizationStatus);
        Assert.Equal("Sprint2Planning", response.NextGate);
        Assert.Equal("Foundation closure only; no productive activation", response.Warning);
    }
}
