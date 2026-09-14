namespace CRM.Domain.DocumentMetadata;

public sealed record DocumentMetadataRuleResult(
    string? DocumentId,
    DocumentMetadataOperation Operation,
    bool Allowed,
    bool Changed,
    DocumentMetadataErrorCode ErrorCode,
    string Message,
    DocumentRelatedEntityType RelatedEntityType,
    string? NormalizedRelatedEntityId,
    string? NormalizedFileReferenceId,
    string? NormalizedFileName,
    string? NormalizedContentType,
    string? NormalizedDescription,
    DocumentMetadataStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == DocumentMetadataErrorCode.None;
}
