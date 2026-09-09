using CRM.Application.CampaignManagement;
using CRM.Domain.CampaignManagement;
using CRM.Domain.Enums;

namespace CRM.Api.Foundation;

public sealed record FoundationCampaignCreateRequest(string? Name, DateOnly StartDate, DateOnly EndDate);
public sealed record FoundationCampaignUpdateRequest(string? Name, DateOnly StartDate, DateOnly EndDate);

public sealed record CampaignManagementApiResponse(
    string? Id,
    CampaignManagementOperation Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    CampaignStatus? Status,
    CampaignManagementApplicationCampaign? Campaign,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    string Warning)
{
    public static CampaignManagementApiResponse From(CampaignManagementApplicationResult result) =>
        new(result.CampaignId, result.Operation, result.Allowed, result.Changed, result.ErrorCode, result.Message,
            result.Campaign?.Status, result.Campaign, true, result.Campaign?.PersistenceMode ?? "NonProductionSeam",
            false, false, false, false, "Foundation Campaign API only; productive route remains locked");

    public static int ToStatusCode(CampaignManagementApplicationResult result) => result.ErrorCode switch
    {
        nameof(CampaignManagementErrorCode.None) => StatusCodes.Status200OK,
        nameof(CampaignManagementErrorCode.CampaignNotFound) => StatusCodes.Status404NotFound,
        nameof(CampaignManagementErrorCode.DraftCampaignRequired) or
        nameof(CampaignManagementErrorCode.ActiveCampaignRequired) or
        nameof(CampaignManagementErrorCode.CompletedCampaignCannotBeModified) or
        nameof(CampaignManagementErrorCode.CancelledCampaignCannotBeModified) or
        nameof(CampaignManagementErrorCode.TerminalCampaignCannotBeModified) => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };
}