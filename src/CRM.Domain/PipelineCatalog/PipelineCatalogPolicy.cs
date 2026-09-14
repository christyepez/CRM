namespace CRM.Domain.PipelineCatalog;

public static class PipelineCatalogPolicy
{
    public const int MaxNameLength = 120;

    public static PipelineCatalogValidationResult Validate(PipelineCatalogRecord catalog)
    {
        var id = Normalize(catalog.Id);
        var name = Normalize(catalog.Name);
        if (!ValidId(id)) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.InvalidPipelineId, "Pipeline id must be a non-empty GUID.");
        if (name is null) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.PipelineNameRequired, "Pipeline name is required.");
        if (name.Length > MaxNameLength) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.PipelineNameTooLong, "Pipeline name is too long.");
        if (catalog.Stages is null || catalog.Stages.Count == 0) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.StagesRequired, "At least one stage is required.");

        var normalized = new List<PipelineStageCatalogRecord>();
        var orders = new HashSet<int>();
        foreach (var stage in catalog.Stages)
        {
            var stageId = Normalize(stage.Id);
            var stageName = Normalize(stage.Name);
            if (!ValidId(stageId)) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.InvalidStageId, "Stage id must be a non-empty GUID.");
            if (stageName is null) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.StageNameRequired, "Stage name is required.");
            if (stageName.Length > MaxNameLength) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.StageNameTooLong, "Stage name is too long.");
            if (stage.Order <= 0) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.InvalidStageOrder, "Stage order must be positive.");
            if (!orders.Add(stage.Order)) return PipelineCatalogValidationResult.Reject(PipelineCatalogErrorCode.DuplicateStageOrder, "Stage order values must be unique.");
            normalized.Add(new(stageId!, stageName, stage.Order));
        }

        return PipelineCatalogValidationResult.Success(new(id!, name, normalized.OrderBy(x => x.Order).ToArray()));
    }

    private static string? Normalize(string? value){var v=(value??string.Empty).Trim();return v.Length==0?null:v;}
    private static bool ValidId(string? value)=>Guid.TryParse(value,out var id)&&id!=Guid.Empty;
}
