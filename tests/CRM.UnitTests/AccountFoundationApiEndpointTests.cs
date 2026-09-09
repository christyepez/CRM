using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class AccountFoundationApiEndpointTests
{
    [Fact]
    public async Task Create_Valid_ReturnsNormalizedDraftAccount()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/accounts", new { name = "  Acme API  ", taxId = " ec-9 ", industry = " Technology ", segment = " Enterprise " });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Acme API", body.RootElement.GetProperty("account").GetProperty("name").GetString());
        Assert.Equal("EC-9", body.RootElement.GetProperty("account").GetProperty("taxId").GetString());
        Assert.Equal("Draft", body.RootElement.GetProperty("account").GetProperty("status").GetString());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
    }

    [Fact]
    public async Task Create_Invalid_ReturnsBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/crm/foundation/accounts", new { name = "   ", taxId = "EC-9" });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("NameRequired", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task Lifecycle_ActivateThenDeactivate_ReturnsExpectedStates()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var created = await client.PostAsJsonAsync("/api/crm/foundation/accounts", new { name = "Lifecycle", taxId = "EC-10", industry = "Tech", segment = "SMB" });
        using var createdBody = JsonDocument.Parse(await created.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();
        var active = await client.PostAsync($"/api/crm/foundation/accounts/{id}/activate", null);
        using var activeBody = JsonDocument.Parse(await active.Content.ReadAsStringAsync());
        var inactive = await client.PostAsync($"/api/crm/foundation/accounts/{id}/deactivate", null);
        using var inactiveBody = JsonDocument.Parse(await inactive.Content.ReadAsStringAsync());
        Assert.Equal("Active", activeBody.RootElement.GetProperty("account").GetProperty("status").GetString());
        Assert.Equal("Inactive", inactiveBody.RootElement.GetProperty("account").GetProperty("status").GetString());
    }

    [Fact]
    public async Task ProductiveAndDeleteRoutes_RemainUnavailable()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync("/api/crm/accounts")).StatusCode);
        var delete = await client.DeleteAsync("/api/crm/foundation/accounts/aaaaaaaa-1111-1111-1111-111111111111");
        Assert.True(delete.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);
    }
}
