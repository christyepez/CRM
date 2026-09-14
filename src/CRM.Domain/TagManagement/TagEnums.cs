namespace CRM.Domain.TagManagement;
public enum TagOperation { Create=0, Update=1, Archive=2 }
public enum TagStatus { Active=0, Archived=1 }
public enum TagRelatedEntityType { Customer=0, Contact=1, Lead=2, Opportunity=3, Case=4 }
public enum TagErrorCode { None=0, InvalidOperation, InvalidTagId, TagNotFound, NameRequired, NameTooLong, DescriptionTooLong, InvalidRelatedEntityType, RelatedEntityIdRequired, InvalidRelatedEntityId, ArchivedTagCannotBeModified, InvalidStatus }
