using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmProductiveApiRouteDraftStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDraftOnlyDisabledProductiveApi()
    {
        var status = new CrmProductiveApiRouteDraftStatusService().GetStatus();

        Assert.Equal("ProductiveApiRouteDraft", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.ProductiveApiRouteDraftExists);
        Assert.False(status.ProductiveRoutesRegistered);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.False(status.AuthRuntimeEnabled);
        Assert.False(status.PortalRuntimeConnected);
        Assert.False(status.DurablePersistenceEnabled);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.True(status.FoundationCrudStillSeparate);
        Assert.Equal("Sprint3P6Sprint3ProductizationReview", status.NextGate);
        Assert.Equal(CrmProductiveApiRouteDraftStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetStatus_ListsFutureRoutesAsUnregisteredAndDisabled()
    {
        var status = new CrmProductiveApiRouteDraftStatusService().GetStatus();

        Assert.Equal(12, status.Routes.Count);
        Assert.Contains(status.Routes, route => route.Method == "GET" && route.Route == "/api/crm/leads");
        Assert.Contains(status.Routes, route => route.Method == "POST" && route.Route == "/api/crm/accounts");
        Assert.Contains(status.Routes, route => route.Method == "PUT" && route.Route == "/api/crm/contacts/{id}");
        Assert.All(status.Routes, route =>
        {
            Assert.Equal("DraftOnly", route.Status);
            Assert.False(route.Registered);
            Assert.False(route.Enabled);
        });
    }
}
