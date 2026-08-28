namespace CRM.Domain.LeadQualification;

public enum LeadQualificationErrorCode
{
    None = 0,
    LeadIdRequired = 1,
    InvalidQualificationDecision = 2,
    InvalidTransition = 3,
    DisqualificationReasonRequired = 4,
    DisqualificationReasonNotAllowed = 5,
    OtherReasonExplanationRequired = 6,
    OtherReasonExplanationTooLong = 7,
    CommentTooLong = 8
}

