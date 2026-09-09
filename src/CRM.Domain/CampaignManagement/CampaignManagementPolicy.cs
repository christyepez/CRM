using CRM.Domain.Enums;

namespace CRM.Domain.CampaignManagement;

public static class CampaignManagementPolicy
{
    public const int MaxNameLength = 160;

    public static CampaignManagementRuleResult Evaluate(CampaignManagementCommand command)
    {
        var id = Normalize(command.CampaignId);
        var name = Normalize(command.Name);

        if (!Enum.IsDefined(command.Operation))
            return Reject(command, CampaignManagementErrorCode.InvalidOperation, "Campaign operation is invalid.", id, name);

        if (command.Operation != CampaignManagementOperation.Create)
        {
            if (!ValidId(id))
                return Reject(command, CampaignManagementErrorCode.InvalidCampaignId, "Campaign id is required.", id, name);
            if (command.ExistingCampaign is null)
                return Reject(command, CampaignManagementErrorCode.CampaignNotFound, "Existing campaign snapshot is required.", id, name);
            if (!string.Equals(id, Normalize(command.ExistingCampaign.CampaignId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, CampaignManagementErrorCode.InvalidCampaignId, "Campaign id cannot change.", id, name, command.ExistingCampaign.Status);
        }

        return command.Operation switch
        {
            CampaignManagementOperation.Activate => EvaluateActivate(command, id!),
            CampaignManagementOperation.Complete => EvaluateComplete(command, id!),
            CampaignManagementOperation.Cancel => EvaluateCancel(command, id!),
            _ => EvaluateCreateOrUpdate(command, id, name)
        };
    }

    private static CampaignManagementRuleResult EvaluateCreateOrUpdate(CampaignManagementCommand command, string? id, string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Reject(command, CampaignManagementErrorCode.NameRequired, "Campaign name is required.", id, name);
        if (name.Length > MaxNameLength)
            return Reject(command, CampaignManagementErrorCode.NameTooLong, "Campaign name exceeds the allowed length.", id, name);
        if (command.StartDate == default)
            return Reject(command, CampaignManagementErrorCode.StartDateRequired, "Campaign start date is required.", id, name);
        if (command.EndDate == default)
            return Reject(command, CampaignManagementErrorCode.EndDateRequired, "Campaign end date is required.", id, name);
        if (command.EndDate < command.StartDate)
            return Reject(command, CampaignManagementErrorCode.InvalidDateRange, "Campaign end date cannot be before start date.", id, name);

        if (command.Operation == CampaignManagementOperation.Update && command.ExistingCampaign!.Status != CampaignStatus.Draft)
        {
            var code = command.ExistingCampaign.Status switch
            {
                CampaignStatus.Completed => CampaignManagementErrorCode.CompletedCampaignCannotBeModified,
                CampaignStatus.Cancelled => CampaignManagementErrorCode.CancelledCampaignCannotBeModified,
                _ => CampaignManagementErrorCode.DraftCampaignRequired
            };
            return Reject(command, code, "Only draft campaigns can be edited.", id, name, command.ExistingCampaign.Status);
        }

        var changed = command.Operation == CampaignManagementOperation.Create || HasChanged(command, name!);
        return Result(command, id, name, CampaignStatus.Draft, true, changed, CampaignManagementErrorCode.None,
            changed ? "Campaign management operation is valid." : "Campaign update has no changes.");
    }

    private static CampaignManagementRuleResult EvaluateActivate(CampaignManagementCommand command, string id)
    {
        var existing = command.ExistingCampaign!;
        if (existing.Status == CampaignStatus.Active)
            return FromExisting(command, existing, id, CampaignStatus.Active, true, false, CampaignManagementErrorCode.None, "Campaign is already active.");
        if (existing.Status == CampaignStatus.Completed)
            return FromExisting(command, existing, id, CampaignStatus.Completed, false, false, CampaignManagementErrorCode.CompletedCampaignCannotBeModified, "Completed campaigns cannot be activated.");
        if (existing.Status == CampaignStatus.Cancelled)
            return FromExisting(command, existing, id, CampaignStatus.Cancelled, false, false, CampaignManagementErrorCode.CancelledCampaignCannotBeModified, "Cancelled campaigns cannot be activated.");
        if (existing.Status != CampaignStatus.Draft)
            return FromExisting(command, existing, id, existing.Status, false, false, CampaignManagementErrorCode.DraftCampaignRequired, "Only draft campaigns can be activated.");
        return FromExisting(command, existing, id, CampaignStatus.Active, true, true, CampaignManagementErrorCode.None, "Campaign activation is valid.");
    }

    private static CampaignManagementRuleResult EvaluateComplete(CampaignManagementCommand command, string id)
    {
        var existing = command.ExistingCampaign!;
        if (existing.Status == CampaignStatus.Completed)
            return FromExisting(command, existing, id, CampaignStatus.Completed, true, false, CampaignManagementErrorCode.None, "Campaign is already completed.");
        if (existing.Status == CampaignStatus.Cancelled)
            return FromExisting(command, existing, id, CampaignStatus.Cancelled, false, false, CampaignManagementErrorCode.CancelledCampaignCannotBeModified, "Cancelled campaigns cannot be completed.");
        if (existing.Status != CampaignStatus.Active)
            return FromExisting(command, existing, id, existing.Status, false, false, CampaignManagementErrorCode.ActiveCampaignRequired, "Only active campaigns can be completed.");
        return FromExisting(command, existing, id, CampaignStatus.Completed, true, true, CampaignManagementErrorCode.None, "Campaign completion is valid.");
    }

    private static CampaignManagementRuleResult EvaluateCancel(CampaignManagementCommand command, string id)
    {
        var existing = command.ExistingCampaign!;
        if (existing.Status == CampaignStatus.Cancelled)
            return FromExisting(command, existing, id, CampaignStatus.Cancelled, true, false, CampaignManagementErrorCode.None, "Campaign is already cancelled.");
        if (existing.Status == CampaignStatus.Completed)
            return FromExisting(command, existing, id, CampaignStatus.Completed, false, false, CampaignManagementErrorCode.CompletedCampaignCannotBeModified, "Completed campaigns cannot be cancelled.");
        if (existing.Status is CampaignStatus.Draft or CampaignStatus.Active)
            return FromExisting(command, existing, id, CampaignStatus.Cancelled, true, true, CampaignManagementErrorCode.None, "Campaign cancellation is valid.");
        return FromExisting(command, existing, id, existing.Status, false, false, CampaignManagementErrorCode.TerminalCampaignCannotBeModified, "Campaign cannot be cancelled from its current status.");
    }

    private static bool HasChanged(CampaignManagementCommand command, string name)
    {
        var existing = command.ExistingCampaign!;
        return !string.Equals(name, Normalize(existing.Name), StringComparison.Ordinal)
            || command.StartDate != existing.StartDate
            || command.EndDate != existing.EndDate;
    }

    private static CampaignManagementRuleResult FromExisting(CampaignManagementCommand command, CampaignManagementSnapshot existing, string id, CampaignStatus status, bool allowed, bool changed, CampaignManagementErrorCode code, string message) =>
        new(id, command.Operation, allowed, changed, code, message, Normalize(existing.Name), existing.StartDate, existing.EndDate, status);

    private static CampaignManagementRuleResult Reject(CampaignManagementCommand command, CampaignManagementErrorCode code, string message, string? id, string? name, CampaignStatus status = CampaignStatus.Draft) =>
        Result(command, id, name, status, false, false, code, message);

    private static CampaignManagementRuleResult Result(CampaignManagementCommand command, string? id, string? name, CampaignStatus status, bool allowed, bool changed, CampaignManagementErrorCode code, string message) =>
        new(id, command.Operation, allowed, changed, code, message, name, command.StartDate, command.EndDate, status);

    private static string? Normalize(string? value) { var normalized = (value ?? string.Empty).Trim(); return normalized.Length == 0 ? null : normalized; }
    private static bool ValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
