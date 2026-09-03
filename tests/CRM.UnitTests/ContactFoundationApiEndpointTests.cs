using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class ContactFoundationApiEndpointTests
{
    [Fact]
    public async Task FoundationContactCreate_ValidRequest_ReturnsOkAndNormalizedContact()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/crm/foundation/contacts", new
        {
            firstName = " Ada ",
            lastName = " Lovelace ",
            email = " ADA@EXAMPLE.TEST ",
            phone = " 0999999999 ",
            title = " Buyer "
        });

        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(body.RootElement.GetProperty("allowed").GetBoolean());
        Assert.True(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Ada Lovelace", body.RootElement.GetProperty("name").GetString());
        Assert.Equal("ada@example.test", body.RootElement.GetProperty("email").GetString());
        Assert.Equal("Buyer", body.RootElement.GetProperty("title").GetString());
        Assert.False(body.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("portalRuntimeEnabled").GetBoolean());
        Assert.False(body.RootElement.GetProperty("commonDbRuntimeEnabled").GetBoolean());
    }

    [Theory]
    [InlineData("{\"firstName\":\"\",\"lastName\":\"\",\"email\":\"ada@example.test\"}", "NameRequired")]
    [InlineData("{\"firstName\":\"Ada\",\"lastName\":\"Lovelace\",\"email\":\"not-an-email\"}", "InvalidEmail")]
    public async Task FoundationContactCreate_InvalidRequest_ReturnsBadRequest(string payload, string expectedErrorCode)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PostAsync(
            "/api/crm/foundation/contacts",
            new StringContent(payload, Encoding.UTF8, "application/json"));
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(expectedErrorCode, body.RootElement.GetProperty("errorCode").GetString());
        Assert.DoesNotContain("System.", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task FoundationContactCreate_ReadAfterCreate_ReturnsCreatedContact()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var create = await client.PostAsJsonAsync("/api/crm/foundation/contacts", new
        {
            firstName = "Grace",
            lastName = "Hopper",
            email = "grace@example.test",
            phone = "0999999999",
            title = "Decision Maker"
        });
        var created = await ReadJsonAsync(create);
        var id = created.RootElement.GetProperty("id").GetString();

        var read = await client.GetAsync($"/api/crm/foundation/contacts/{id}");
        var body = await ReadJsonAsync(read);

        Assert.Equal(HttpStatusCode.OK, read.StatusCode);
        Assert.Equal(id, body.RootElement.GetProperty("data").GetProperty("id").GetString());
        Assert.Equal("grace@example.test", body.RootElement.GetProperty("data").GetProperty("email").GetString());
    }

    [Fact]
    public async Task FoundationContactUpdate_ValidRequest_ReturnsOkAndReadAfterUpdateReflectsChange()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = await CreateContactAsync(client);

        var update = await client.PutAsJsonAsync($"/api/crm/foundation/contacts/{id}", new
        {
            firstName = "Ada",
            lastName = "Byron",
            email = "ADA.BYRON@EXAMPLE.TEST",
            phone = "0999999999",
            title = "Sponsor"
        });
        var updated = await ReadJsonAsync(update);

        Assert.Equal(HttpStatusCode.OK, update.StatusCode);
        Assert.True(updated.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("Ada Byron", updated.RootElement.GetProperty("name").GetString());
        Assert.Equal("ada.byron@example.test", updated.RootElement.GetProperty("email").GetString());

        var read = await client.GetAsync($"/api/crm/foundation/contacts/{id}");
        var body = await ReadJsonAsync(read);

        Assert.Equal("Ada", body.RootElement.GetProperty("data").GetProperty("firstName").GetString());
        Assert.Equal("Byron", body.RootElement.GetProperty("data").GetProperty("lastName").GetString());
        Assert.Equal("ada.byron@example.test", body.RootElement.GetProperty("data").GetProperty("email").GetString());
    }

    [Fact]
    public async Task FoundationContactUpdate_NotFound_ReturnsNotFoundWithoutExceptionDetails()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/contacts/{Guid.NewGuid():D}", new
        {
            firstName = "Ada",
            lastName = "Lovelace",
            email = "ada@example.test"
        });
        var bodyText = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains("ContactNotFound", bodyText);
        Assert.DoesNotContain("Exception", bodyText);
        Assert.DoesNotContain("C:\\", bodyText);
    }

    [Fact]
    public async Task FoundationContactUpdate_InvalidRequest_ReturnsBadRequest()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = await CreateContactAsync(client);

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/contacts/{id}", new
        {
            firstName = "Ada",
            lastName = "Lovelace",
            email = "bad-email"
        });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("InvalidEmail", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Fact]
    public async Task FoundationContactUpdate_SameData_ReturnsOkWithChangedFalse()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();
        var id = await CreateContactAsync(client);

        var response = await client.PutAsJsonAsync($"/api/crm/foundation/contacts/{id}", new
        {
            firstName = " Ada ",
            lastName = " Lovelace ",
            email = "ADA@EXAMPLE.TEST",
            phone = "0999999999",
            title = "Buyer"
        });
        var body = await ReadJsonAsync(response);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(body.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal("None", body.RootElement.GetProperty("errorCode").GetString());
    }

    [Theory]
    [InlineData("/api/crm/contacts")]
    [InlineData("/api/crm/contacts/synthetic-contact")]
    public async Task ProductiveContactWriteRoutes_RemainUnavailable(string route)
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        var post = await client.PostAsJsonAsync(route, new { firstName = "Ada", lastName = "Lovelace" });
        var put = await client.PutAsJsonAsync(route, new { firstName = "Ada", lastName = "Lovelace" });

        Assert.True(post.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.Locked);
        Assert.True(put.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.Locked);
    }

    private static async Task<string> CreateContactAsync(HttpClient client)
    {
        var create = await client.PostAsJsonAsync("/api/crm/foundation/contacts", new
        {
            firstName = "Ada",
            lastName = "Lovelace",
            email = "ada@example.test",
            phone = "0999999999",
            title = "Buyer"
        });
        var created = await ReadJsonAsync(create);
        return created.RootElement.GetProperty("id").GetString()!;
    }

    private static async Task<JsonDocument> ReadJsonAsync(HttpResponseMessage response)
    {
        var stream = await response.Content.ReadAsStreamAsync();
        return await JsonDocument.ParseAsync(stream);
    }
}
