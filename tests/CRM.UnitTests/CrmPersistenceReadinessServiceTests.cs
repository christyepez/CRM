using CRM.Application.Persistence;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPersistenceReadinessServiceTests
{
    [Fact]
    public void GetReadiness_ReturnsDesignOnlyWithoutDatabase()
    {
        var response = new CrmPersistenceReadinessService().GetReadiness();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("PersistenceDesignReview", response.Status);
        Assert.Equal("DesignOnly", response.PersistenceMode);
        Assert.False(response.DatabaseConfigured);
        Assert.False(response.MigrationReady);
        Assert.False(response.DbContextConfigured);
        Assert.False(response.SqlServerOwnedByCrm);
        Assert.Equal("NonProduction", response.RuntimeMode);
        Assert.Equal("Sprint2P2PersistenceSeam", response.NextGate);
        Assert.Equal("Persistence design review only; no database configured", response.Warning);
        Assert.Contains("Lead", response.FirstAggregates);
        Assert.Contains("Opportunity", response.DeferredAggregates);
    }
}
