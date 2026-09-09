using CRM.Domain.Enums;

namespace CRM.Domain.AccountManagement;

public sealed record AccountManagementRuleResult(
    string? AccountId,
    AccountManagementOperation Operation,
    bool Allowed,
    bool Changed,
    AccountManagementErrorCode ErrorCode,
    string Message,
    string? NormalizedName,
    string? NormalizedTaxId,
    string? NormalizedIndustry,
    string? NormalizedSegment,
    AccountStatus ResultStatus)
{
    public bool Success => Allowed && ErrorCode == AccountManagementErrorCode.None;
}
