using CRM.Domain.PipelineCatalog;

namespace CRM.Application.Ports.PipelineCatalog;

/// <summary>NonProductionPersistenceSeam: deterministic synthetic pipeline catalog only.</summary>
public interface IPipelineCatalogSource
{
    Task<IReadOnlyCollection<PipelineCatalogRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PipelineCatalogRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
