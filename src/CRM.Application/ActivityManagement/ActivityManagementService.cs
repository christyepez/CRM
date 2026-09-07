using CRM.Application.Ports.Persistence;
using CRM.Domain.ActivityManagement;
using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;

namespace CRM.Application.ActivityManagement;

public sealed class ActivityManagementService(
    IActivityFoundationStore activities,
    ILeadFoundationStore leads,
    IContactFoundationStore contacts) : IActivityManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<ActivityManagementApplicationActivity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var all = await activities.GetAllAsync(cancellationToken);
        return all.Select(ToApplicationActivity).ToArray();
    }

    public async Task<ActivityManagementApplicationActivity?> GetByIdAsync(string activityId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var activity = await activities.GetByIdAsync(activityId, cancellationToken);
        return activity is null ? null : ToApplicationActivity(activity);
    }

    public async Task<ActivityManagementApplicationResult> CreateAsync(ActivityManagementCreateApplicationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var command = new ActivityManagementCommand(
            ActivityManagementOperation.Create,
            ActivityId: null,
            request.Type,
            request.Subject,
            request.ScheduledAtUtc,
            request.LeadId,
            request.ContactId);

        var evaluation = ActivityManagementPolicy.Evaluate(command);
        if (!evaluation.Success)
        {
            return ToApplicationResult(evaluation, activity: null);
        }

        var targetResult = await ValidateTargetExistsAsync(evaluation, cancellationToken);
        if (targetResult is not null)
        {
            return targetResult;
        }

        var activity = CreateActivity(evaluation);
        var saved = await activities.SaveAsync(activity, cancellationToken);

        return ToApplicationResult(evaluation with { ActivityId = saved.Id.ToString() }, ToApplicationActivity(saved));
    }

    public async Task<ActivityManagementApplicationResult> UpdateAsync(string activityId, ActivityManagementUpdateApplicationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existing = await activities.GetByIdAsync(activityId, cancellationToken);
        if (existing is null)
        {
            return NotFound(ActivityManagementOperation.Update, activityId);
        }

        var command = new ActivityManagementCommand(
            ActivityManagementOperation.Update,
            activityId,
            request.Type,
            request.Subject,
            request.ScheduledAtUtc,
            request.LeadId,
            request.ContactId,
            ToSnapshot(existing));

        var evaluation = ActivityManagementPolicy.Evaluate(command);
        if (!evaluation.Success)
        {
            return ToApplicationResult(evaluation, activity: null);
        }

        var targetResult = await ValidateTargetExistsAsync(evaluation, cancellationToken);
        if (targetResult is not null)
        {
            return targetResult;
        }

        if (!evaluation.Changed)
        {
            return ToApplicationResult(evaluation, ToApplicationActivity(existing));
        }

        existing.UpdateSchedule(
            evaluation.Type,
            evaluation.NormalizedSubject!,
            evaluation.ScheduledAtUtc,
            ToCrmId(evaluation.NormalizedLeadId),
            ToCrmId(evaluation.NormalizedContactId));

        var saved = await activities.SaveAsync(existing, cancellationToken);

        return ToApplicationResult(evaluation, ToApplicationActivity(saved));
    }

    public async Task<ActivityManagementApplicationResult> CompleteAsync(string activityId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existing = await activities.GetByIdAsync(activityId, cancellationToken);
        if (existing is null)
        {
            return NotFound(ActivityManagementOperation.Complete, activityId);
        }

        var command = new ActivityManagementCommand(
            ActivityManagementOperation.Complete,
            activityId,
            existing.Type,
            existing.Subject,
            existing.ScheduledAtUtc,
            existing.LeadId?.ToString(),
            existing.ContactId?.ToString(),
            ToSnapshot(existing));

        var evaluation = ActivityManagementPolicy.Evaluate(command);
        if (!evaluation.Success)
        {
            return ToApplicationResult(evaluation, activity: null);
        }

        if (!evaluation.Changed)
        {
            return ToApplicationResult(evaluation, ToApplicationActivity(existing));
        }

        existing.Complete(DateTimeOffset.UtcNow);
        var saved = await activities.SaveAsync(existing, cancellationToken);

        return ToApplicationResult(evaluation with { CompletedAtUtc = saved.CompletedAtUtc }, ToApplicationActivity(saved));
    }

    public async Task<ActivityManagementApplicationResult> CancelAsync(string activityId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existing = await activities.GetByIdAsync(activityId, cancellationToken);
        if (existing is null)
        {
            return NotFound(ActivityManagementOperation.Cancel, activityId);
        }

        var command = new ActivityManagementCommand(
            ActivityManagementOperation.Cancel,
            activityId,
            existing.Type,
            existing.Subject,
            existing.ScheduledAtUtc,
            existing.LeadId?.ToString(),
            existing.ContactId?.ToString(),
            ToSnapshot(existing));

        var evaluation = ActivityManagementPolicy.Evaluate(command);
        if (!evaluation.Success)
        {
            return ToApplicationResult(evaluation, activity: null);
        }

        if (!evaluation.Changed)
        {
            return ToApplicationResult(evaluation, ToApplicationActivity(existing));
        }

        existing.Cancel();
        var saved = await activities.SaveAsync(existing, cancellationToken);

        return ToApplicationResult(evaluation, ToApplicationActivity(saved));
    }

    private async Task<ActivityManagementApplicationResult?> ValidateTargetExistsAsync(ActivityManagementRuleResult evaluation, CancellationToken cancellationToken)
    {
        if (evaluation.NormalizedLeadId is not null)
        {
            var lead = await leads.GetPreviewByIdAsync(evaluation.NormalizedLeadId, cancellationToken);
            return lead is null
                ? TargetNotFound(evaluation, "Lead target was not found.")
                : null;
        }

        if (evaluation.NormalizedContactId is not null)
        {
            var contact = await contacts.GetPreviewByIdAsync(evaluation.NormalizedContactId, cancellationToken);
            return contact is null
                ? TargetNotFound(evaluation, "Contact target was not found.")
                : null;
        }

        return null;
    }

    private static ActivityManagementApplicationResult TargetNotFound(ActivityManagementRuleResult evaluation, string message) =>
        new(
            evaluation.ActivityId,
            evaluation.Operation,
            Allowed: false,
            Changed: false,
            ActivityManagementErrorCode.ActivityNotFound.ToString(),
            message,
            evaluation.ResultStatus,
            Activity: null);

    private static ActivityManagementApplicationResult NotFound(ActivityManagementOperation operation, string activityId) =>
        new(
            activityId,
            operation,
            Allowed: false,
            Changed: false,
            ActivityManagementErrorCode.ActivityNotFound.ToString(),
            "Activity was not found.",
            Status: null,
            Activity: null);

    private static Activity CreateActivity(ActivityManagementRuleResult evaluation)
    {
        var id = CrmId.New();
        var leadId = ToCrmId(evaluation.NormalizedLeadId);
        var contactId = ToCrmId(evaluation.NormalizedContactId);

        return leadId is not null
            ? Activity.ScheduleForLead(id, leadId.Value, evaluation.Type, evaluation.NormalizedSubject!, evaluation.ScheduledAtUtc)
            : Activity.ScheduleForContact(id, contactId!.Value, evaluation.Type, evaluation.NormalizedSubject!, evaluation.ScheduledAtUtc);
    }

    private static ActivityManagementSnapshot ToSnapshot(Activity activity) =>
        new(
            activity.Id.ToString(),
            activity.Type,
            activity.Subject,
            activity.ScheduledAtUtc,
            activity.LeadId?.ToString(),
            activity.ContactId?.ToString(),
            activity.Status,
            activity.CompletedAtUtc);

    private static ActivityManagementApplicationResult ToApplicationResult(ActivityManagementRuleResult evaluation, ActivityManagementApplicationActivity? activity) =>
        new(
            activity?.Id ?? evaluation.ActivityId,
            evaluation.Operation,
            evaluation.Allowed,
            evaluation.Changed,
            evaluation.ErrorCode.ToString(),
            evaluation.Message,
            activity?.Status ?? evaluation.ResultStatus,
            activity);

    private static ActivityManagementApplicationActivity ToApplicationActivity(Activity activity) =>
        new(
            activity.Id.ToString(),
            activity.Type,
            activity.Subject,
            activity.ScheduledAtUtc,
            activity.LeadId?.ToString(),
            activity.ContactId?.ToString(),
            activity.Status,
            activity.CompletedAtUtc,
            PersistenceMode,
            ProductiveCrudEnabled: false);

    private static CrmId? ToCrmId(string? id) =>
        Guid.TryParse(id, out var parsed) && parsed != Guid.Empty ? CrmId.From(parsed) : null;
}
