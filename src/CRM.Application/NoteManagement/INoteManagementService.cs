namespace CRM.Application.NoteManagement;

public interface INoteManagementService
{
    Task<IReadOnlyCollection<NoteManagementApplicationNote>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NoteManagementApplicationNote?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<NoteManagementApplicationResult> CreateAsync(NoteManagementCreateRequest request, CancellationToken cancellationToken = default);
    Task<NoteManagementApplicationResult> UpdateAsync(string id, NoteManagementUpdateRequest request, CancellationToken cancellationToken = default);
    Task<NoteManagementApplicationResult> ArchiveAsync(string id, CancellationToken cancellationToken = default);
}
