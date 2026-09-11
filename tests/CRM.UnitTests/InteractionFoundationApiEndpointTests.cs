using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class InteractionFoundationApiEndpointTests
{
    private const string SeedInteractionId = "66666666-6666-6666-6666-666666666666";
    private const string SeedRelatedEntityId = "22222222-2222-2222-2222-222222222222";
    private static readonly DateTimeOffset SeedOccurredAtUtc = new(2026, 1, 15, 15, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task ListAndDetail_Seed_AreAvailableThroughFoundationApi()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var list = await client.GetAsync("/api/crm/foundation/interactions");
        var detail = await client.GetAsync($"/api/crm/foundation/interactions/{SeedInteractionId}");

        Assert.Equal(HttpStatusCode.OK, list.StatusCode);
        Assert.Equal(HttpStatusCode.OK, detail.StatusCode);
    }

    [Fact]
    public async Task Create_ValidRequest_ReturnsExplicitApiContract()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var relatedEntityId = Guid.NewGuid().ToString("D");

        var response = await client.PostAsJsonAsync("/api/crm/foundation/interactions", new
        {
            relatedEntityType = "Lead",
            relatedEntityId = $"  {relatedEntityId}  ",
            channel = "Email",
            direction = "Inbound",
            subject = "  Pricing question  ",
            summary = "  Synthetic lead asked about pricing.  ",
            occurredAtUtc = new DateTimeOffset(2026, 2, 1, 13, 30, 0, TimeSpan.Zero)
        });

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("foundationMode").GetBoolean());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("activitySchedulingEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("crossEntityMutationEnabled").GetBoolean());
        Assert.Equal("Pricing question", body.RootElement.GetProperty("interaction").GetProperty("subject").GetString());
        Assert.Equal("Lead", body.RootElement.GetProperty("interaction").GetProperty("relatedEntityType").GetString());
    }

    [Fact]
    public async Task Create_InvalidRequest_ReturnsBadRequestWithoutUnsafeDetails()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/interactions", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = "not-a-guid",
            channel = "Phone",
            direction = "Outbound",
            subject = "Bad interaction",
            summary = "Invalid related entity id.",
            occurredAtUtc = SeedOccurredAtUtc
        });

        var raw = await response.Content.ReadAsStringAsync();
        using var body = JsonDocument.Parse(raw);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("InvalidRelatedEntityId", body.RootElement.GetProperty("errorCode").GetString());
        Assert.DoesNotContain("Exception", raw, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("StackTrace", raw, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Update_NoChange_PreservesIdempotentSuccessAsOk()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/interactions/{SeedInteractionId}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = SeedRelatedEntityId,
            channel = "Phone",
            direction = "Outbound",
            subject = "Synthetic follow-up call",
            summary = "Synthetic contact confirmed a follow-up discussion for foundation validation.",
            occurredAtUtc = SeedOccurredAtUtc
        });

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Recorded", body.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task MissingInteractions_ReturnNotFound()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var missingId = Guid.NewGuid().ToString("D");

        var detail = await client.GetAsync($"/api/crm/foundation/interactions/{missingId}");
        var update = await client.PutAsJsonAsync($"/api/crm/foundation/interactions/{missingId}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = SeedRelatedEntityId,
            channel = "Phone",
            direction = "Outbound",
            subject = "Missing interaction",
            summary = "Missing interaction summary.",
            occurredAtUtc = SeedOccurredAtUtc
        });
        var voided = await client.PostAsync($"/api/crm/foundation/interactions/{missingId}/void", null);

        Assert.Equal(HttpStatusCode.NotFound, detail.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, update.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, voided.StatusCode);
    }

    [Fact]
    public async Task Void_MapsSuccessAndRepeatedVoidIdempotency()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var voided = await client.PostAsync($"/api/crm/foundation/interactions/{SeedInteractionId}/void", null);
        var repeated = await client.PostAsync($"/api/crm/foundation/interactions/{SeedInteractionId}/void", null);

        Assert.Equal(HttpStatusCode.OK, voided.StatusCode);
        await AssertChangedAsync(voided, true, "Voided");
        Assert.Equal(HttpStatusCode.OK, repeated.StatusCode);
        await AssertChangedAsync(repeated, false, "Voided");
    }

    [Fact]
    public async Task Update_VoidedInteraction_ReturnsConflict()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        await client.PostAsync($"/api/crm/foundation/interactions/{SeedInteractionId}/void", null);

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/interactions/{SeedInteractionId}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = SeedRelatedEntityId,
            channel = "Meeting",
            direction = "Outbound",
            subject = "Changed",
            summary = "Changed summary.",
            occurredAtUtc = SeedOccurredAtUtc
        });

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("VoidedInteractionCannotBeModified", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task Create_FutureOccurrence_ReturnsBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/interactions", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = SeedRelatedEntityId,
            channel = "Phone",
            direction = "Outbound",
            subject = "Future interaction",
            summary = "Must be rejected.",
            occurredAtUtc = DateTimeOffset.UtcNow.AddDays(1)
        });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("OccurredAtUtcInFuture", body.RootElement.GetProperty("errorCode").GetString());
    }
    [Fact]
    public async Task ProductiveAndDeleteRoutes_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var productive = await client.GetAsync("/api/crm/interactions");
        var foundationDelete = await client.DeleteAsync($"/api/crm/foundation/interactions/{SeedInteractionId}");
        var productiveDelete = await client.DeleteAsync($"/api/crm/interactions/{SeedInteractionId}");

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
