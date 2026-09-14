using CRM.Application.Ports.PipelineCatalog;
using CRM.Domain.PipelineCatalog;

namespace CRM.Application.PipelineCatalog;

public sealed class PipelineCatalogService(IPipelineCatalogSource source) : IPipelineCatalogService
{
    private const string SourceMode = "DeterministicSyntheticFoundation";

    public async Task<IReadOnlyCollection<PipelineCatalogApplicationItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var records = await source.GetAllAsync(cancellationToken);
        return records.Select(ValidateAndMap).ToArray();
    }

    public async Task<PipelineCatalogApplicationItem?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var record = await source.GetByIdAsync(id, cancellationToken);
        return record is null ? null : ValidateAndMap(record);
    }

    private static PipelineCatalogApplicationItem ValidateAndMap(PipelineCatalogRecord record)
    {
        var validation = PipelineCatalogPolicy.Validate(record);
        if (!validation.Valid || validation.Catalog is null)
            throw new InvalidOperationException($"Synthetic pipeline catalog is invalid: {validation.ErrorCode}");

        var catalog = validation.Catalog;
        return new(
            catalog.Id,
            catalog.Name,
            catalog.Stages.Select(x => new PipelineStageApplicationItem(x.Id, x.Name, x.Order)).ToArray(),
            SourceMode,
            Mutable: false,
            PortalCatalogRuntimeEnabled: false);
    }
}
