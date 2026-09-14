using Xunit;
namespace CRM.ArchitectureTests;
public sealed class TagManagementArchitectureTests
{
 static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2302_RemainsFoundationOnly(){var r=Root();var svc=File.ReadAllText(Path.Combine(r,"src","CRM.Application","TagManagement","TagManagementService.cs"));var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));Assert.Contains("ITagFoundationStore",svc);Assert.Contains("PortalIdentityRuntimeEnabled",File.ReadAllText(Path.Combine(r,"src","CRM.Application","TagManagement","TagApplicationContracts.cs")));Assert.Contains("ITagManagementService, TagManagementService",p);Assert.DoesNotContain("/api/crm/foundation/tags",p,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("DbContext",svc,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("HttpClient",svc,StringComparison.OrdinalIgnoreCase);}
}
