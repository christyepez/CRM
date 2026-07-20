using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmReadinessServiceTests
{
    [Fact]
    public void GetReadiness_ReturnsFoundationOnlyStatus()
    {
        var response = new CrmReadinessService().GetReadiness();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("ReadyForFoundationOnly", response.Status);
        Assert.Equal("Planned", response.PortalIntegration);
        Assert.Equal("Planned", response.FinancialIntegration);
        Assert.Equal("NonProduction", response.RuntimeMode);
    }
}
