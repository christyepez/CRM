namespace CRM.Application.OpportunityManagement;

public interface IOpportunityManagementService
{
    Task<IReadOnlyCollection<OpportunityManagementApplicationOpportunity>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationOpportunity?> GetByIdAsync(string opportunityId,CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationResult> CreateAsync(OpportunityManagementCreateRequest request,CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationResult> UpdateAsync(string opportunityId,OpportunityManagementUpdateRequest request,CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationResult> ProgressAsync(string opportunityId,OpportunityManagementProgressRequest request,CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationResult> WinAsync(string opportunityId,CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationResult> LoseAsync(string opportunityId,CancellationToken cancellationToken=default);
    Task<OpportunityManagementApplicationResult> CancelAsync(string opportunityId,CancellationToken cancellationToken=default);
}
