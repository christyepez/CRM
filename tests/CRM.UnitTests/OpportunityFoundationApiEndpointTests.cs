using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class OpportunityFoundationApiEndpointTests
{
    private const string PipelineId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb";
    private const string Stage1 = "cccccccc-cccc-cccc-cccc-cccccccccccc";
    private const string Stage2 = "dddddddd-dddd-dddd-dddd-dddddddddddd";

    private static object[] Stages =>
    [
        new { stageId = Stage1, name = "Qualification", order = 1 },
        new { stageId = Stage2, name = "Proposal", order = 2 }
    ];

    [Fact]
    public async Task FoundationOpportunity_ListAndSeedDetail_ReturnOk()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var list = await client.GetAsync("/api/crm/foundation/opportunities");
        var detail = await client.GetAsync("/api/crm/foundation/opportunities/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

        Assert.Equal(HttpStatusCode.OK, list.StatusCode);
        Assert.Equal(HttpStatusCode.OK, detail.StatusCode);
    }

    [Fact]
    public async Task FoundationOpportunity_DetailMissing_ReturnsNotFound()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.GetAsync($"/api/crm/foundation/opportunities/{Guid.NewGuid():D}");
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("OpportunityNotFound", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task FoundationOpportunity_Create_NormalizesAndReadAfterCreateWorks()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/opportunities", CreatePayload("  Synthetic Deal  "));
        var body = await ReadJsonAsync(response);
        var id = body.RootElement.GetProperty("id").GetString();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("allowed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Synthetic Deal", body.RootElement.GetProperty("opportunity").GetProperty("accountName").GetString());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("portalRuntimeEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("commonDbRuntimeEnabled").GetBoolean());

        var read = await client.GetAsync($"/api/crm/foundation/opportunities/{id}");
        Assert.Equal(HttpStatusCode.OK, read.StatusCode);
    }

    [Fact]
    public async Task FoundationOpportunity_Create_InvalidAndNullStages_ReturnBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var invalid = await client.PostAsJsonAsync("/api/crm/foundation/opportunities", new
        {
            accountName = "Deal",
            expectedValue = -1m,
            currency = "USD",
            probability = 20,
            pipelineId = PipelineId,
            stageId = Stage1,
            stages = Stages
        });
        var nullStages = await client.PostAsJsonAsync("/api/crm/foundation/opportunities", new
        {
            accountName = "Deal",
            expectedValue = 10m,
            currency = "USD",
            probability = 20,
            pipelineId = PipelineId,
            stageId = Stage1,
            stages = (object?)null
        });

        Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, nullStages.StatusCode);
    }

    [Fact]
    public async Task FoundationOpportunity_UpdateAndNoChange_ReturnSuccess()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = await CreateAsync(client, "Update Deal");
        var payload = CreatePayload("Updated Deal", probability: 35);

        var changed = await client.PutAsJsonAsync($"/api/crm/foundation/opportunities/{id}", payload);
        var changedBody = await ReadJsonAsync(changed);
        var noChange = await client.PutAsJsonAsync($"/api/crm/foundation/opportunities/{id}", payload);
        var noChangeBody = await ReadJsonAsync(noChange);

        Assert.Equal(HttpStatusCode.OK, changed.StatusCode);
        Assert.True(changedBody.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal(HttpStatusCode.OK, noChange.StatusCode);
        Assert.False(noChangeBody.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task FoundationOpportunity_ProgressesOnlyThroughApplicationPolicy()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = await CreateAsync(client, "Progress Deal");

        var response = await client.PostAsJsonAsync($"/api/crm/foundation/opportunities/{id}/progress", new
        {
            stageId = Stage2,
            stages = Stages
        });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal(Stage2, body.RootElement.GetProperty("opportunity").GetProperty("stageId").GetString());
    }

    [Theory]
    [InlineData("win", "Won")]
    [InlineData("lose", "Lost")]
    [InlineData("cancel", "Cancelled")]
    public async Task FoundationOpportunity_TerminalActions_AreIdempotent(string action, string expectedStatus)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = await CreateAsync(client, $"{action} Deal");

        var first = await client.PostAsync($"/api/crm/foundation/opportunities/{id}/{action}", null);
        var firstBody = await ReadJsonAsync(first);
        var repeat = await client.PostAsync($"/api/crm/foundation/opportunities/{id}/{action}", null);
        var repeatBody = await ReadJsonAsync(repeat);

        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        Assert.Equal(expectedStatus, firstBody.RootElement.GetProperty("status").GetString());
        Assert.True(firstBody.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal(HttpStatusCode.OK, repeat.StatusCode);
        Assert.False(repeatBody.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task ProductiveOpportunityRoutes_AndDelete_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = Guid.NewGuid().ToString("D");

        foreach (var request in new[]
        {
            new HttpRequestMessage(HttpMethod.Get, "/api/crm/opportunities"),
            new HttpRequestMessage(HttpMethod.Post, "/api/crm/opportunities"),
            new HttpRequestMessage(HttpMethod.Put, $"/api/crm/opportunities/{id}"),
            new HttpRequestMessage(HttpMethod.Post, $"/api/crm/opportunities/{id}/win"),
            new HttpRequestMessage(HttpMethod.Delete, $"/api/crm/opportunities/{id}")
        })
        {
            using var response = await client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        using var foundationDelete = await client.DeleteAsync($"/api/crm/foundation/opportunities/{id}");
        Assert.Contains(foundationDelete.StatusCode, new[] { HttpStatusCode.NotFound, HttpStatusCode.MethodNotAllowed });
    }

    private static object CreatePayload(string accountName, int probability = 25) => new
    {
        accountName,
        expectedValue = 25000m,
        currency = "usd",
        probability,
        pipelineId = PipelineId,
        stageId = Stage1,
        stages = Stages,
        leadId = (string?)null,
        contactId = (string?)null,
        accountId = (string?)null,
        activityId = (string?)null
    };

    private static async Task<string> CreateAsync(HttpClient client, string accountName)
    {
        var response = await client.PostAsJsonAsync("/api/crm/foundation/opportunities", CreatePayload(accountName));
        var body = await ReadJsonAsync(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return body.RootElement.GetProperty("id").GetString()!;
    }

    private static async Task<JsonDocument> ReadJsonAsync(HttpResponseMessage response) =>
        JsonDocument.Parse(await response.Content.ReadAsStringAsync());
}
