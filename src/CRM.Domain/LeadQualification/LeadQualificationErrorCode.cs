namespace CRM.Domain.LeadQualification;

public enum LeadQualificationErrorCode
{
    None = 0,
    LeadIdRequired = 1,
    LeadNotFound = 2,
    InvalidQualificationDecision = 3,
    InvalidTransition = 4,
    DisqualificationReasonRequired = 5,
    DisqualificationReasonNotAllowed = 6,
    OtherReasonExplanationRequired = 7,
    OtherReasonExplanationTooLong = 8,
    CommentTooLong = 9
}

