namespace CRM.Application.DocumentMetadata;
public interface IDocumentMetadataService
{
 Task<IReadOnlyCollection<DocumentMetadataApplicationDocument>> GetAllAsync(CancellationToken cancellationToken=default);
 Task<DocumentMetadataApplicationDocument?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
 Task<DocumentMetadataApplicationResult> CreateAsync(DocumentMetadataCreateRequest request,CancellationToken cancellationToken=default);
 Task<DocumentMetadataApplicationResult> UpdateAsync(string id,DocumentMetadataUpdateRequest request,CancellationToken cancellationToken=default);
 Task<DocumentMetadataApplicationResult> ArchiveAsync(string id,CancellationToken cancellationToken=default);
}
