using Xunit;
namespace CRM.ArchitectureTests;
public sealed class Customer360ArchitectureTests
{
 static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2502_RemainsReadOnlyAndFoundationOnly(){var r=Root();var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));var s=File.ReadAllText(Path.Combine(r,"src","CRM.Application","Customer360","Customer360ReadService.cs"));Assert.Contains("ICustomer360ReadService, Customer360ReadService",p);Assert.Contains("ICustomer360FoundationProvider, InMemoryCustomer360FoundationProvider",p);Assert.DoesNotContain("/api/crm/foundation/customer360",p,StringComparison.OrdinalIgnoreCase);Assert.Contains("false,false,false",s.Replace(" ",string.Empty),StringComparison.OrdinalIgnoreCase);}
}
