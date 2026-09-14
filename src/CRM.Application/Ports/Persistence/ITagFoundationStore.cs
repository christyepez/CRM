using CRM.Domain.TagManagement;
namespace CRM.Application.Ports.Persistence;
public sealed record TagFoundationRecord(string Id,string Name,string? Description,TagStatus Status,TagRelatedEntityType? RelatedEntityType,string? RelatedEntityId);
public interface ITagFoundationStore
{
 Task<IReadOnlyCollection<TagFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default);
 Task<TagFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
 Task<TagFoundationRecord> SaveAsync(TagFoundationRecord item,CancellationToken cancellationToken=default);
}
