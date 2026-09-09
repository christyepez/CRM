using Xunit;

namespace CRM.ArchitectureTests;

public sealed class AccountManagementArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void AccountManagementDomain_RemainsIndependent()
    {
        var root=Root();
        var dir=Path.Combine(root,"src","CRM.Domain","AccountManagement");
        Assert.True(Directory.Exists(dir));
        var source=string.Join("\n",Directory.GetFiles(dir,"*.cs").Select(File.ReadAllText));
        foreach(var forbidden in new[]{"CRM.Application","CRM.Infrastructure","CRM.Api","EntityFrameworkCore","HttpClient","DbContext"}) Assert.DoesNotContain(forbidden,source,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AccountManagement_HasNoDeleteOperation()
    {
        var root=Root();
        var source=File.ReadAllText(Path.Combine(root,"src","CRM.Domain","AccountManagement","AccountManagementOperation.cs"));
        Assert.DoesNotContain("Delete",source,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveAccountAndDeleteRoutes_RemainAbsent()
    {
        var root=Root();
        var program=File.ReadAllText(Path.Combine(root,"src","CRM.Api","Program.cs"));
        Assert.Contains("/api/crm/foundation/accounts",program,StringComparison.Ordinal);
        foreach(var forbidden in new[]{"MapGet(\"/api/crm/accounts","MapPost(\"/api/crm/accounts","MapPut(\"/api/crm/accounts","MapDelete(\"/api/crm/accounts","MapDelete(\"/api/crm/foundation/accounts"}) Assert.DoesNotContain(forbidden,program,StringComparison.OrdinalIgnoreCase);
    }
}
