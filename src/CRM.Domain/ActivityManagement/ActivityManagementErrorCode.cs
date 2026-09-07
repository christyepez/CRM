namespace CRM.Domain.ActivityManagement;

public enum ActivityManagementErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidActivityId,
    InvalidActivityType,
    InvalidStatus,
    SubjectRequired,
    SubjectTooLong,
    ScheduledAtRequired,
    ActivityTargetRequired,
    MultipleActivityTargetsNotAllowed,
    InvalidLeadId,
    InvalidContactId,
    ActivityNotFound,
    CompletedActivityCannotBeModified,
    CancelledActivityCannotBeModified,
    CancelledActivityCannotBeCompleted,
    CompletedActivityCannotBeCancelled,
    ValidationFailed
}
