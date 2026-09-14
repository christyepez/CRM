using CRM.Application.PipelineCatalog;

namespace CRM.Api.Foundation;

public sealed record PipelineStageApiItem(string Id,string Name,int Order);
public sealed record PipelineCatalogApiItem(string Id,string Name,IReadOnlyCollection<PipelineStageApiItem> Stages,string SourceMode,bool Mutable,bool PortalCatalogRuntimeEnabled,bool FoundationMode);

public static class PipelineCatalogApiResponse
{
    public static PipelineCatalogApiItem From(PipelineCatalogApplicationItem item) => new(
        item.Id,item.Name,item.Stages.Select(x=>new PipelineStageApiItem(x.Id,x.Name,x.Order)).ToArray(),
        item.SourceMode,item.Mutable,item.PortalCatalogRuntimeEnabled,FoundationMode:true);
}
