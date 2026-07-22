using System.Reflection;
using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ArchitectureDependencyTests
{
    [Fact]
    public void Domain_DoesNotDependOnApplicationInfrastructureOrApi()
    {
        var references = ReferencedAssemblyNames(typeof(Domain.AssemblyMarker).Assembly);

        Assert.DoesNotContain("CRM.Application", references);
        Assert.DoesNotContain("CRM.Infrastructure", references);
        Assert.DoesNotContain("CRM.Api", references);
    }

    [Fact]
    public void Application_DoesNotDependOnInfrastructureOrApi()
    {
        var references = ReferencedAssemblyNames(typeof(Application.Foundation.CrmReadinessService).Assembly);

        Assert.DoesNotContain("CRM.Infrastructure", references);
        Assert.DoesNotContain("CRM.Api", references);
    }

    [Fact]
    public void Domain_IsNotCoupledToIdentity()
    {
        var references = ReferencedAssemblyNames(typeof(Domain.AssemblyMarker).Assembly);

        Assert.DoesNotContain(references, name => name.Contains("Identity", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Infrastructure_DoesNotContainProductivePersistenceYet()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"));

        Assert.DoesNotContain("DbContext", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbSet<", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MigrationBuilder", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Api_OnlyExposesAllowedContractAndFoundationPreviewEndpoints()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("/api/crm/domain-catalog", program);
        Assert.Contains("/api/crm/contracts", program);
        Assert.Contains("/api/crm/integration-boundaries", program);
        Assert.Contains("/api/crm/foundation/leads/preview", program);
        Assert.Contains("/api/crm/foundation/accounts/preview", program);
        Assert.Contains("/api/crm/foundation/contacts/preview", program);
        Assert.Contains("/api/crm/foundation/leads/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/accounts/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/contacts/read-model-preview", program);
        Assert.Contains("/api/crm/foundation/read-model-status", program);
        Assert.Contains("/api/crm/foundation/portal-integration/status", program);
        Assert.Contains("/api/crm/foundation/portal-integration/contracts", program);
        Assert.Contains("/api/crm/foundation/portal-integration/required-capabilities", program);
        Assert.Contains("/api/crm/foundation/financial-integration/status", program);
        Assert.Contains("/api/crm/foundation/financial-integration/contracts", program);
        Assert.Contains("/api/crm/foundation/financial-integration/required-capabilities", program);
        Assert.Contains("/api/crm/foundation/financial-integration/events", program);
        Assert.Contains("/api/crm/foundation/reporting/status", program);
        Assert.Contains("/api/crm/foundation/reporting/kpis", program);
        Assert.Contains("/api/crm/foundation/reporting/dashboards", program);
        Assert.Contains("/api/crm/foundation/reporting/analytics-read-models", program);
        Assert.Contains("/api/crm/foundation/sprint-1/closure-status", program);
        Assert.Contains("/api/crm/foundation/persistence/readiness", program);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("\"/api/crm/accounts\"", program);
        Assert.DoesNotContain("\"/api/crm/contacts\"", program);
        Assert.DoesNotContain("MapPut", program);
        Assert.DoesNotContain("MapPatch", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("Create" + "Lead", program);
    }

    [Fact]
    public void Source_DoesNotContainIdentityTokenStorageOrOwnSqlServer()
    {
        var source = ReadSourceFiles("src", "tests", "frontend", "docker-compose.yml", "docker-compose.crm.yml");

        Assert.DoesNotContain("Microsoft.AspNetCore." + "Identity", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Add" + "Identity", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("mcr.microsoft.com/" + "mssql", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void PortalPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Portal");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FuturePortalAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("BaseUrl", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferencePortalIntegrationPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Portal", source, StringComparison.Ordinal);
        Assert.DoesNotContain("IPortal", source, StringComparison.Ordinal);
    }

    [Fact]
    public void PortalPlaceholder_DoesNotMakeRuntimePortalCalls()
    {
        var source = ReadSourceFiles("src", "CRM.Infrastructure");

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("PortalCorporativoUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void PortalEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/contracts\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/portal-integration/required-capabilities\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/portal-integration", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/portal-integration", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/portal-integration", program);
    }

    [Fact]
    public void FinancialPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Financial");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FutureFinancialAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("ConnectionString", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceFinancialIntegrationPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Financial", source, StringComparison.Ordinal);
        Assert.DoesNotContain("IFinancial", source, StringComparison.Ordinal);
    }

    [Fact]
    public void FinancialPlaceholder_DoesNotMakeRuntimeFinancialCallsOrDbAccess()
    {
        var source = ReadSourceFiles("src", "CRM.Infrastructure");

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("FinancieroUrl", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void FinancialEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/contracts\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/required-capabilities\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/financial-integration/events\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/financial-integration", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/financial-integration", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/financial-integration", program);
    }

    [Fact]
    public void Source_DoesNotReferenceFinancieroRuntimeSriOrSharedDatabase()
    {
        var source = ReadSourceFiles("src", "docker-compose.yml", "docker-compose.crm.yml");

        Assert.DoesNotContain("ProjectReference Include=\"..\\..\\Financiero", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("FinancieroDb", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SharedDatabase", source.Replace("NoSharedDatabase", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SriClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("RIDE", source, StringComparison.Ordinal);
        Assert.DoesNotContain("XAdES", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ATS", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingPorts_AreApplicationInterfacesOnly()
    {
        var root = FindRepositoryRoot();
        var portsPath = Path.Combine(root, "src", "CRM.Application", "Ports", "Reporting");

        Assert.True(Directory.Exists(portsPath));
        foreach (var file in Directory.EnumerateFiles(portsPath, "*.cs"))
        {
            var source = File.ReadAllText(file);

            Assert.Contains("interface", source, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FutureReportingAdapter", source, StringComparison.Ordinal);
            Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("PowerBI", source, StringComparison.Ordinal);
            Assert.DoesNotContain("EmbedToken", source, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void Domain_DoesNotReferenceReportingPorts()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Domain"));

        Assert.DoesNotContain("CRM.Application.Ports.Reporting", source, StringComparison.Ordinal);
        Assert.DoesNotContain("ICrmKpi", source, StringComparison.Ordinal);
        Assert.DoesNotContain("ICrmDashboard", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingPlaceholder_DoesNotUsePowerBiRuntimeOrIds()
    {
        var source = ReadSourceFiles(Path.Combine("src", "CRM.Infrastructure"))
            .Replace("FuturePowerBiEmbedding", string.Empty, StringComparison.Ordinal)
            .Replace("\"EmbedToken\"", string.Empty, StringComparison.Ordinal);

        Assert.DoesNotContain("HttpClient", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Microsoft.PowerBI", source, StringComparison.Ordinal);
        Assert.DoesNotContain("EmbedToken", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("WorkspaceId", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ReportId", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DatasetId", source, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("NonProductionPlaceholder", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ReportingEndpoints_AreFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/status\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/kpis\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/dashboards\"", program);
        Assert.Contains("MapGet(\"/api/crm/foundation/reporting/analytics-read-models\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/reporting", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/reporting", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/reporting", program);
    }

    [Fact]
    public void ClosureEndpoint_IsFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/sprint-1/closure-status\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-1", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-1", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/sprint-1", program);
        Assert.Contains("Foundation closure only; no productive activation", ReadSourceFiles("src", "CRM.Application"));
    }

    [Fact]
    public void PersistenceReadinessEndpoint_IsFoundationGetOnly()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.Contains("MapGet(\"/api/crm/foundation/persistence/readiness\"", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/persistence", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/persistence", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/persistence", program);
        Assert.Contains("Persistence design review only; no database configured", ReadSourceFiles("src", "CRM.Application"));
    }

    private static IReadOnlySet<string> ReferencedAssemblyNames(Assembly assembly) =>
        assembly.GetReferencedAssemblies().Select(reference => reference.Name ?? "").ToHashSet(StringComparer.OrdinalIgnoreCase);

    private static string ReadSourceFiles(params string[] roots)
    {
        var repositoryRoot = FindRepositoryRoot();
        var contents = new List<string>();
        foreach (var root in roots)
        {
            var path = Path.Combine(repositoryRoot, root);
            if (File.Exists(path))
            {
                contents.Add(File.ReadAllText(path));
                continue;
            }

            if (!Directory.Exists(path))
            {
                continue;
            }

            foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}node_modules{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}dist{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}.angular{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase))
                         .Where(file => !file.Contains($"{Path.DirectorySeparatorChar}tools{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)))
            {
                contents.Add(File.ReadAllText(file));
            }
        }

        return string.Join(Environment.NewLine, contents);
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "CRM.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("Repository root was not found.");
    }
}
