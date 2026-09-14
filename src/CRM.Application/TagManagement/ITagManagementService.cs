namespace CRM.Application.TagManagement;
public interface ITagManagementService
{
 Task<IReadOnlyCollection<TagApplicationItem>> GetAllAsync(CancellationToken cancellationToken=default);
 Task<TagApplicationItem?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
 Task<TagApplicationResult> CreateAsync(TagCreateRequest request,CancellationToken cancellationToken=default);
 Task<TagApplicationResult> UpdateAsync(string id,TagUpdateRequest request,CancellationToken cancellationToken=default);
 Task<TagApplicationResult> ArchiveAsync(string id,CancellationToken cancellationToken=default);
}
