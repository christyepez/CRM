using CRM.Domain.Enums;

namespace CRM.Domain.CaseManagement;

public sealed record CaseManagementRuleResult(
    string? CaseId,
    CaseManagementOperation Operation,
    bool Allowed,
    bool Changed,
    CaseManagementErrorCode ErrorCode,
    string Message,
    string? NormalizedCustomerId,
    string? NormalizedTitle,
    string? NormalizedSummary,
    CasePriority ResultPriority,
    CaseStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == CaseManagementErrorCode.None;
}
