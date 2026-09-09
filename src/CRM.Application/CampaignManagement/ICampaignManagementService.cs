namespace CRM.Application.CampaignManagement;

public interface ICampaignManagementService
{
    Task<IReadOnlyCollection<CampaignManagementApplicationCampaign>> ListAsync(CancellationToken cancellationToken = default);
    Task<CampaignManagementApplicationCampaign?> GetAsync(string campaignId, CancellationToken cancellationToken = default);
    Task<CampaignManagementApplicationResult> CreateAsync(CampaignManagementCreateRequest request, CancellationToken cancellationToken = default);
    Task<CampaignManagementApplicationResult> UpdateAsync(string campaignId, CampaignManagementUpdateRequest request, CancellationToken cancellationToken = default);
    Task<CampaignManagementApplicationResult> ActivateAsync(string campaignId, CancellationToken cancellationToken = default);
    Task<CampaignManagementApplicationResult> CompleteAsync(string campaignId, CancellationToken cancellationToken = default);
    Task<CampaignManagementApplicationResult> CancelAsync(string campaignId, CancellationToken cancellationToken = default);
}