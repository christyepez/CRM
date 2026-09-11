using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class CaseFoundationApiEndpointTests
{
    private const string SeedCaseId = "55555555-5555-5555-5555-555555555555";
    private const string SeedCustomerId = "11111111-1111-1111-1111-111111111111";

    [Fact]
    public async Task ListAndDetail_Seed_AreAvailableThroughFoundationApi()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var list = await client.GetAsync("/api/crm/foundation/cases");
        var detail = await client.GetAsync($"/api/crm/foundation/cases/{SeedCaseId}");

        Assert.Equal(HttpStatusCode.OK, list.StatusCode);
        Assert.Equal(HttpStatusCode.OK, detail.StatusCode);
    }

    [Fact]
    public async Task Create_ValidRequest_ReturnsExplicitApiContract()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/cases", new
        {
            customerId = SeedCustomerId,
            title = "  API case  ",
            summary = "  Synthetic case created through the foundation API.  ",
            priority = "High"
        });

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("foundationMode").GetBoolean());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("customerMutationEnabled").GetBoolean());
        Assert.Equal("API case", body.RootElement.GetProperty("case").GetProperty("title").GetString());
        Assert.Equal("High", body.RootElement.GetProperty("case").GetProperty("priority").GetString());
    }

    [Fact]
    public async Task Create_InvalidRequest_ReturnsBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/cases", new
        {
            customerId = "not-a-guid",
            title = "Bad case",
            summary = "Invalid customer id.",
            priority = "Medium"
        });

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("InvalidCustomerId", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task Update_NoChange_PreservesIdempotentSuccessAsOk()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/cases/{SeedCaseId}", new
        {
            customerId = SeedCustomerId,
            title = "Synthetic billing case",
            summary = "Synthetic customer reported a billing mismatch for foundation validation.",
            priority = "Medium"
        });

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task MissingCases_ReturnNotFound()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var missingId = Guid.NewGuid().ToString("D");

        var detail = await client.GetAsync($"/api/crm/foundation/cases/{missingId}");
        var update = await client.PutAsJsonAsync($"/api/crm/foundation/cases/{missingId}", new
        {
            customerId = SeedCustomerId,
            title = "Missing case",
            summary = "Missing case summary.",
            priority = "Low"
        });
        var start = await client.PostAsync($"/api/crm/foundation/cases/{missingId}/start", null);

        Assert.Equal(HttpStatusCode.NotFound, detail.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, update.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, start.StatusCode);
    }

    [Fact]
    public async Task Lifecycle_StartResolveClose_MapsSuccessAndIdempotency()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var create = await client.PostAsJsonAsync("/api/crm/foundation/cases", new
        {
            customerId = SeedCustomerId,
            title = "Lifecycle case",
            summary = "Synthetic case for lifecycle API validation.",
            priority = "Critical"
        });
        using var createdBody = JsonDocument.Parse(await create.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();

        var started = await client.PostAsync($"/api/crm/foundation/cases/{id}/start", null);
        var repeatedStart = await client.PostAsync($"/api/crm/foundation/cases/{id}/start", null);
        var resolved = await client.PostAsync($"/api/crm/foundation/cases/{id}/resolve", null);
        var repeatedResolve = await client.PostAsync($"/api/crm/foundation/cases/{id}/resolve", null);
        var closed = await client.PostAsync($"/api/crm/foundation/cases/{id}/close", null);
        var repeatedClose = await client.PostAsync($"/api/crm/foundation/cases/{id}/close", null);

        Assert.Equal(HttpStatusCode.OK, started.StatusCode);
        await AssertChangedAsync(started, true, "InProgress");
        Assert.Equal(HttpStatusCode.OK, repeatedStart.StatusCode);
        await AssertChangedAsync(repeatedStart, false, "InProgress");
        Assert.Equal(HttpStatusCode.OK, resolved.StatusCode);
        await AssertChangedAsync(resolved, true, "Resolved");
        Assert.Equal(HttpStatusCode.OK, repeatedResolve.StatusCode);
        await AssertChangedAsync(repeatedResolve, false, "Resolved");
        Assert.Equal(HttpStatusCode.OK, closed.StatusCode);
        await AssertChangedAsync(closed, true, "Closed");
        Assert.Equal(HttpStatusCode.OK, repeatedClose.StatusCode);
        await AssertChangedAsync(repeatedClose, false, "Closed");
    }

    [Fact]
    public async Task InvalidLifecycleTransition_ReturnsConflict()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync($"/api/crm/foundation/cases/{SeedCaseId}/close", null);

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("InvalidStatusTransition", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task ProductiveAndDeleteRoutes_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var productive = await client.GetAsync("/api/crm/cases");
        var foundationDelete = await client.DeleteAsync($"/api/crm/foundation/cases/{SeedCaseId}");
        var productiveDelete = await client.DeleteAsync($"/api/crm/cases/{SeedCaseId}");

        Assert.Equal(HttpStatusCode.NotFound, productive.StatusCode);
        Assert.True(foundationDelete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
        Assert.True(productiveDelete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
    }

    private static async Task AssertChangedAsync(HttpResponseMessage response, bool changed, string status)
    {
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.Equal(changed, body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal(status, body.RootElement.GetProperty("status").GetString());
    }
}
