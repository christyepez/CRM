using Xunit;

namespace CRM.ArchitectureTests;

public sealed class AccountApiArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void FoundationAccountApi_UsesDedicatedApplicationService()
    {
        var root=Root();
        var program=File.ReadAllText(Path.Combine(root,"src","CRM.Api","Program.cs"));
        Assert.Contains("IAccountManagementService",program,StringComparison.Ordinal);
        Assert.Contains("/api/crm/foundation/accounts/{id}/activate",program,StringComparison.Ordinal);
        Assert.Contains("/api/crm/foundation/accounts/{id}/deactivate",program,StringComparison.Ordinal);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/accounts",program,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapGet(\"/api/crm/accounts",program,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AccountApiContracts_DoNotBindDomainEntitiesOrDbContext()
    {
        var root=Root();
        var source=File.ReadAllText(Path.Combine(root,"src","CRM.Api","Foundation","AccountManagementApiContracts.cs"));
        Assert.Contains("AccountManagementCreateRequest",source,StringComparison.Ordinal);
        Assert.Contains("AccountManagementUpdateRequest",source,StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Domain.Entities.Account",source,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext",source,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("EntityFrameworkCore",source,StringComparison.OrdinalIgnoreCase);
    }
}
