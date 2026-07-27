using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmProductiveRoutesLockedStubStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDocumentOnlyPreferredStrategy()
    {
        var status = new CrmProductiveRoutesLockedStubStatusService().GetStatus();

        Assert.Equal("ProductiveRoutesLockedStubValidation", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("DocumentOnlyPreferred", status.LockedStubsStrategy);
        Assert.False(status.ProductiveRoutesRegistered);
        Assert.False(status.LockedStubsRegistered);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.False(status.DbRequired);
        Assert.False(status.AuthRuntimeRequired);
        Assert.True(status.FoundationCrudStillSeparate);
        Assert.Equal(CrmProductiveRoutesLockedStubStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmProductiveRoutesLockedStubStatusService.WarningText, status.Warning);
        Assert.All(status.FutureRoutes, route =>
        {
            Assert.False(route.Registered);
            Assert.False(route.ExecutesBusinessLogic);
        });
    }
}
