namespace CRM.Domain.PipelineCatalog;

public sealed record PipelineStageCatalogRecord(
    string Id,
    string Name,
    int Order);

public sealed record PipelineCatalogRecord(
    string Id,
    string Name,
    IReadOnlyCollection<PipelineStageCatalogRecord> Stages);

public enum PipelineCatalogErrorCode
{
    None = 0,
    InvalidPipelineId,
    PipelineNameRequired,
    PipelineNameTooLong,
    StagesRequired,
    InvalidStageId,
    StageNameRequired,
    StageNameTooLong,
    InvalidStageOrder,
    DuplicateStageOrder
}

public sealed record PipelineCatalogValidationResult(
    bool Valid,
    PipelineCatalogErrorCode ErrorCode,
    string Message,
    PipelineCatalogRecord? Catalog)
{
    public static PipelineCatalogValidationResult Success(PipelineCatalogRecord catalog) =>
        new(true, PipelineCatalogErrorCode.None, "Pipeline catalog is valid.", catalog);

    public static PipelineCatalogValidationResult Reject(PipelineCatalogErrorCode code, string message) =>
        new(false, code, message, null);
}
