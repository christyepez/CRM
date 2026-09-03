namespace CRM.Domain.ContactManagement;

public enum ContactManagementErrorCode
{
    None = 0,
    InvalidContactId,
    NameRequired,
    NameTooLong,
    InvalidEmail,
    EmailTooLong,
    InvalidPhone,
    PhoneTooLong,
    RoleTooLong,
    InvalidPreferredContactMethod,
    PreferredContactMethodRequiresEmail,
    PreferredContactMethodRequiresPhone,
    InvalidAccountReferenceFormat,
    ContactNotFound,
    ValidationFailed
}
