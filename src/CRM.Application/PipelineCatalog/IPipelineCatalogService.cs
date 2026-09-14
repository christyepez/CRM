namespace CRM.Application.PipelineCatalog;

public interface IPipelineCatalogService
{
    Task<IReadOnlyCollection<PipelineCatalogApplicationItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PipelineCatalogApplicationItem?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
