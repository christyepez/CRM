using CRM.Domain.Enums;

namespace CRM.Domain.ContactManagement;

public sealed record ContactManagementRuleResult(
    string? ContactId,
    ContactManagementOperation Operation,
    bool Allowed,
    bool Changed,
    ContactManagementErrorCode ErrorCode,
    string Message,
    string? NormalizedName,
    string? NormalizedEmail,
    string? NormalizedPhone,
    string? NormalizedRole,
    string? NormalizedAccountId,
    PreferredContactMethod PreferredContactMethod)
{
    public bool Success => Allowed && ErrorCode == ContactManagementErrorCode.None;

    public static ContactManagementRuleResult Rejected(
        ContactManagementCommand command,
        ContactManagementErrorCode errorCode,
        string message,
        string? normalizedName = null,
        string? normalizedEmail = null,
        string? normalizedPhone = null,
        string? normalizedRole = null,
        string? normalizedAccountId = null) =>
        new(
            command.ContactId?.Trim(),
            command.Operation,
            Allowed: false,
            Changed: false,
            errorCode,
            message,
            normalizedName,
            normalizedEmail,
            normalizedPhone,
            normalizedRole,
            normalizedAccountId,
            command.PreferredContactMethod);
}
