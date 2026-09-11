namespace CRM.Application.CaseManagement;

public interface ICaseManagementService
{
    Task<IReadOnlyCollection<CaseManagementApplicationCase>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CaseManagementApplicationCase?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<CaseManagementApplicationResult> CreateAsync(CaseManagementCreateRequest request, CancellationToken cancellationToken = default);
    Task<CaseManagementApplicationResult> UpdateAsync(string id, CaseManagementUpdateRequest request, CancellationToken cancellationToken = default);
    Task<CaseManagementApplicationResult> StartAsync(string id, CancellationToken cancellationToken = default);
    Task<CaseManagementApplicationResult> ResolveAsync(string id, CancellationToken cancellationToken = default);
    Task<CaseManagementApplicationResult> CloseAsync(string id, CancellationToken cancellationToken = default);
}
