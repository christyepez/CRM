using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class PipelineCatalogFoundationApiTests
{
    private const string SeedId="88888888-8888-8888-8888-888888888888";
    [Fact]
    public async Task ListAndDetail_AreReadOnlyFoundationEndpoints()
    {
        using var f=new WebApplicationFactory<Program>(); using var c=f.CreateClient();
        var list=await c.GetAsync("/api/crm/foundation/pipelines");
        var detail=await c.GetAsync($"/api/crm/foundation/pipelines/{SeedId}");
        Assert.Equal(HttpStatusCode.OK,list.StatusCode); Assert.Equal(HttpStatusCode.OK,detail.StatusCode);
        using var body=JsonDocument.Parse(await detail.Content.ReadAsStringAsync());
        Assert.True(body.RootElement.GetProperty("foundationMode").GetBoolean());
        Assert.False(body.RootElement.GetProperty("mutable").GetBoolean());
        Assert.False(body.RootElement.GetProperty("portalCatalogRuntimeEnabled").GetBoolean());
    }
    [Fact]
    public async Task MissingAndMutationRoutes_AreUnavailable()
    {
        using var f=new WebApplicationFactory<Program>(); using var c=f.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync($"/api/crm/foundation/pipelines/{Guid.NewGuid():D}")).StatusCode);
        Assert.True((await c.PostAsync("/api/crm/foundation/pipelines",null)).StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
        Assert.True((await c.PutAsync($"/api/crm/foundation/pipelines/{SeedId}",null)).StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
        Assert.True((await c.DeleteAsync($"/api/crm/foundation/pipelines/{SeedId}")).StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
        Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync("/api/crm/pipelines")).StatusCode);
    }
}
