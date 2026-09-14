using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
namespace CRM.UnitTests;
public sealed class Customer360HardeningTests
{
 [Fact] public async Task Snapshot_ExposesOnlyNonNegativeFoundationAggregates(){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();var r=await c.GetAsync("/api/crm/foundation/customer360");Assert.Equal(HttpStatusCode.OK,r.StatusCode);using var d=JsonDocument.Parse(await r.Content.ReadAsStringAsync());var x=d.RootElement[0];foreach(var n in new[]{"contactCount","openOpportunityCount","openCaseCount","interactionCount","noteCount","documentCount","tagCount","assignmentCount"})Assert.True(x.GetProperty(n).GetInt32()>=0);Assert.Equal("DeterministicSyntheticFoundation",x.GetProperty("sourceMode").GetString());Assert.False(x.GetProperty("productiveRuntimeEnabled").GetBoolean());Assert.False(x.GetProperty("portalRuntimeEnabled").GetBoolean());Assert.False(x.GetProperty("commonDbRuntimeEnabled").GetBoolean());}
 [Theory][InlineData("POST")][InlineData("PUT")][InlineData("DELETE")][InlineData("PATCH")] public async Task MutationMethods_AreUnavailable(string method){using var f=new WebApplicationFactory<Program>();using var c=f.CreateClient();using var req=new HttpRequestMessage(new HttpMethod(method),"/api/crm/foundation/customer360/11111111-1111-1111-1111-111111111111");var r=await c.SendAsync(req);Assert.True(r.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed);}
}
