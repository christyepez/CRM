using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class ActivityFoundationApiEndpointTests
{
    [Fact]
    public async Task FoundationActivityCreate_ValidLeadTarget_ReturnsOkAndFoundationFlags()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        const string leadId = "22222222-2222-2222-2222-222222222222";

        var response = await client.PostAsJsonAsync("/api/crm/foundation/activities", new
        {
            type = "Call",
            subject = " Follow up ",
            scheduledAtUtc = DateTimeOffset.UtcNow.AddHours(2),
            leadId
        });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("allowed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Follow up", body.RootElement.GetProperty("activity").GetProperty("subject").GetString());
        Assert.Equal("Scheduled", body.RootElement.GetProperty("status").GetString());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("portalRuntimeEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("commonDbRuntimeEnabled").GetBoolean());
    }

    [Fact]
    public async Task FoundationActivityCreate_InvalidEnum_ReturnsBadRequestWithoutExceptionDetails()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync(
            "/api/crm/foundation/activities",
            new StringContent("{\"type\":\"Visit\",\"subject\":\"Follow up\",\"scheduledAtUtc\":\"2026-01-01T00:00:00Z\",\"leadId\":\"22222222-2222-2222-2222-222222222222\"}", Encoding.UTF8, "application/json"));
        var bodyText = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("InvalidRequest", bodyText);
        Assert.DoesNotContain("System.", bodyText);
        Assert.DoesNotContain("Exception", bodyText);
    }

    [Fact]
    public async Task FoundationActivityCreate_MissingTarget_ReturnsBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/activities", new
        {
            type = "Task",
            subject = "Follow up",
            scheduledAtUtc = DateTimeOffset.UtcNow.AddDays(1)
        });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("ActivityTargetRequired", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task FoundationActivityGetById_AfterCreate_ReturnsActivity()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var activityId = await CreateActivityForLeadAsync(client);

        var response = await client.GetAsync($"/api/crm/foundation/activities/{activityId}");
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(activityId, body.RootElement.GetProperty("id").GetString());
        Assert.Equal("NonProductionSeam", body.RootElement.GetProperty("persistenceMode").GetString());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
    }

    [Fact]
    public async Task FoundationActivityUpdate_UsesRouteIdAndReturnsChanged()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        const string leadId = "22222222-2222-2222-2222-222222222222";
        var activityId = await CreateActivityForLeadAsync(client);

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/activities/{activityId}", new
        {
            id = Guid.NewGuid().ToString("D"),
            type = "Meeting",
            subject = "Decision meeting",
            scheduledAtUtc = DateTimeOffset.UtcNow.AddDays(2),
            leadId
        });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal(activityId, body.RootElement.GetProperty("id").GetString());
        Assert.Equal("Meeting", body.RootElement.GetProperty("activity").GetProperty("type").GetString());
    }

    [Fact]
    public async Task FoundationActivityComplete_ValidActivity_ReturnsCompleted()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var activityId = await CreateActivityForLeadAsync(client);

        var response = await client.PostAsync($"/api/crm/foundation/activities/{activityId}/complete", null);
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Completed", body.RootElement.GetProperty("status").GetString());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task FoundationActivityCancel_ValidActivity_ReturnsCancelled()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var activityId = await CreateActivityForLeadAsync(client);

        var response = await client.PostAsync($"/api/crm/foundation/activities/{activityId}/cancel", null);
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Cancelled", body.RootElement.GetProperty("status").GetString());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Theory]
    [InlineData("GET", "/api/crm/activities")]
    [InlineData("GET", "/api/crm/activities/synthetic-activity")]
    [InlineData("POST", "/api/crm/activities")]
    [InlineData("PUT", "/api/crm/activities/synthetic-activity")]
    [InlineData("DELETE", "/api/crm/activities/synthetic-activity")]
    public async Task ProductiveActivityRoutes_RemainUnavailable(string method, string route)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        using var request = new HttpRequestMessage(new HttpMethod(method), route);
        if (method is "POST" or "PUT")
        {
            request.Content = JsonContent.Create(new { subject = "Follow up" });
        }

        var response = await client.SendAsync(request);

        Assert.True(response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.Locked);
    }

    [Fact]
    public async Task FoundationActivityDelete_IsNotAvailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.DeleteAsync($"/api/crm/foundation/activities/{Guid.NewGuid():D}");

        Assert.True(response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
    }

    private static async Task<string> CreateActivityForLeadAsync(HttpClient client)
    {
        const string leadId = "22222222-2222-2222-2222-222222222222";
        var create = await client.PostAsJsonAsync("/api/crm/foundation/activities", new
        {
            type = "Call",
            subject = "Follow up",
            scheduledAtUtc = DateTimeOffset.UtcNow.AddHours(2),
            leadId
        });
        var created = await ReadJsonAsync(create);
        return created.RootElement.TryGetProperty("id", out var id)
            ? id.GetString()!
            : created.RootElement.GetProperty("data").GetProperty("id").GetString()!;
    }

    private static async Task<JsonDocument> ReadJsonAsync(HttpResponseMessage response)
    {
        var stream = await response.Content.ReadAsStreamAsync();
        return await JsonDocument.ParseAsync(stream);
    }
}
