namespace CRM.Domain.AccountManagement;

public enum AccountManagementErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidAccountId,
    AccountNotFound,
    NameRequired,
    NameTooLong,
    TaxIdTooLong,
    IndustryTooLong,
    SegmentTooLong,
    InvalidStatusTransition
}
