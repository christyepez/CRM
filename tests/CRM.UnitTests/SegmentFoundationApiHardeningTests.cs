using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class SegmentFoundationApiHardeningTests
{
    [Fact]
    public async Task Update_NoChange_ReturnsChangedFalse()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        const string id = "bbbbbbbb-2222-2222-2222-222222222222";
        var response = await client.PutAsJsonAsync($"/api/crm/foundation/segments/{id}", new { name = "Enterprise Customers", criteriaSummary = "High-value enterprise audience." });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task Activate_Repeated_IsIdempotent()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var created = await client.PostAsJsonAsync("/api/crm/foundation/segments", new { name = "Idempotent Segment", criteriaSummary = "Synthetic segment for lifecycle validation." });
        using var createdBody = JsonDocument.Parse(await created.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();        Assert.Equal(HttpStatusCode.OK, (await client.PostAsync($"/api/crm/foundation/segments/{id}/activate", null)).StatusCode);
        var repeat = await client.PostAsync($"/api/crm/foundation/segments/{id}/activate", null);
        using var repeatBody = JsonDocument.Parse(await repeat.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, repeat.StatusCode);
        Assert.False(repeatBody.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Active", repeatBody.RootElement.GetProperty("segment").GetProperty("status").GetString());
    }

    [Fact]
    public async Task Deactivate_Draft_ReturnsConflict()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var created = await client.PostAsJsonAsync("/api/crm/foundation/segments", new { name = "Draft Segment", criteriaSummary = "Draft conflict validation." });
        using var createdBody = JsonDocument.Parse(await created.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();
        var response = await client.PostAsync($"/api/crm/foundation/segments/{id}/deactivate", null);
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("InvalidStatusTransition", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task Update_MissingSegment_ReturnsNotFound()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PutAsJsonAsync($"/api/crm/foundation/segments/{Guid.NewGuid()}", new { name = "Missing", criteriaSummary = "Missing segment." });
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}