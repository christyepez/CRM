namespace CRM.Application.AssignmentManagement;

public interface IAssignmentManagementService
{
    Task<IReadOnlyCollection<AssignmentApplicationItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AssignmentApplicationItem?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<AssignmentApplicationResult> CreateAsync(AssignmentCreateRequest request, CancellationToken cancellationToken = default);
    Task<AssignmentApplicationResult> UpdateAsync(string id, AssignmentUpdateRequest request, CancellationToken cancellationToken = default);
    Task<AssignmentApplicationResult> ArchiveAsync(string id, CancellationToken cancellationToken = default);
}
