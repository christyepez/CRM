using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class AccountFoundationApiHardeningTests
{
    [Fact]
    public async Task Update_NoChange_ReturnsChangedFalse()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        const string id = "aaaaaaaa-1111-1111-1111-111111111111";
        var response = await client.PutAsJsonAsync($"/api/crm/foundation/accounts/{id}", new { name = "Contoso Foundation", taxId = "EC-179001", industry = "Technology", segment = "Enterprise" });
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task Activate_Repeated_IsIdempotent()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var created = await client.PostAsJsonAsync("/api/crm/foundation/accounts", new { name = "Idempotent", taxId = "EC-20", industry = "Tech", segment = "SMB" });
        using var createdBody = JsonDocument.Parse(await created.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();
        Assert.Equal(HttpStatusCode.OK, (await client.PostAsync($"/api/crm/foundation/accounts/{id}/activate", null)).StatusCode);
        var repeat = await client.PostAsync($"/api/crm/foundation/accounts/{id}/activate", null);
        using var repeatBody = JsonDocument.Parse(await repeat.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.OK, repeat.StatusCode);
        Assert.False(repeatBody.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Active", repeatBody.RootElement.GetProperty("account").GetProperty("status").GetString());
    }

    [Fact]
    public async Task Deactivate_Draft_ReturnsConflict()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var created = await client.PostAsJsonAsync("/api/crm/foundation/accounts", new { name = "Draft conflict", taxId = "EC-21", industry = "Tech", segment = "SMB" });
        using var createdBody = JsonDocument.Parse(await created.Content.ReadAsStringAsync());
        var id = createdBody.RootElement.GetProperty("id").GetString();
        var response = await client.PostAsync($"/api/crm/foundation/accounts/{id}/deactivate", null);
        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("InvalidStatusTransition", body.RootElement.GetProperty("errorCode").GetString());
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
    }

    [Fact]
    public async Task Update_MissingAccount_ReturnsNotFound()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = Guid.NewGuid().ToString();
        var response = await client.PutAsJsonAsync($"/api/crm/foundation/accounts/{id}", new { name = "Missing", taxId = "EC-22", industry = "Tech", segment = "SMB" });
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
