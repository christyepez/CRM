using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CRM.UnitTests;

public sealed class DocumentMetadataHardeningTests
{
    [Fact]
    public async Task CreateUpdateNoChangeArchiveRepeatAndConflict_AreDeterministic()
    {
        using var factory=new WebApplicationFactory<Program>(); using var client=factory.CreateClient();
        var relatedId=Guid.NewGuid().ToString("D");
        var payload=new{relatedEntityType="Lead",relatedEntityId=relatedId,fileReferenceId="portal-content-ref-hardening",fileName="hardening.pdf",contentType="application/pdf",description="Synthetic metadata hardening"};
        var create=await client.PostAsJsonAsync("/api/crm/foundation/documents",payload); Assert.Equal(HttpStatusCode.OK,create.StatusCode);
        using var created=JsonDocument.Parse(await create.Content.ReadAsStringAsync()); var id=created.RootElement.GetProperty("document").GetProperty("id").GetString()!;
        var noChange=await client.PutAsJsonAsync($"/api/crm/foundation/documents/{id}",payload); using var noChangeBody=JsonDocument.Parse(await noChange.Content.ReadAsStringAsync()); Assert.False(noChangeBody.RootElement.GetProperty("changed").GetBoolean());
        var archive=await client.PostAsync($"/api/crm/foundation/documents/{id}/archive",null); Assert.Equal(HttpStatusCode.OK,archive.StatusCode);
        var repeat=await client.PostAsync($"/api/crm/foundation/documents/{id}/archive",null); using var repeatBody=JsonDocument.Parse(await repeat.Content.ReadAsStringAsync()); Assert.False(repeatBody.RootElement.GetProperty("changed").GetBoolean());
        Assert.Equal(HttpStatusCode.Conflict,(await client.PutAsJsonAsync($"/api/crm/foundation/documents/{id}",payload)).StatusCode);
    }

    [Fact]
    public async Task OversizedMetadata_IsRejectedWith400()
    {
        using var factory=new WebApplicationFactory<Program>(); using var client=factory.CreateClient();
        var response=await client.PostAsJsonAsync("/api/crm/foundation/documents",new
        {
            relatedEntityType="Contact", relatedEntityId=Guid.NewGuid().ToString("D"),
            fileReferenceId=new string('r',513), fileName="boundary.pdf",
            contentType="application/pdf", description="Synthetic boundary test"
        });
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }
}
