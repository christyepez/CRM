using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class SegmentFoundationApiTests
{
    [Fact]
    public async Task ListAndDetail_Seed_AreAvailable()
    {
        using var factory=new WebApplicationFactory<Program>();
        using var client=factory.CreateClient();
        var list=await client.GetAsync("/api/crm/foundation/segments");
        Assert.Equal(HttpStatusCode.OK,list.StatusCode);
        var detail=await client.GetAsync("/api/crm/foundation/segments/bbbbbbbb-2222-2222-2222-222222222222");
        Assert.Equal(HttpStatusCode.OK,detail.StatusCode);
    }

    [Fact]
    public async Task Create_Invalid_ReturnsBadRequest()
    {
        using var factory=new WebApplicationFactory<Program>();
        using var client=factory.CreateClient();
        var response=await client.PostAsJsonAsync("/api/crm/foundation/segments",new { name=" ",criteriaSummary="criteria" });
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }

    [Fact]
    public async Task CreateUpdateAndLifecycle_WorkThroughFoundationApi()
    {
        using var factory=new WebApplicationFactory<Program>();
        using var client=factory.CreateClient();
        var create=await client.PostAsJsonAsync("/api/crm/foundation/segments",new { name=" API Segment ",criteriaSummary=" API criteria " });
        Assert.Equal(HttpStatusCode.OK,create.StatusCode);
        using var created=JsonDocument.Parse(await create.Content.ReadAsStringAsync());
        var id=created.RootElement.GetProperty("id").GetString();
        Assert.Equal("API Segment",created.RootElement.GetProperty("segment").GetProperty("name").GetString());
        var update=await client.PutAsJsonAsync($"/api/crm/foundation/segments/{id}",new { name="API Segment 2",criteriaSummary="Updated criteria" });
        Assert.Equal(HttpStatusCode.OK,update.StatusCode);
        Assert.Equal(HttpStatusCode.OK,(await client.PostAsync($"/api/crm/foundation/segments/{id}/activate",null)).StatusCode);
        Assert.Equal(HttpStatusCode.OK,(await client.PostAsync($"/api/crm/foundation/segments/{id}/deactivate",null)).StatusCode);
    }

    [Fact]
    public async Task MissingAndProductiveRoutes_RemainLocked()
    {
        using var factory=new WebApplicationFactory<Program>();
        using var client=factory.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound,(await client.GetAsync($"/api/crm/foundation/segments/{Guid.NewGuid()}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound,(await client.GetAsync("/api/crm/segments")).StatusCode);
        var delete=await client.DeleteAsync("/api/crm/foundation/segments/bbbbbbbb-2222-2222-2222-222222222222");
        Assert.True(delete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
    }
}
