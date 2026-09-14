using CRM.Application.Ports.Persistence;
using CRM.Domain.InteractionManagement;

namespace CRM.Application.InteractionManagement;

public sealed class InteractionManagementService(IInteractionFoundationStore store) : IInteractionManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<InteractionManagementApplicationInteraction>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await store.GetAllAsync(cancellationToken)).Select(ToApplication).ToArray();

    public async Task<InteractionManagementApplicationInteraction?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var item = await store.GetByIdAsync(id, cancellationToken);
        return item is null ? null : ToApplication(item);
    }

    public async Task<InteractionManagementApplicationResult> CreateAsync(InteractionManagementCreateRequest request, CancellationToken cancellationToken = default)
    {
        var evaluation = InteractionManagementPolicy.Evaluate(new(
            InteractionManagementOperation.Create,
            null,
            request.RelatedEntityType,
            request.RelatedEntityId,
            request.Channel,
            request.Direction,
            request.Subject,
            request.Summary,
            request.OccurredAtUtc ?? default,
            DateTimeOffset.UtcNow));

        if (!evaluation.Success || !evaluation.Changed)
            return ToResult(evaluation, null);

        var saved = await store.SaveAsync(new(
            Guid.NewGuid().ToString("D"),
            evaluation.RelatedEntityType,
            evaluation.NormalizedRelatedEntityId!,
            evaluation.Channel,
            evaluation.Direction,
            evaluation.NormalizedSubject!,
            evaluation.NormalizedSummary!,
            evaluation.OccurredAtUtc,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation with { InteractionId = saved.Id }, ToApplication(saved));
    }

    public async Task<InteractionManagementApplicationResult> UpdateAsync(string id, InteractionManagementUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound(InteractionManagementOperation.Update, id);

        var evaluation = InteractionManagementPolicy.Evaluate(new(
            InteractionManagementOperation.Update,
            id,
            request.RelatedEntityType,
            request.RelatedEntityId,
            request.Channel,
            request.Direction,
            request.Subject,
            request.Summary,
            request.OccurredAtUtc ?? default,
            DateTimeOffset.UtcNow,
            Snapshot(existing)));

        if (!evaluation.Success)
            return ToResult(evaluation, null);
        if (!evaluation.Changed)
            return ToResult(evaluation, ToApplication(existing));

        var saved = await store.SaveAsync(new(
            id,
            evaluation.RelatedEntityType,
            evaluation.NormalizedRelatedEntityId!,
            evaluation.Channel,
            evaluation.Direction,
            evaluation.NormalizedSubject!,
            evaluation.NormalizedSummary!,
            evaluation.OccurredAtUtc,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation, ToApplication(saved));
    }

    public async Task<InteractionManagementApplicationResult> VoidAsync(string id, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound(InteractionManagementOperation.Void, id);

        var evaluation = InteractionManagementPolicy.Evaluate(new(
            InteractionManagementOperation.Void,
            id,
            existing.RelatedEntityType,
            existing.RelatedEntityId,
            existing.Channel,
            existing.Direction,
            existing.Subject,
            existing.Summary,
            existing.OccurredAtUtc,
            DateTimeOffset.UtcNow,
            Snapshot(existing)));

        if (!evaluation.Success)
            return ToResult(evaluation, null);
        if (!evaluation.Changed)
            return ToResult(evaluation, ToApplication(existing));

        var saved = await store.SaveAsync(new(
            id,
            evaluation.RelatedEntityType,
            evaluation.NormalizedRelatedEntityId!,
            evaluation.Channel,
            evaluation.Direction,
            evaluation.NormalizedSubject!,
            evaluation.NormalizedSummary!,
            evaluation.OccurredAtUtc,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation, ToApplication(saved));
    }

    private static InteractionManagementSnapshot Snapshot(InteractionFoundationRecord item) =>
        new(item.Id, item.RelatedEntityType, item.RelatedEntityId, item.Channel, item.Direction, item.Subject, item.Summary, item.OccurredAtUtc, item.Status);

    private static InteractionManagementApplicationInteraction ToApplication(InteractionFoundationRecord item) =>
        new(item.Id, item.RelatedEntityType, item.RelatedEntityId, item.Channel, item.Direction, item.Subject, item.Summary, item.OccurredAtUtc, item.Status, PersistenceMode, false);

    private static InteractionManagementApplicationResult ToResult(InteractionManagementRuleResult evaluation, InteractionManagementApplicationInteraction? item) =>
        new(item?.Id ?? evaluation.InteractionId, evaluation.Operation.ToString(), evaluation.Allowed, evaluation.Changed, evaluation.ErrorCode.ToString(), evaluation.Message, item?.Status ?? evaluation.ResultStatus, item);

    private static InteractionManagementApplicationResult NotFound(InteractionManagementOperation operation, string id) =>
        new(id, operation.ToString(), false, false, InteractionManagementErrorCode.InteractionNotFound.ToString(), "Interaction was not found.", null, null);
}
