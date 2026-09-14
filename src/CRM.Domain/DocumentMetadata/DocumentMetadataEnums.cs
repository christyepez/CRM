namespace CRM.Domain.DocumentMetadata;

public enum DocumentMetadataOperation
{
    Create = 0,
    Update = 1,
    Archive = 2
}

public enum DocumentMetadataStatus
{
    Active = 0,
    Archived = 1
}

public enum DocumentRelatedEntityType
{
    Customer = 0,
    Contact = 1,
    Lead = 2,
    Opportunity = 3,
    Case = 4
}

public enum DocumentMetadataErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidDocumentId,
    DocumentNotFound,
    InvalidRelatedEntityType,
    RelatedEntityIdRequired,
    InvalidRelatedEntityId,
    FileReferenceIdRequired,
    FileReferenceIdTooLong,
    FileNameRequired,
    FileNameTooLong,
    ContentTypeTooLong,
    DescriptionTooLong,
    ArchivedDocumentCannotBeModified,
    InvalidStatus
}
