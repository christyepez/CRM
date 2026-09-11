namespace CRM.Application.InteractionManagement;

public interface IInteractionManagementService
{
    Task<IReadOnlyCollection<InteractionManagementApplicationInteraction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InteractionManagementApplicationInteraction?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<InteractionManagementApplicationResult> CreateAsync(InteractionManagementCreateRequest request, CancellationToken cancellationToken = default);
    Task<InteractionManagementApplicationResult> UpdateAsync(string id, InteractionManagementUpdateRequest request, CancellationToken cancellationToken = default);
    Task<InteractionManagementApplicationResult> VoidAsync(string id, CancellationToken cancellationToken = default);
}
