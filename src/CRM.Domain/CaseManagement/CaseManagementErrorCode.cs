namespace CRM.Domain.CaseManagement;

public enum CaseManagementErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidCaseId,
    CaseNotFound,
    CustomerIdRequired,
    InvalidCustomerId,
    TitleRequired,
    TitleTooLong,
    SummaryRequired,
    SummaryTooLong,
    InvalidPriority,
    InvalidStatusTransition,
    ResolvedCaseCannotBeModified,
    ClosedCaseCannotBeModified
}
