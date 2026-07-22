using CRM.Infrastructure.Persistence.EfPrototype;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmEfPrototypeGuardrailTests
{
    [Fact]
    public void PrototypeOptions_AreDisabledByDefault()
    {
        var options = CrmEfPrototypeOptions.Disabled;

        Assert.True(options.EfPrototypeExists);
        Assert.False(options.EfRuntimeEnabled);
        Assert.False(options.DbContextRuntimeActive);
        Assert.False(options.MigrationsEnabled);
        Assert.False(options.DurablePersistenceEnabled);
        Assert.False(options.ProductiveCrudEnabled);
    }

    [Fact]
    public void Prototype_IsPlaceholderOnly()
    {
        var prototype = new CrmDbContextPrototype();

        Assert.False(prototype.Options.EfRuntimeEnabled);
        Assert.False(prototype.Options.DbContextRuntimeActive);
        Assert.Contains("placeholder only", CrmDbContextPrototype.Warning);
        Assert.False(CrmEfPrototypeMarker.RuntimeActive);
    }
}
