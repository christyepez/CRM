using CRM.Application.Customer360;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
namespace CRM.UnitTests;
public sealed class Customer360ReadServiceTests
{
 [Fact] public async Task Provider_ReturnsDeterministicReadOnlySnapshot(){using var f=new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();using var s=f.Services.CreateScope();var svc=s.ServiceProvider.GetRequiredService<ICustomer360ReadService>();var all=await svc.GetAllAsync();var x=Assert.Single(all);Assert.Equal("DeterministicSyntheticFoundation",x.SourceMode);Assert.False(x.ProductiveRuntimeEnabled);Assert.False(x.PortalRuntimeEnabled);Assert.False(x.CommonDbRuntimeEnabled);}
 [Fact] public async Task InvalidId_ReturnsNull(){using var f=new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();using var s=f.Services.CreateScope();var svc=s.ServiceProvider.GetRequiredService<ICustomer360ReadService>();Assert.Null(await svc.GetByIdAsync("bad"));}
}
