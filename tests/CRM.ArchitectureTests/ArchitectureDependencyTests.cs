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

    private static IReadOnlySet<string> ReferencedAssemblyNames(Assembly assembly) =>
        assembly.GetReferencedAssemblies().Select(reference => reference.Name ?? "").ToHashSet(StringComparer.OrdinalIgnoreCase);
}
