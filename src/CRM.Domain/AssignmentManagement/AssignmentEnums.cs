namespace CRM.Domain.AssignmentManagement;

public enum AssignmentOperation { Create = 0, Update = 1, Archive = 2 }
public enum AssignmentStatus { Active = 0, Archived = 1 }
public enum AssignmentRelatedEntityType { Customer = 0, Contact = 1, Lead = 2, Opportunity = 3, Case = 4 }
public enum AssignmentErrorCode
{
    None = 0, InvalidOperation, InvalidAssignmentId, AssignmentNotFound,
    InvalidRelatedEntityType, RelatedEntityIdRequired, InvalidRelatedEntityId,
    AssigneeReferenceIdRequired, AssigneeReferenceIdTooLong,
    AssignmentLabelTooLong, ArchivedAssignmentCannotBeModified, InvalidStatus
}
