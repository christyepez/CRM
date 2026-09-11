using Xunit;

namespace CRM.ArchitectureTests;

public sealed class CaseManagementArchitectureTests
{
    private static string Root()
    {
        var current = AppContext.BaseDirectory;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "CRM.sln")))
                return current;
            current = Directory.GetParent(current)?.FullName;
        }

        throw new DirectoryNotFoundException();
    }

    [Fact]
    public void CaseDomain_RemainsDependencyFree()
    {
        var root = Root();
        var files = Directory.GetFiles(Path.Combine(root, "src", "CRM.Domain", "CaseManagement"), "*.cs");
        var text = string.Join("\n", files.Select(File.ReadAllText));
        foreach (var marker in new[] { "CRM.Application", "CRM.Infrastructure", "CRM.Api", "Microsoft.EntityFrameworkCore", "HttpClient", "DbContext" })
            Assert.DoesNotContain(marker, text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CasePolicy_ContainsLifecycleAndLimits()
    {
        var root = Root();
        var source = File.ReadAllText(Path.Combine(root, "src", "CRM.Domain", "CaseManagement", "CaseManagementPolicy.cs"));
        foreach (var marker in new[] { "MaxTitleLength = 160", "MaxSummaryLength = 1000", "CaseStatus.Open", "CaseStatus.InProgress", "CaseStatus.Resolved", "CaseStatus.Closed", "EvaluateStart", "EvaluateResolve", "EvaluateClose" })
            Assert.Contains(marker, source, StringComparison.Ordinal);
    }

    [Fact]
    public void CaseRoutes_RemainUnavailableInDomainStory()
    {
        var root = Root();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        foreach (var marker in new[] { "MapGet(\"/api/crm/cases", "MapPost(\"/api/crm/cases", "MapPut(\"/api/crm/cases", "MapDelete(\"/api/crm/cases", "MapGet(\"/api/crm/foundation/cases", "MapPost(\"/api/crm/foundation/cases", "MapPut(\"/api/crm/foundation/cases", "MapDelete(\"/api/crm/foundation/cases" })
            Assert.DoesNotContain(marker, program, StringComparison.OrdinalIgnoreCase);
    }
}

