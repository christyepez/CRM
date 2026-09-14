namespace CRM.Domain.NoteManagement;

public enum NoteManagementOperation
{
    Create = 0,
    Update = 1,
    Archive = 2
}

public enum NoteRelatedEntityType
{
    Customer = 0,
    Contact = 1,
    Lead = 2,
    Opportunity = 3,
    Case = 4
}

public enum NoteStatus
{
    Active = 0,
    Archived = 1
}

public enum NoteManagementErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidNoteId,
    NoteNotFound,
    InvalidRelatedEntityType,    RelatedEntityIdRequired,
    InvalidRelatedEntityId,
    TextRequired,
    TextTooLong,
    InvalidStatus,
    ArchivedNoteCannotBeModified
}
