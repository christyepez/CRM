using CRM.Application.PipelineCatalog;
using CRM.Application.Ports.PipelineCatalog;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class PipelineCatalogServiceTests
{
    [Fact]
    public async Task GetAll_ReturnsDeterministicReadOnlyCatalog()
    {
        var service = new PipelineCatalogService(new SyntheticPipelineCatalogSource());
        var items = await service.GetAllAsync();
        var item = Assert.Single(items);
        Assert.Equal("Synthetic Sales Pipeline", item.Name);
        Assert.Equal(new[]{1,2,3,4}, item.Stages.Select(x=>x.Order).ToArray());
        Assert.False(item.Mutable);
        Assert.False(item.PortalCatalogRuntimeEnabled);
        Assert.Equal("DeterministicSyntheticFoundation", item.SourceMode);
    }

    [Fact]
    public async Task GetById_Unknown_ReturnsNull()
    {
        var service = new PipelineCatalogService(new SyntheticPipelineCatalogSource());
        Assert.Null(await service.GetByIdAsync(Guid.NewGuid().ToString("D")));
    }
}
