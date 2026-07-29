using Xunit;

namespace CRM.ArchitectureTests;

public sealed class LockedProductiveRouteRuntimeRegistrationTests
{
    [Fact]
    public void Registrar_IsDefaultDisabledAndRegistersOnlyLockedNonProductionMethods()
    {
        var root = FindRepositoryRoot();
        var registrar = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "ProductiveRoutes", "LockedProductiveRouteRuntimeRegistration.cs"));
        var options = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "ProductiveRoutes", "LockedProductiveRouteRuntimeRegistrationOptions.cs"));
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));

        Assert.Contains("Crm:ProductiveRoutes:LockedRegistrationEnabled", registrar);
        Assert.Contains("LockedRegistrationEnabled", options);
        Assert.Contains("TryMapLockedProductiveRoutes", program);
        Assert.Contains("IsProduction", registrar);
        Assert.Contains("Status423Locked", registrar);
        Assert.Contains("MapGet", registrar);
        Assert.Contains("MapPost", registrar);
        Assert.Contains("MapPut", registrar);
        Assert.Contains("MapPatch", registrar);
        Assert.DoesNotContain("MapDelete", registrar);
        Assert.DoesNotContain("SqlConnection", registrar);
        Assert.DoesNotContain("DbConnection", registrar);
        Assert.DoesNotContain("UseSqlServer", registrar);
        Assert.DoesNotContain("AddDbContext", registrar);
        Assert.DoesNotContain("HttpClient", registrar);
        Assert.DoesNotContain("Request.Headers", registrar);
        Assert.DoesNotContain("Authorization", registrar);
        Assert.DoesNotContain("password", registrar, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("secret", registrar, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void P5DocumentationAndServiceMarkersExist()
    {
        var root = FindRepositoryRoot();
        var source = ReadText(root, "src", "CRM.Application", "Foundation", "CrmLockedProductiveRouteRuntimeRegistrationStatusService.cs");
        var contracts = ReadText(root, "src", "CRM.Application", "Foundation", "CrmLockedProductiveRouteRuntimeRegistrationContracts.cs");

        Assert.Contains("LockedProductiveRouteRuntimeRegistrationWith423", source);
        Assert.Contains("Locked productive routes are not registered by default; explicit NonProduction flag returns 423 without side effects", source);
        Assert.Contains("ProductiveRoutesRegisteredByDefault: false", source);
        Assert.Contains("ProductiveRoutesRegisteredWhenExplicitlyEnabled: true", source);
        Assert.Contains("DefaultNegativeRouteStatus: 404", source);
        Assert.Contains("ExplicitlyEnabledLockedRouteStatus: 423", source);
        Assert.Contains("ProductiveCrudEnabled: false", source);
        Assert.Contains("ProductiveDomainExecutionEnabled: false", source);
        Assert.Contains("ProductivePersistenceEnabled: false", source);
        Assert.Contains("DeleteEndpointsEnabled: false", source);
        Assert.Contains("PortalAuthRuntimeEnabled: false", source);
        Assert.Contains("DbRuntimeEnabled: false", source);
        Assert.Contains("SideEffectsAllowed: false", source);
        Assert.Contains("Sprint7P6Sprint7GateDecision", source);
        Assert.Contains("CrmLockedProductiveRouteRuntimeRegistrationStatusResponse", contracts);

        foreach (var path in new[]
        {
            Path.Combine(root, "docs", "api", "crm-sprint-7-p5-locked-productive-route-runtime-registration-with-423.md"),
            Path.Combine(root, "docs", "api", "crm-locked-productive-route-runtime-registration-policy.md"),
            Path.Combine(root, "docs", "api", "crm-locked-productive-route-runtime-registration-contract.md"),
            Path.Combine(root, "docs", "security", "crm-locked-productive-route-runtime-registration-safety-boundary.md"),
            Path.Combine(root, "docs", "operations", "crm-locked-productive-route-runtime-registration-runbook.md"),
            Path.Combine(root, "docs", "operations", "crm-locked-productive-route-runtime-registration-rollback.md"),
            Path.Combine(root, "docs", "architecture", "crm-locked-productive-route-runtime-registration-architecture.md")
        })
        {
            Assert.True(File.Exists(path), path);
        }
    }

    private static string ReadText(params string[] parts) => File.ReadAllText(Path.Combine(parts));

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
