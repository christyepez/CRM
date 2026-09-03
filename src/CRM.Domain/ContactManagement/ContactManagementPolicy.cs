using CRM.Domain.Enums;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.ContactManagement;

public static class ContactManagementPolicy
{
    public const int MaxNameLength = 160;
    public const int MaxEmailLength = 254;
    public const int MaxPhoneLength = 24;
    public const int MaxRoleLength = 80;

    public static ContactManagementRuleResult Evaluate(ContactManagementCommand command)
    {
        var normalizedContactId = Normalize(command.ContactId);
        var normalizedName = Normalize(command.Name);
        var normalizedEmail = NormalizeEmail(command.Email);
        var normalizedPhone = Normalize(command.Phone);
        var normalizedRole = Normalize(command.Role);
        var normalizedAccountId = Normalize(command.AccountId);

        if (!Enum.IsDefined(command.Operation))
        {
            return Reject(command, ContactManagementErrorCode.ValidationFailed, "Contact operation is invalid.");
        }

        if (!Enum.IsDefined(command.PreferredContactMethod))
        {
            return Reject(command, ContactManagementErrorCode.InvalidPreferredContactMethod, "Preferred contact method is invalid.");
        }

        if (command.Operation == ContactManagementOperation.Update)
        {
            if (!IsValidId(normalizedContactId))
            {
                return Reject(command, ContactManagementErrorCode.InvalidContactId, "Contact id is required for update.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }

            if (command.ExistingContact is null)
            {
                return Reject(command, ContactManagementErrorCode.ContactNotFound, "Existing contact snapshot is required for update.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }

            if (!string.Equals(normalizedContactId, Normalize(command.ExistingContact.ContactId), StringComparison.OrdinalIgnoreCase))
            {
                return Reject(command, ContactManagementErrorCode.InvalidContactId, "Contact id cannot change during update.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }
        }

        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            return Reject(command, ContactManagementErrorCode.NameRequired, "Contact name is required.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
        }

        if (normalizedName.Length > MaxNameLength)
        {
            return Reject(command, ContactManagementErrorCode.NameTooLong, "Contact name exceeds the allowed length.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
        }

        if (normalizedEmail is not null)
        {
            if (normalizedEmail.Length > MaxEmailLength)
            {
                return Reject(command, ContactManagementErrorCode.EmailTooLong, "Contact email exceeds the allowed length.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }

            if (!IsValidEmail(normalizedEmail))
            {
                return Reject(command, ContactManagementErrorCode.InvalidEmail, "Contact email is invalid.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }
        }

        if (normalizedPhone is not null)
        {
            if (normalizedPhone.Length > MaxPhoneLength)
            {
                return Reject(command, ContactManagementErrorCode.PhoneTooLong, "Contact phone exceeds the allowed length.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }

            if (!IsValidPhone(normalizedPhone))
            {
                return Reject(command, ContactManagementErrorCode.InvalidPhone, "Contact phone is invalid.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
            }
        }

        if (normalizedRole?.Length > MaxRoleLength)
        {
            return Reject(command, ContactManagementErrorCode.RoleTooLong, "Contact role exceeds the allowed length.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
        }

        if (normalizedAccountId is not null && !IsValidId(normalizedAccountId))
        {
            return Reject(command, ContactManagementErrorCode.InvalidAccountReferenceFormat, "Account id format is invalid.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
        }

        if (command.PreferredContactMethod == PreferredContactMethod.Email && normalizedEmail is null)
        {
            return Reject(command, ContactManagementErrorCode.PreferredContactMethodRequiresEmail, "Preferred contact method Email requires an email.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
        }

        if (command.PreferredContactMethod == PreferredContactMethod.Phone && normalizedPhone is null)
        {
            return Reject(command, ContactManagementErrorCode.PreferredContactMethodRequiresPhone, "Preferred contact method Phone requires a phone.", normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);
        }

        var changed = command.Operation == ContactManagementOperation.Create || HasChanged(command, normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);

        return new ContactManagementRuleResult(
            normalizedContactId,
            command.Operation,
            Allowed: true,
            changed,
            ContactManagementErrorCode.None,
            changed ? "Contact management operation is valid." : "Contact update has no changes.",
            normalizedName,
            normalizedEmail,
            normalizedPhone,
            normalizedRole,
            normalizedAccountId,
            command.PreferredContactMethod);
    }

    private static bool HasChanged(ContactManagementCommand command, string normalizedName, string? normalizedEmail, string? normalizedPhone, string? normalizedRole, string? normalizedAccountId)
    {
        var existing = command.ExistingContact;
        if (existing is null)
        {
            return true;
        }

        return !string.Equals(normalizedName, Normalize(existing.Name), StringComparison.Ordinal)
            || !string.Equals(normalizedEmail, NormalizeEmail(existing.Email), StringComparison.Ordinal)
            || !string.Equals(normalizedPhone, Normalize(existing.Phone), StringComparison.Ordinal)
            || !string.Equals(normalizedRole, Normalize(existing.Role), StringComparison.Ordinal)
            || !string.Equals(normalizedAccountId, Normalize(existing.AccountId), StringComparison.OrdinalIgnoreCase)
            || command.PreferredContactMethod != existing.PreferredContactMethod;
    }

    private static ContactManagementRuleResult Reject(
        ContactManagementCommand command,
        ContactManagementErrorCode errorCode,
        string message,
        string? normalizedName = null,
        string? normalizedEmail = null,
        string? normalizedPhone = null,
        string? normalizedRole = null,
        string? normalizedAccountId = null) =>
        ContactManagementRuleResult.Rejected(command, errorCode, message, normalizedName, normalizedEmail, normalizedPhone, normalizedRole, normalizedAccountId);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static string? NormalizeEmail(string? value)
    {
        var normalized = Normalize(value);
        return normalized?.ToLowerInvariant();
    }

    private static bool IsValidEmail(string value)
    {
        try
        {
            _ = EmailAddress.From(value);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private static bool IsValidPhone(string value)
    {
        try
        {
            _ = PhoneNumber.From(value);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    private static bool IsValidId(string? value) =>
        Guid.TryParse(value, out var parsed) && parsed != Guid.Empty;
}
