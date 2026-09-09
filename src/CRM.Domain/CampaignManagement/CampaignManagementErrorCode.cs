namespace CRM.Domain.CampaignManagement;

public enum CampaignManagementErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidCampaignId,
    CampaignNotFound,
    NameRequired,
    NameTooLong,
    StartDateRequired,
    EndDateRequired,
    InvalidDateRange,
    DraftCampaignRequired,
    ActiveCampaignRequired,
    CompletedCampaignCannotBeModified,
    CancelledCampaignCannotBeModified,
    TerminalCampaignCannotBeModified,
    ValidationFailed
}
