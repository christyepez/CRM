namespace CRM.Application.PipelineCatalog;

public sealed record PipelineStageApplicationItem(
    string Id,
    string Name,
    int Order);

public sealed record PipelineCatalogApplicationItem(
    string Id,
    string Name,
    IReadOnlyCollection<PipelineStageApplicationItem> Stages,
    string SourceMode,
    bool Mutable,
    bool PortalCatalogRuntimeEnabled);
