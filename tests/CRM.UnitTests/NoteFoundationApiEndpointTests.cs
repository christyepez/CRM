using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class NoteFoundationApiEndpointTests
{
    private const string SeedNoteId = "77777777-7777-7777-7777-777777777777";
    private const string SeedRelatedEntityId = "11111111-1111-1111-1111-111111111111";

    [Fact]
    public async Task ListAndDetail_Seed_AreAvailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/crm/foundation/notes")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync($"/api/crm/foundation/notes/{SeedNoteId}")).StatusCode);
    }

    [Fact]
    public async Task Create_ValidRequest_ReturnsExplicitContract()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var relatedId = Guid.NewGuid().ToString("D");        var response = await client.PostAsJsonAsync("/api/crm/foundation/notes", new
        {
            relatedEntityType = "Lead",
            relatedEntityId = $"  {relatedId}  ",
            text = "  Synthetic API note  "
        });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("foundationMode").GetBoolean());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("activitySchedulingEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("crossEntityMutationEnabled").GetBoolean());
        Assert.Equal("Synthetic API note", body.RootElement.GetProperty("note").GetProperty("text").GetString());
        Assert.Equal("Lead", body.RootElement.GetProperty("note").GetProperty("relatedEntityType").GetString());
    }

    [Fact]
    public async Task InvalidAndMissing_MapToSafeStatusCodes()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var invalid = await client.PostAsJsonAsync("/api/crm/foundation/notes", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = "bad",
            text = "Invalid"
        });        var raw = await invalid.Content.ReadAsStringAsync();
        using var body = JsonDocument.Parse(raw);
        Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);
        Assert.Equal("InvalidRelatedEntityId", body.RootElement.GetProperty("errorCode").GetString());
        Assert.DoesNotContain("Exception", raw, StringComparison.OrdinalIgnoreCase);
        var missingId = Guid.NewGuid().ToString("D");
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/crm/foundation/notes/{missingId}")).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.PostAsync($"/api/crm/foundation/notes/{missingId}/archive", null)).StatusCode);
    }

    [Fact]
    public async Task Update_NoChange_RemainsSuccessful()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PutAsJsonAsync($"/api/crm/foundation/notes/{SeedNoteId}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = SeedRelatedEntityId,
            text = "Synthetic CRM note for foundation validation."
        });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Active", body.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task Archive_IsIdempotent_AndArchivedUpdateConflicts()
    {        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var first = await client.PostAsync($"/api/crm/foundation/notes/{SeedNoteId}/archive", null);
        var repeat = await client.PostAsync($"/api/crm/foundation/notes/{SeedNoteId}/archive", null);
        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        Assert.Equal(HttpStatusCode.OK, repeat.StatusCode);
        using var repeatBody = JsonDocument.Parse(await repeat.Content.ReadAsStringAsync());
        Assert.False(repeatBody.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Archived", repeatBody.RootElement.GetProperty("status").GetString());
        var update = await client.PutAsJsonAsync($"/api/crm/foundation/notes/{SeedNoteId}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = SeedRelatedEntityId,
            text = "Attempted change after archive"
        });
        using var updateBody = JsonDocument.Parse(await update.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Conflict, update.StatusCode);
        Assert.Equal("ArchivedNoteCannotBeModified", updateBody.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task ProductiveAndDeleteRoutes_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/crm/notes")).StatusCode);
        var foundationDelete = await client.DeleteAsync($"/api/crm/foundation/notes/{SeedNoteId}");
        var productiveDelete = await client.DeleteAsync($"/api/crm/notes/{SeedNoteId}");
        Assert.True(foundationDelete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);