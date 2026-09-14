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
    public async Task ListDetailCreate_AreAvailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/crm/foundation/notes")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync($"/api/crm/foundation/notes/{SeedNoteId}")).StatusCode);
        var relatedId = Guid.NewGuid().ToString("D");
        var response = await client.PostAsJsonAsync("/api/crm/foundation/notes", new { relatedEntityType = "Lead", relatedEntityId = relatedId, text = " Synthetic API note " });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("foundationMode").GetBoolean());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.Equal("Synthetic API note", body.RootElement.GetProperty("note").GetProperty("text").GetString());
    }

    [Fact]
    public async Task InvalidMissingArchiveAndConflict_MapCorrectly()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var invalid = await client.PostAsJsonAsync("/api/crm/foundation/notes", new { relatedEntityType = "Contact", relatedEntityId = "bad", text = "Invalid" });
        Assert.Equal(HttpStatusCode.BadRequest, invalid.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/crm/foundation/notes/{Guid.NewGuid():D}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.PostAsync($"/api/crm/foundation/notes/{SeedNoteId}/archive", null)).StatusCode);
        var repeat = await client.PostAsync($"/api/crm/foundation/notes/{SeedNoteId}/archive", null);
        using var repeated = JsonDocument.Parse(await repeat.Content.ReadAsStringAsync());
        Assert.False(repeated.RootElement.GetProperty("changed").GetBoolean());
        var update = await client.PutAsJsonAsync($"/api/crm/foundation/notes/{SeedNoteId}", new { relatedEntityType = "Contact", relatedEntityId = SeedRelatedEntityId, text = "Changed" });
        Assert.Equal(HttpStatusCode.Conflict, update.StatusCode);
    }

    [Fact]
    public async Task ProductiveAndDeleteRoutes_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/crm/notes")).StatusCode);
        var foundationDelete = await client.DeleteAsync($"/api/crm/foundation/notes/{SeedNoteId}");
        Assert.True(foundationDelete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
    }
    [Fact]
    public async Task Create_TextAboveMax_ReturnsSafeBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/notes", new { relatedEntityType = "Contact", relatedEntityId = SeedRelatedEntityId, text = new string('x', 4001) });
        var raw = await response.Content.ReadAsStringAsync();
        using var body = JsonDocument.Parse(raw);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("TextTooLong", body.RootElement.GetProperty("errorCode").GetString());
        Assert.DoesNotContain("Exception", raw, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("StackTrace", raw, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Update_NoChange_ReturnsChangedFalse()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PutAsJsonAsync($"/api/crm/foundation/notes/{SeedNoteId}", new { relatedEntityType = "Contact", relatedEntityId = SeedRelatedEntityId, text = "Synthetic CRM note for foundation validation." });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }
}