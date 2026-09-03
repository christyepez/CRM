using CRM.Domain.Enums;

namespace CRM.Domain.ContactManagement;

public sealed record ContactManagementCommand(
    ContactManagementOperation Operation,
    string? ContactId,
    string? Name,
    string? Email,
    string? Phone,
    string? Role,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod,
    ContactManagementSnapshot? ExistingContact = null);

public sealed record ContactManagementSnapshot(
    string ContactId,
    string Name,
    string? Email,
    string? Phone,
    string? Role,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod,
    ContactStatus Status);
