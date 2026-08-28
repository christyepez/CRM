using CRM.Domain.Enums;

namespace CRM.Domain.LeadQualification;

public sealed record LeadQualificationRuleResult(
    string LeadId,
    LeadStatus PreviousStatus,
    LeadStatus CurrentStatus,
    LeadQualificationDecision Decision,
    LeadDisqualificationReasonCode? ReasonCode,
    bool Allowed,
    bool Changed,
    LeadQualificationErrorCode ErrorCode,
    string Message)
{
    public static LeadQualificationRuleResult Success(
        string leadId,
        LeadStatus previousStatus,
        LeadStatus currentStatus,
        LeadQualificationDecision decision,
        LeadDisqualificationReasonCode? reasonCode,
        bool changed) =>
        new(leadId, previousStatus, currentStatus, decision, reasonCode, true, changed, LeadQualificationErrorCode.None, changed ? "Lead qualification state changed." : "Lead qualification request is idempotent.");

    public static LeadQualificationRuleResult Rejected(
        string leadId,
        LeadStatus currentStatus,
        LeadQualificationDecision decision,
        LeadDisqualificationReasonCode? reasonCode,
        LeadQualificationErrorCode errorCode,
        string message) =>
        new(leadId, currentStatus, currentStatus, decision, reasonCode, false, false, errorCode, message);
}

