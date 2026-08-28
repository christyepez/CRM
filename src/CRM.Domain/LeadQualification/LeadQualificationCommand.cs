namespace CRM.Domain.LeadQualification;

public sealed record LeadQualificationCommand(
    string LeadId,
    LeadQualificationDecision Decision,
    LeadDisqualificationReasonCode? ReasonCode = null,
    string? OtherReasonExplanation = null,
    string? Comment = null);

