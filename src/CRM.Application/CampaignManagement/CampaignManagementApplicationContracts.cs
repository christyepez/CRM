using CRM.Domain.CampaignManagement;
using CRM.Domain.Enums;

namespace CRM.Application.CampaignManagement;

public sealed record CampaignManagementCreateRequest(string? Name, DateOnly StartDate, DateOnly EndDate);
public sealed record CampaignManagementUpdateRequest(string? Name, DateOnly StartDate, DateOnly EndDate);
public sealed record CampaignManagementApplicationCampaign(
    string Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    CampaignStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record CampaignManagementApplicationResult(
    string? CampaignId,
    CampaignManagementOperation Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    CampaignManagementApplicationCampaign? Campaign)
{
    public bool Success => Allowed && string.Equals(ErrorCode, CampaignManagementErrorCode.None.ToString(), StringComparison.Ordinal);
}