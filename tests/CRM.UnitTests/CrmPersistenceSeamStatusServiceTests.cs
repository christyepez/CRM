using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmPersistenceSeamStatusServiceTests
{
    [Fact]
    public async Task GetStatusAsync_ReturnsNonProductionSeamWithoutDurablePersistence()
    {
        var service = CreateService();

        var response = await service.GetStatusAsync();

        Assert.Equal("CRM", response.Module);
        Assert.Equal("PersistenceSeamActive", response.Status);
        Assert.Equal("NonProductionSeam", response.PersistenceMode);
        Assert.True(response.FoundationStoreEnabled);
        Assert.False(response.DatabaseConfigured);
        Assert.False(response.DbContextConfigured);
        Assert.False(response.MigrationReady);
        Assert.False(response.DurablePersistence);
        Assert.False(response.ProductiveCrudEnabled);
        Assert.Equal("NonProduction", response.RuntimeMode);
        Assert.Equal("Sprint2P3PortalAuthorizationAdapterSimulation", response.NextGate);
        Assert.Equal(CrmPersistenceSeamStatusService.WarningText, response.Warning);
        Assert.All(response.Stores, store => Assert.False(store.DurablePersistence));
    }

    [Fact]
    public void GetFeatureFlags_ReturnsSafeHardcodedFlags()
    {
        var flags = new StaticCrmPersistenceFeatureFlagProvider().GetFeatureFlags();

        Assert.True(flags.FoundationMode);
        Assert.True(flags.PersistenceSeamEnabled);
        Assert.False(flags.PersistenceEnabled);
        Assert.False(flags.ProductiveCrudEnabled);
        Assert.False(flags.DurablePersistenceEnabled);
        Assert.Equal(CrmPersistenceSeamStatusService.WarningText, flags.Warning);
    }

    [Fact]
    public async Task ClearPreviewAsync_ClearsOnlyInMemoryPreviewState()
    {
        var service = CreateService();

        var response = await service.ClearPreviewAsync();

        Assert.All(response.Stores, store => Assert.Equal(0, store.PreviewCount));
        Assert.False(response.DurablePersistence);
        Assert.False(response.DatabaseConfigured);
    }

    private static CrmPersistenceSeamStatusService CreateService()
    {
        ICrmPersistenceFeatureFlagProvider flags = new StaticCrmPersistenceFeatureFlagProvider();
        ILeadFoundationStore leads = new InMemoryLeadFoundationStore();
        IAccountFoundationStore accounts = new InMemoryAccountFoundationStore();
        IContactFoundationStore contacts = new InMemoryContactFoundationStore();

        return new CrmPersistenceSeamStatusService(flags, leads, accounts, contacts);
    }
}
