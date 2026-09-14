using Xunit;
namespace CRM.ArchitectureTests;
public sealed class Customer360ArchitectureTests
{
 static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2503_ExposesReadOnlyFoundationRoutesOnly(){var r=Root();var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));Assert.Contains("/api/crm/foundation/customer360",p);Assert.Contains("/api/crm/foundation/customer360/{customerId}",p);Assert.DoesNotContain("MapPost(\"/api/crm/foundation/customer360",p);Assert.DoesNotContain("MapPut(\"/api/crm/foundation/customer360",p);Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/customer360",p);Assert.DoesNotContain("\"/api/crm/customer360\"",p);}
}
