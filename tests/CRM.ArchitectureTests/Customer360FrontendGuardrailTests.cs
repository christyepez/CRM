using Xunit;
namespace CRM.ArchitectureTests;
public sealed class Customer360FrontendGuardrailTests
{
 static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2504_PageIsReadOnlyFoundationOnly(){var m=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));var start=m.IndexOf("type Customer360View",StringComparison.Ordinal);var end=m.IndexOf("const routes: Routes",start,StringComparison.Ordinal);var s=m[start..end];Assert.Contains("/api/crm/foundation/customer360",s);Assert.DoesNotContain(".post<",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain(".put<",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain(".delete<",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("formControl",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("(ngSubmit)",s,StringComparison.OrdinalIgnoreCase);Assert.Contains("productiveRuntimeEnabled",s);Assert.Contains("portalRuntimeEnabled",s);Assert.Contains("commonDbRuntimeEnabled",s);}
}
