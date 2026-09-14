using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class AssignmentHardeningTests
{
    [Fact]
    public async Task Update_NoChange_ReturnsChangedFalse()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var create = await client.PostAsJsonAsync("/api/crm/foundation/assignments", new
        {
            relatedEntityType = "Lead",
            relatedEntityId = "11111111-1111-1111-1111-111111111111",
            assigneeReferenceId = "portal-user-hardening",
            assignmentLabel = "Owner"
        });
        using var created = JsonDocument.Parse(await create.Content.ReadAsStringAsync());
        var id = created.RootElement.GetProperty("id").GetString()!;
        var update = await client.PutAsJsonAsync($"/api/crm/foundation/assignments/{id}", new
        {
            relatedEntityType = "Lead",
            relatedEntityId = "11111111-1111-1111-1111-111111111111",
            assigneeReferenceId = "portal-user-hardening",
            assignmentLabel = "Owner"
        });
        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        using var body = JsonDocument.Parse(await update.Content.ReadAsStringAsync());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task Archive_IsIdempotent_AndArchivedUpdateConflicts()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var create = await client.PostAsJsonAsync("/api/crm/foundation/assignments", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = "11111111-1111-1111-1111-111111111111",
            assigneeReferenceId = "portal-user-archive",
            assignmentLabel = "Advisor"
        });
        using var created = JsonDocument.Parse(await create.Content.ReadAsStringAsync());
        var id = created.RootElement.GetProperty("id").GetString()!;
        Assert.Equal(HttpStatusCode.OK, (await client.PostAsync($"/api/crm/foundation/assignments/{id}/archive", null)).StatusCode);
        var repeat = await client.PostAsync($"/api/crm/foundation/assignments/{id}/archive", null);
        using var repeatBody = JsonDocument.Parse(await repeat.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, repeat.StatusCode);
        Assert.False(repeatBody.RootElement.GetProperty("changed").GetBoolean());
        var conflict = await client.PutAsJsonAsync($"/api/crm/foundation/assignments/{id}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = "11111111-1111-1111-1111-111111111111",
            assigneeReferenceId = "portal-user-changed",
            assignmentLabel = "Changed"
        });
        Assert.Equal(HttpStatusCode.Conflict, conflict.StatusCode);
    }

    [Theory]
    [InlineData(201, 5)]
    [InlineData(5, 121)]
    public async Task MetadataAboveMax_ReturnsSafeBadRequest(int assigneeLength, int labelLength)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/assignments", new
        {
            relatedEntityType = "Opportunity",
            relatedEntityId = Guid.NewGuid().ToString("D"),
            assigneeReferenceId = new string('a', assigneeLength),
            assignmentLabel = new string('b', labelLength)
        });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
