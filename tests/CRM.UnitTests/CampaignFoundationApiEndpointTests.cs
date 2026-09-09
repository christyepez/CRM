using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class CampaignFoundationApiEndpointTests
{
    [Fact]
    public async Task Create_Valid_ReturnsFoundationCampaign()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/campaigns", new { name = "  Campaign API  ", startDate = "2026-10-01", endDate = "2026-10-31" });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("allowed").GetBoolean());
        Assert.Equal("Campaign API", body.RootElement.GetProperty("campaign").GetProperty("name").GetString());
        Assert.Equal("Draft", body.RootElement.GetProperty("status").GetString());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
    }

    [Fact]
    public async Task Create_InvalidRange_ReturnsBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/campaigns", new { name = "Bad", startDate = "2026-10-31", endDate = "2026-10-01" });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("InvalidDateRange", body.RootElement.GetProperty("errorCode").GetString());
    }
    [Fact]
    public async Task Lifecycle_ActivateThenComplete_ReturnsExpectedStates()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var created = await client.PostAsJsonAsync("/api/crm/foundation/campaigns", new { name = "Lifecycle", startDate = "2026-11-01", endDate = "2026-11-30" });
        using var createdBody = JsonDocument.Parse(await created.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();
        var active = await client.PostAsync($"/api/crm/foundation/campaigns/{id}/activate", null);
        using var activeBody = JsonDocument.Parse(await active.Content.ReadAsStringAsync());
        var completed = await client.PostAsync($"/api/crm/foundation/campaigns/{id}/complete", null);
        using var completeBody = JsonDocument.Parse(await completed.Content.ReadAsStringAsync());
        Assert.Equal("Active", activeBody.RootElement.GetProperty("status").GetString());
        Assert.Equal("Completed", completeBody.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task ProductiveAndDeleteRoutes_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/crm/campaigns")).StatusCode);
        var delete = await client.DeleteAsync("/api/crm/foundation/campaigns/44444444-4444-4444-4444-444444444444");
        Assert.True(delete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
    }
}