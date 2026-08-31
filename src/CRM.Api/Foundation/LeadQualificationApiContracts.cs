using CRM.Application.Foundation;
using CRM.Domain.LeadQualification;

namespace CRM.Api.Foundation;

public sealed record LeadQualificationApiRequest(
    LeadQualificationDecision? Decision,
    LeadDisqualificationReasonCode? DisqualificationReason,
    string? OtherReason,
    string? Comment)
{
    public LeadQualificationRequest ToApplicationRequest(string leadId) =>
        new(leadId, Decision, DisqualificationReason, OtherReason, Comment);
}

public sealed record LeadQualificationApiResponse(
    string LeadId,
    string PreviousStatus,
    string CurrentStatus,
    string Decision,
    string? DisqualificationReason,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    bool FoundationMode,
    string PersistenceMode,
    bool ProductiveLeadQualificationRouteEnabled,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled)
{
    public static LeadQualificationApiResponse From(LeadQualificationResult result) =>
        new(
            result.LeadId,
            result.PreviousStatus.ToString(),
            result.CurrentStatus.ToString(),
            result.Decision.ToString(),
            result.ReasonCode?.ToString(),
            result.Allowed,
            result.Changed,
            result.ErrorCode.ToString(),
            result.Message,
            result.FoundationMode,
            result.PersistenceMode,
            false,
            result.PortalAuthRuntimeEnabled,
            result.CommonDbRuntimeEnabled);

    public static int ToStatusCode(LeadQualificationResult result) =>
        result.ErrorCode switch
        {
            LeadQualificationErrorCode.None => StatusCodes.Status200OK,
            LeadQualificationErrorCode.LeadNotFound => StatusCodes.Status404NotFound,
            LeadQualificationErrorCode.InvalidTransition => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };
}

