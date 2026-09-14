using CRM.Domain.DocumentMetadata;
namespace CRM.Application.Ports.Persistence;
public sealed record DocumentMetadataFoundationRecord(string Id,DocumentRelatedEntityType RelatedEntityType,string RelatedEntityId,string FileReferenceId,string FileName,string? ContentType,string? Description,DocumentMetadataStatus Status);
/// <summary>FoundationOnly synthetic CRM document metadata/reference storage. No binary content.</summary>
public interface IDocumentMetadataFoundationStore
{
 Task<IReadOnlyCollection<DocumentMetadataFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default);
 Task<DocumentMetadataFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
 Task<DocumentMetadataFoundationRecord> SaveAsync(DocumentMetadataFoundationRecord item,CancellationToken cancellationToken=default);
}
