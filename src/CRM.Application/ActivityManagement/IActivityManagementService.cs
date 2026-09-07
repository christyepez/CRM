namespace CRM.Application.ActivityManagement;

public interface IActivityManagementService
{
    Task<IReadOnlyCollection<ActivityManagementApplicationActivity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ActivityManagementApplicationActivity?> GetByIdAsync(string activityId, CancellationToken cancellationToken = default);

    Task<ActivityManagementApplicationResult> CreateAsync(ActivityManagementCreateApplicationRequest request, CancellationToken cancellationToken = default);

    Task<ActivityManagementApplicationResult> UpdateAsync(string activityId, ActivityManagementUpdateApplicationRequest request, CancellationToken cancellationToken = default);

    Task<ActivityManagementApplicationResult> CompleteAsync(string activityId, CancellationToken cancellationToken = default);

    Task<ActivityManagementApplicationResult> CancelAsync(string activityId, CancellationToken cancellationToken = default);
}
