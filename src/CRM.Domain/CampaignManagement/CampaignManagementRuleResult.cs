using CRM.Domain.Enums;

namespace CRM.Domain.CampaignManagement;

public sealed record CampaignManagementRuleResult(
    string? CampaignId,
    CampaignManagementOperation Operation,
    bool Allowed,
    bool Changed,
    CampaignManagementErrorCode ErrorCode,
    string Message,
    string? NormalizedName,
    DateOnly StartDate,
    DateOnly EndDate,
    CampaignStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == CampaignManagementErrorCode.None;
}
