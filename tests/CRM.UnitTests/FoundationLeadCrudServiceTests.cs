using CRM.Application.Foundation;
using CRM.Application.Portal;
using CRM.Infrastructure.Persistence.Foundation;
using CRM.Infrastructure.Portal.Simulation;
using Xunit;

namespace CRM.UnitTests;

public sealed class FoundationLeadCrudServiceTests
{
    [Fact]
    public async Task CreateAndUpdateAsync_UseFoundationStoreAndSimulationPermission()
    {
        var service = CreateService();

        var created = await service.CreateAsync(new FoundationLeadCreateRequest(" Ada ", " Preview ", "ada@example.test", "Preview Co", "Buyer", "555-0101"));
        var updated = await service.UpdateAsync(created.Data!.Id, new FoundationLeadUpdateRequest("Ada", "Preview", "ada@example.test", "Preview Co", "Sponsor", "555-0102", "Contacted"));
        var fetched = await service.GetByIdAsync(created.Data.Id);

        Assert.True(created.Allowed);
        Assert.True(updated.Allowed);
        Assert.Equal("FoundationSimulation", created.AuthorizationMode);
        Assert.False(created.ProductiveCrudEnabled);
        Assert.False(created.DurablePersistence);
        Assert.False(created.DatabaseConfigured);
        Assert.Equal("Contacted", fetched.Data!.Status);
    }

    private static FoundationLeadCrudService CreateService()
    {
        var permissionProvider = new SimulatedPortalPermissionProvider();
        return new FoundationLeadCrudService(
            new InMemoryLeadFoundationStore(),
            new StaticCrmPersistenceFeatureFlagProvider(),
            new CrmFoundationPermissionGuard(permissionProvider));
    }
}
