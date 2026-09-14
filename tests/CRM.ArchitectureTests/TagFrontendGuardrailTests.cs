using Xunit;
namespace CRM.ArchitectureTests;
public sealed class TagFrontendGuardrailTests
{
 static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2304_UsesFoundationTagApiOnly(){var s=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));Assert.Contains("/api/crm/foundation/tags",s);Assert.Contains("path: 'foundation/tags'",s);Assert.Contains("class TagManagementPageComponent",s);Assert.DoesNotContain("deleteTag(",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("'/api/crm/tags'",s,StringComparison.Ordinal);}
}
