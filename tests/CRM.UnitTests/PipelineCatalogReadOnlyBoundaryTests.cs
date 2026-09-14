using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class PipelineCatalogReadOnlyBoundaryTests
{
    private const string SeedId="88888888-8888-8888-8888-888888888888";
    [Fact]
    public async Task EveryMutationVerb_IsUnavailable()
    {
        using var f=new WebApplicationFactory<Program>(); using var c=f.CreateClient();
        foreach(var req in new[]{new HttpRequestMessage(HttpMethod.Post,"/api/crm/foundation/pipelines"),new HttpRequestMessage(HttpMethod.Put,$"/api/crm/foundation/pipelines/{SeedId}"),new HttpRequestMessage(HttpMethod.Patch,$"/api/crm/foundation/pipelines/{SeedId}"),new HttpRequestMessage(HttpMethod.Delete,$"/api/crm/foundation/pipelines/{SeedId}")})
        {
            using(req){var res=await c.SendAsync(req);Assert.True(res.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);}
        }
    }

    [Fact]
    public async Task ProductivePipelineRoutes_AreUnavailable()
    {
        using var f=new WebApplicationFactory<Program>(); using var c=f.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync("/api/crm/pipelines")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync($"/api/crm/pipelines/{SeedId}")).StatusCode);
    }
}
