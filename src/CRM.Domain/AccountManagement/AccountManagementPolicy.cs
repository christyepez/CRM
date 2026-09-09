using CRM.Domain.Enums;

namespace CRM.Domain.AccountManagement;

public static class AccountManagementPolicy
{
    public const int MaxNameLength = 160;
    public const int MaxTaxIdLength = 64;
    public const int MaxIndustryLength = 120;
    public const int MaxSegmentLength = 80;

    public static AccountManagementRuleResult Evaluate(AccountManagementCommand command)
    {
        var id = Normalize(command.AccountId);
        var name = Normalize(command.Name);
        var taxId = Normalize(command.TaxId)?.ToUpperInvariant();
        var industry = Normalize(command.Industry);
        var segment = Normalize(command.Segment);

        if (!Enum.IsDefined(command.Operation))
            return Reject(command, AccountManagementErrorCode.InvalidOperation, "Account operation is invalid.", id, name, taxId, industry, segment);

        if (command.Operation != AccountManagementOperation.Create)
        {
            if (!ValidId(id))
                return Reject(command, AccountManagementErrorCode.InvalidAccountId, "Account id is required.", id, name, taxId, industry, segment);
            if (command.ExistingAccount is null)
                return Reject(command, AccountManagementErrorCode.AccountNotFound, "Existing account snapshot is required.", id, name, taxId, industry, segment);
            if (!string.Equals(id, Normalize(command.ExistingAccount.AccountId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, AccountManagementErrorCode.InvalidAccountId, "Account id cannot change.", id, name, taxId, industry, segment, command.ExistingAccount.Status);
        }

        if (command.Operation is AccountManagementOperation.Create or AccountManagementOperation.Update)
            return EvaluateProfile(command, id, name, taxId, industry, segment);

        return command.Operation switch
        {
            AccountManagementOperation.Activate => EvaluateActivate(command, id!),
            AccountManagementOperation.Deactivate => EvaluateDeactivate(command, id!),
            _ => Reject(command, AccountManagementErrorCode.InvalidOperation, "Account operation is invalid.", id, name, taxId, industry, segment)
        };
    }

    private static AccountManagementRuleResult EvaluateProfile(AccountManagementCommand command, string? id, string? name, string? taxId, string? industry, string? segment)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Reject(command, AccountManagementErrorCode.NameRequired, "Account name is required.", id, name, taxId, industry, segment);
        if (name.Length > MaxNameLength)
            return Reject(command, AccountManagementErrorCode.NameTooLong, "Account name exceeds the allowed length.", id, name, taxId, industry, segment);
        if ((taxId?.Length ?? 0) > MaxTaxIdLength)
            return Reject(command, AccountManagementErrorCode.TaxIdTooLong, "Tax id exceeds the allowed length.", id, name, taxId, industry, segment);
        if ((industry?.Length ?? 0) > MaxIndustryLength)
            return Reject(command, AccountManagementErrorCode.IndustryTooLong, "Industry exceeds the allowed length.", id, name, taxId, industry, segment);
        if ((segment?.Length ?? 0) > MaxSegmentLength)
            return Reject(command, AccountManagementErrorCode.SegmentTooLong, "Segment exceeds the allowed length.", id, name, taxId, industry, segment);

        var status = command.Operation == AccountManagementOperation.Create ? AccountStatus.Draft : command.ExistingAccount!.Status;
        var changed = command.Operation == AccountManagementOperation.Create || HasChanged(command.ExistingAccount!, name, taxId, industry, segment);
        return Result(command, id, name, taxId, industry, segment, status, true, changed, AccountManagementErrorCode.None,
            changed ? "Account management operation is valid." : "Account update has no changes.");
    }

    private static AccountManagementRuleResult EvaluateActivate(AccountManagementCommand command, string id)
    {
        var existing = command.ExistingAccount!;
        if (existing.Status == AccountStatus.Active)
            return FromExisting(command, existing, id, AccountStatus.Active, true, false, AccountManagementErrorCode.None, "Account is already active.");
        if (existing.Status is AccountStatus.Draft or AccountStatus.Inactive)
            return FromExisting(command, existing, id, AccountStatus.Active, true, true, AccountManagementErrorCode.None, "Account activation is valid.");
        return FromExisting(command, existing, id, existing.Status, false, false, AccountManagementErrorCode.InvalidStatusTransition, "Account cannot be activated from its current status.");
    }

    private static AccountManagementRuleResult EvaluateDeactivate(AccountManagementCommand command, string id)
    {
        var existing = command.ExistingAccount!;
        if (existing.Status == AccountStatus.Inactive)
            return FromExisting(command, existing, id, AccountStatus.Inactive, true, false, AccountManagementErrorCode.None, "Account is already inactive.");
        if (existing.Status == AccountStatus.Active)
            return FromExisting(command, existing, id, AccountStatus.Inactive, true, true, AccountManagementErrorCode.None, "Account deactivation is valid.");
        return FromExisting(command, existing, id, existing.Status, false, false, AccountManagementErrorCode.InvalidStatusTransition, "Only active accounts can be deactivated.");
    }

    private static bool HasChanged(AccountManagementSnapshot existing, string name, string? taxId, string? industry, string? segment) =>
        !string.Equals(name, Normalize(existing.Name), StringComparison.Ordinal)
        || !string.Equals(taxId, Normalize(existing.TaxId)?.ToUpperInvariant(), StringComparison.Ordinal)
        || !string.Equals(industry, Normalize(existing.Industry), StringComparison.Ordinal)
        || !string.Equals(segment, Normalize(existing.Segment), StringComparison.Ordinal);

    private static AccountManagementRuleResult FromExisting(AccountManagementCommand command, AccountManagementSnapshot existing, string id, AccountStatus status, bool allowed, bool changed, AccountManagementErrorCode code, string message) =>
        new(id, command.Operation, allowed, changed, code, message, Normalize(existing.Name), Normalize(existing.TaxId)?.ToUpperInvariant(), Normalize(existing.Industry), Normalize(existing.Segment), status);

    private static AccountManagementRuleResult Reject(AccountManagementCommand command, AccountManagementErrorCode code, string message, string? id, string? name, string? taxId, string? industry, string? segment, AccountStatus status = AccountStatus.Draft) =>
        Result(command, id, name, taxId, industry, segment, status, false, false, code, message);

    private static AccountManagementRuleResult Result(AccountManagementCommand command, string? id, string? name, string? taxId, string? industry, string? segment, AccountStatus status, bool allowed, bool changed, AccountManagementErrorCode code, string message) =>
        new(id, command.Operation, allowed, changed, code, message, name, taxId, industry, segment, status);

    private static string? Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : normalized;
    }

    private static bool ValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
