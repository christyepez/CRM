using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace CRM.UnitTests;
public sealed class Customer360FoundationApiTests
{
 [Fact] public async Task ListAndDetail_AreReadOnlyAndAvailable(){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();var l=await c.GetAsync("/api/crm/foundation/customer360");Assert.Equal(HttpStatusCode.OK,l.StatusCode);using var d=JsonDocument.Parse(await l.Content.ReadAsStringAsync());var id=d.RootElement[0].GetProperty("customerId").GetString();Assert.Equal(HttpStatusCode.OK,(await c.GetAsync($"/api/crm/foundation/customer360/{id}")).StatusCode);}
 [Fact] public async Task MissingInvalidAndMutationRoutes_AreUnavailable(){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync($"/api/crm/foundation/customer360/{Guid.NewGuid():D}")).StatusCode);Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync("/api/crm/foundation/customer360/bad")).StatusCode);Assert.Equal(HttpStatusCode.NotFound,(await c.GetAsync("/api/crm/customer360")).StatusCode);var post=await c.PostAsync("/api/crm/foundation/customer360",null);Assert.True(post.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);var del=await c.DeleteAsync("/api/crm/foundation/customer360/11111111-1111-1111-1111-111111111111");Assert.True(del.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);}
}
