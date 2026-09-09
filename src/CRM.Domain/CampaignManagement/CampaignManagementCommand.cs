using CRM.Domain.Enums;

namespace CRM.Domain.CampaignManagement;

public sealed record CampaignManagementCommand(
    CampaignManagementOperation Operation,
    string? CampaignId,
    string? Name,
    DateOnly StartDate,
    DateOnly EndDate,
    CampaignManagementSnapshot? ExistingCampaign = null);

public sealed record CampaignManagementSnapshot(
    string CampaignId,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    CampaignStatus Status);
