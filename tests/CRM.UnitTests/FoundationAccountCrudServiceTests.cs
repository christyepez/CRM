using CRM.Application.Foundation;
using CRM.Application.Portal;
using CRM.Infrastructure.Persistence.Foundation;
using CRM.Infrastructure.Portal.Simulation;
using Xunit;

namespace CRM.UnitTests;

public sealed class FoundationAccountCrudServiceTests
{
    [Fact]
    public async Task CreateAsync_ReturnsFoundationMetadataWithoutDurablePersistence()
    {
        var service = new FoundationAccountCrudService(
            new InMemoryAccountFoundationStore(),
            new StaticCrmPersistenceFeatureFlagProvider(),
            new CrmFoundationPermissionGuard(new SimulatedPortalPermissionProvider()));

        var result = await service.CreateAsync(new FoundationAccountCreateRequest("Preview Account", "Services", "SMB", "555-0202", "account@example.test"));

        Assert.True(result.Allowed);
        Assert.NotNull(result.Data);
        Assert.Equal("NonProductionSeam", result.PersistenceMode);
        Assert.Equal("FoundationSimulation", result.AuthorizationMode);
        Assert.False(result.ProductiveCrudEnabled);
        Assert.False(result.DurablePersistence);
        Assert.False(result.DatabaseConfigured);
    }
}
