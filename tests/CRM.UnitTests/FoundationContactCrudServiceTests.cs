using CRM.Application.Foundation;
using CRM.Application.Portal;
using CRM.Infrastructure.Persistence.Foundation;
using CRM.Infrastructure.Portal.Simulation;
using Xunit;

namespace CRM.UnitTests;

public sealed class FoundationContactCrudServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsSeedPreviewFromFoundationStore()
    {
        var service = new FoundationContactCrudService(
            new InMemoryContactFoundationStore(),
            new StaticCrmPersistenceFeatureFlagProvider(),
            new CrmFoundationPermissionGuard(new SimulatedPortalPermissionProvider()));

        var result = await service.GetAllAsync();

        Assert.True(result.Allowed);
        Assert.NotEmpty(result.Data!);
        Assert.Equal("Foundation CRUD only; no productive endpoint or database configured", result.Warning);
        Assert.False(result.ProductiveCrudEnabled);
    }
}
