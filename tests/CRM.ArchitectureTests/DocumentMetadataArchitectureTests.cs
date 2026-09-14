using Xunit;
namespace CRM.ArchitectureTests;
public sealed class DocumentMetadataArchitectureTests
{
 private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
 [Fact] public void S2202_RemainsMetadataOnlyFoundationSlice(){var r=Root();var service=File.ReadAllText(Path.Combine(r,"src","CRM.Application","DocumentMetadata","DocumentMetadataService.cs"));var program=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));Assert.Contains("BinaryStorageEnabled",File.ReadAllText(Path.Combine(r,"src","CRM.Application","DocumentMetadata","DocumentMetadataApplicationContracts.cs")));Assert.Contains("IDocumentMetadataFoundationStore",service);Assert.Contains("IDocumentMetadataService, DocumentMetadataService",program);Assert.DoesNotContain("/api/crm/foundation/documents",program,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("Content/File",service,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("FileStream",service,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("Blob",service,StringComparison.OrdinalIgnoreCase);}
}
