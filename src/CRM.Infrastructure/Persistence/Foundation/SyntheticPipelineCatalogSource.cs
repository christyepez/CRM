using CRM.Application.Ports.PipelineCatalog;
using CRM.Domain.PipelineCatalog;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class SyntheticPipelineCatalogSource : IPipelineCatalogSource
{
    private static readonly IReadOnlyCollection<PipelineCatalogRecord> Catalogs =
    [
        new(
            "88888888-8888-8888-8888-888888888888",
            "Synthetic Sales Pipeline",
            [
                new("88888888-8888-8888-8888-888888888881", "Qualification", 1),
                new("88888888-8888-8888-8888-888888888882", "Proposal", 2),
                new("88888888-8888-8888-8888-888888888883", "Negotiation", 3),
                new("88888888-8888-8888-8888-888888888884", "Closed", 4)
            ])
    ];

    public Task<IReadOnlyCollection<PipelineCatalogRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Catalogs);
    }

    public Task<PipelineCatalogRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Catalogs.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase)));
    }
}
