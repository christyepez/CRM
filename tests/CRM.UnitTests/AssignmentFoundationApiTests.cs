using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class AssignmentFoundationApiTests
{
    private const string SeedId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb";

    [Fact]
    public async Task ListAndDetail_AreAvailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/api/crm/foundation/assignments")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync($"/api/crm/foundation/assignments/{SeedId}")).StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsSafetyFlagsDisabled()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/assignments", new
        {
            relatedEntityType = "Lead",
            relatedEntityId = Guid.NewGuid().ToString("D"),
            assigneeReferenceId = "portal-user-api",
            assignmentLabel = "Owner"
        });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("portalIdentityRuntimeEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("crossEntityMutationEnabled").GetBoolean());
    }

    [Fact]
    public async Task InvalidMissingArchiveConflictAndProductiveRoutes_MapSafely()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var bad = await client.PostAsJsonAsync("/api/crm/foundation/assignments", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = "bad",
            assigneeReferenceId = "portal-user-x"
        });
        Assert.Equal(HttpStatusCode.BadRequest, bad.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound,
            (await client.GetAsync($"/api/crm/foundation/assignments/{Guid.NewGuid():D}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK,
            (await client.PostAsync($"/api/crm/foundation/assignments/{SeedId}/archive", null)).StatusCode);
        var conflict = await client.PutAsJsonAsync($"/api/crm/foundation/assignments/{SeedId}", new
        {
            relatedEntityType = "Contact",
            relatedEntityId = "11111111-1111-1111-1111-111111111111",
            assigneeReferenceId = "portal-user-new",
            assignmentLabel = "Changed"
        });
        Assert.Equal(HttpStatusCode.Conflict, conflict.StatusCode);
        var delete = await client.DeleteAsync($"/api/crm/foundation/assignments/{SeedId}");
        Assert.True(delete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/crm/assignments")).StatusCode);
    }
}
