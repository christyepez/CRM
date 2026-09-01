using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class LeadQualificationApiEndpointTests
{
    [Fact]
    public async Task FoundationQualificationEndpoint_ValidQualify_ReturnsOkAndFoundationOnlyContract()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/leads/lead-preview-001/qualification", new
        {
            decision = "Qualify",
            comment = "Synthetic foundation qualification."
        });

        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("lead-preview-001", body.RootElement.GetProperty("leadId").GetString());
        Assert.Equal("Qualified", body.RootElement.GetProperty("currentStatus").GetString());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.False(body.RootElement.GetProperty("productiveLeadQualificationRouteEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("portalRuntimeEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("commonDbRuntimeEnabled").GetBoolean());
    }

    [Fact]
    public async Task FoundationQualificationEndpoint_ValidDisqualify_ReturnsOk()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/leads/lead-preview-001/qualification", new
        {
            decision = "Disqualify",
            disqualificationReason = "NoInterest",
            comment = "Synthetic foundation disqualification."
        });

        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Disqualified", body.RootElement.GetProperty("currentStatus").GetString());
        Assert.Equal("NoInterest", body.RootElement.GetProperty("disqualificationReason").GetString());
    }

    [Fact]
    public async Task FoundationQualificationEndpoint_IdempotentSameState_ReturnsOkWithChangedFalse()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        await client.PostAsJsonAsync("/api/crm/foundation/leads/lead-preview-001/qualification", new { decision = "Qualify" });

        var response = await client.PostAsJsonAsync("/api/crm/foundation/leads/lead-preview-001/qualification", new { decision = "Qualify" });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task FoundationQualificationEndpoint_MissingLead_ReturnsNotFoundWithoutExceptionDetails()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/leads/missing-lead/qualification", new { decision = "Qualify" });
        var bodyText = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains("LeadNotFound", bodyText);
        Assert.DoesNotContain("System.", bodyText);
        Assert.DoesNotContain("Exception", bodyText);
        Assert.DoesNotContain("C:\\", bodyText);
    }

    [Fact]
    public async Task FoundationQualificationEndpoint_InvalidTransition_ReturnsConflictWithoutWriting()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        await client.PostAsJsonAsync("/api/crm/foundation/leads/lead-preview-001/qualification", new
        {
            decision = "Disqualify",
            disqualificationReason = "Duplicate"
        });

        var response = await client.PostAsJsonAsync("/api/crm/foundation/leads/lead-preview-001/qualification", new { decision = "Qualify" });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("InvalidTransition", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Theory]
    [InlineData("{")]
    [InlineData("{\"decision\":\"Disqualify\"}")]
    [InlineData("{\"decision\":\"Qualify\",\"disqualificationReason\":\"Duplicate\"}")]
    public async Task FoundationQualificationEndpoint_BadRequestCases_ReturnBadRequest(string payload)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync(
            "/api/crm/foundation/leads/lead-preview-001/qualification",
            new StringContent(payload, Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ProductiveQualificationRoute_RemainsUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/leads/lead-preview-001/qualification", new { decision = "Qualify" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("GET")]
    [InlineData("PUT")]
    [InlineData("PATCH")]
    [InlineData("DELETE")]
    public async Task FoundationQualificationEndpoint_UnsupportedMethods_DoNotExecuteQualification(string method)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        using var request = new HttpRequestMessage(new HttpMethod(method), "/api/crm/foundation/leads/lead-preview-001/qualification")
        {
            Content = method is "PUT" or "PATCH" ? JsonContent.Create(new { decision = "Qualify" }) : null
        };

        var response = await client.SendAsync(request);

        Assert.True(response.StatusCode is HttpStatusCode.MethodNotAllowed or HttpStatusCode.NotFound);
    }

    private static async Task<JsonDocument> ReadJsonAsync(HttpResponseMessage response)
    {
        var stream = await response.Content.ReadAsStreamAsync();
        return await JsonDocument.ParseAsync(stream);
    }
}
