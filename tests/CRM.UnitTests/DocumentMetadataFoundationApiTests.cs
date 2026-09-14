using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace CRM.UnitTests;
public sealed class DocumentMetadataFoundationApiTests
{
 private const string SeedId="99999999-9999-9999-9999-999999999999";
 [Fact] public async Task ListAndDetail_AreAvailable(){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();Assert.Equal(HttpStatusCode.OK,(await c.GetAsync("/api/crm/foundation/documents")).StatusCode);Assert.Equal(HttpStatusCode.OK,(await c.GetAsync($"/api/crm/foundation/documents/{SeedId}")).StatusCode);}
 [Fact] public async Task Create_ReturnsMetadataFlagsFalse(){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();var r=await c.PostAsJsonAsync("/api/crm/foundation/documents",new{relatedEntityType="Contact",relatedEntityId=Guid.NewGuid().ToString("D"),fileReferenceId="portal-ref-api",fileName="api.pdf",contentType="application/pdf",description="api"});using var b=JsonDocument.Parse(await r.Content.ReadAsStringAsync());Assert.Equal(HttpStatusCode.OK,r.StatusCode);Assert.False(b.RootElement.GetProperty("productiveCrudEnabled").GetBoolean());Assert.False(b.RootElement.GetProperty("binaryStorageEnabled").GetBoolean());Assert.False(b.RootElement.GetProperty("portalContentRuntimeEnabled").GetBoolean());}
 [Fact] public async Task MissingInvalidArchiveConflictAndDelete_MapSafely(){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync($"/api/crm/foundation/documents/{Guid.NewGuid():D}")).StatusCode);var bad=await c.PostAsJsonAsync("/api/crm/foundation/documents",new{relatedEntityType="Contact",relatedEntityId="bad",fileReferenceId="x",fileName="x.pdf"});Assert.Equal(HttpStatusCode.BadRequest,bad.StatusCode);Assert.Equal(HttpStatusCode.OK,(await c.PostAsync($"/api/crm/foundation/documents/{SeedId}/archive",null)).StatusCode);var conflict=await c.PutAsJsonAsync($"/api/crm/foundation/documents/{SeedId}",new{relatedEntityType="Contact",relatedEntityId="11111111-1111-1111-1111-111111111111",fileReferenceId="portal-ref",fileName="changed.pdf"});Assert.Equal(HttpStatusCode.Conflict,conflict.StatusCode);var del=await c.DeleteAsync($"/api/crm/foundation/documents/{SeedId}");Assert.True(del.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync("/api/crm/documents")).StatusCode);}
}
