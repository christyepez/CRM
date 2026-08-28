using CRM.Domain.Enums;
using CRM.Domain.LeadQualification;

namespace CRM.Application.Foundation;

public sealed record LeadQualificationRequest(
    string? LeadId,
    LeadQualificationDecision? Decision,
    LeadDisqualificationReasonCode? ReasonCode,
    string? OtherReasonExplanation,
    string? Comment);

public sealed record LeadQualificationResult(
    string LeadId,
    LeadStatus PreviousStatus,
    LeadStatus CurrentStatus,
    LeadQualificationDecision Decision,
    LeadDisqualificationReasonCode? ReasonCode,
    bool Allowed,
    bool Changed,
    LeadQualificationErrorCode ErrorCode,
    string Message,
    bool FoundationMode,
    string PersistenceMode,
    bool ProductiveLeadRouteUnlocked,
    bool PortalAuthRuntimeEnabled,
    bool CommonDbRuntimeEnabled);

public interface ILeadQualificationService
{
    Task<LeadQualificationResult> QualifyAsync(LeadQualificationRequest request, CancellationToken cancellationToken = default);
}

