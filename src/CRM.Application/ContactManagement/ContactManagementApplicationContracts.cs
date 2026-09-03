using CRM.Domain.ContactManagement;
using CRM.Domain.Enums;

namespace CRM.Application.ContactManagement;

public sealed record ContactManagementCreateApplicationRequest(
    string? Name,
    string? Email,
    string? Phone,
    string? Role,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod);

public sealed record ContactManagementUpdateApplicationRequest(
    string? Name,
    string? Email,
    string? Phone,
    string? Role,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod);

public sealed record ContactManagementApplicationContact(
    string Id,
    string Name,
    string? Email,
    string? Phone,
    string? Role,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod,
    string Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record ContactManagementApplicationResult(
    string? ContactId,
    ContactManagementOperation Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    ContactManagementApplicationContact? Contact)
{
    public bool Success => Allowed && string.Equals(ErrorCode, ContactManagementErrorCode.None.ToString(), StringComparison.Ordinal);
}
