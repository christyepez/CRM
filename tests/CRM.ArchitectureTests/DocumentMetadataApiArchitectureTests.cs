using Xunit;
namespace CRM.ArchitectureTests;
public sealed class DocumentMetadataApiArchitectureTests
{
 private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2203_OnlyFoundationDocumentMetadataRoutesAreExposed(){var p=File.ReadAllText(Path.Combine(Root(),"src","CRM.Api","Program.cs"));Assert.Contains("MapGet(\"/api/crm/foundation/documents\"",p);Assert.Contains("MapPost(\"/api/crm/foundation/documents\"",p);Assert.Contains("MapPut(\"/api/crm/foundation/documents/{id}\"",p);Assert.Contains("/archive",p);Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/documents",p,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("MapGet(\"/api/crm/documents",p,StringComparison.OrdinalIgnoreCase);}
}
