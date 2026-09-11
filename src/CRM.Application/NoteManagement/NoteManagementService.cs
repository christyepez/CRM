using CRM.Application.Ports.Persistence;
using CRM.Domain.NoteManagement;

namespace CRM.Application.NoteManagement;

public sealed class NoteManagementService(INoteFoundationStore store) : INoteManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<NoteManagementApplicationNote>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await store.GetAllAsync(cancellationToken)).Select(ToApplication).ToArray();

    public async Task<NoteManagementApplicationNote?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var item = await store.GetByIdAsync(id, cancellationToken);
        return item is null ? null : ToApplication(item);
    }

    public async Task<NoteManagementApplicationResult> CreateAsync(NoteManagementCreateRequest request, CancellationToken cancellationToken = default)
    {
        var evaluation = NoteManagementPolicy.Evaluate(new(
            NoteManagementOperation.Create,
            null,
            request.RelatedEntityType,
            request.RelatedEntityId,
            request.Text));

        if (!evaluation.Success || !evaluation.Changed)
            return ToResult(evaluation, null);
        var saved = await store.SaveAsync(new(
            Guid.NewGuid().ToString("D"),
            evaluation.RelatedEntityType,
            evaluation.NormalizedRelatedEntityId!,
            evaluation.NormalizedText!,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation with { NoteId = saved.Id }, ToApplication(saved));
    }

    public async Task<NoteManagementApplicationResult> UpdateAsync(string id, NoteManagementUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound(NoteManagementOperation.Update, id);

        var evaluation = NoteManagementPolicy.Evaluate(new(
            NoteManagementOperation.Update,
            id,
            request.RelatedEntityType,
            request.RelatedEntityId,
            request.Text,
            Snapshot(existing)));

        if (!evaluation.Success)
            return ToResult(evaluation, null);
        if (!evaluation.Changed)
            return ToResult(evaluation, ToApplication(existing));
        var saved = await store.SaveAsync(new(
            id,
            evaluation.RelatedEntityType,
            evaluation.NormalizedRelatedEntityId!,
            evaluation.NormalizedText!,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation, ToApplication(saved));
    }

    public async Task<NoteManagementApplicationResult> ArchiveAsync(string id, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound(NoteManagementOperation.Archive, id);

        var evaluation = NoteManagementPolicy.Evaluate(new(
            NoteManagementOperation.Archive,
            id,
            existing.RelatedEntityType,
            existing.RelatedEntityId,
            existing.Text,
            Snapshot(existing)));

        if (!evaluation.Success)
            return ToResult(evaluation, null);
        if (!evaluation.Changed)
            return ToResult(evaluation, ToApplication(existing));
        var saved = await store.SaveAsync(new(
            id,
            evaluation.RelatedEntityType,
            evaluation.NormalizedRelatedEntityId!,
            evaluation.NormalizedText!,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation, ToApplication(saved));
    }

    private static NoteManagementSnapshot Snapshot(NoteFoundationRecord item) =>
        new(item.Id, item.RelatedEntityType, item.RelatedEntityId, item.Text, item.Status);

    private static NoteManagementApplicationNote ToApplication(NoteFoundationRecord item) =>
        new(item.Id, item.RelatedEntityType, item.RelatedEntityId, item.Text, item.Status, PersistenceMode, false);

    private static NoteManagementApplicationResult ToResult(NoteManagementRuleResult evaluation, NoteManagementApplicationNote? item) =>
        new(item?.Id ?? evaluation.NoteId, evaluation.Operation.ToString(), evaluation.Allowed, evaluation.Changed,
            evaluation.ErrorCode.ToString(), evaluation.Message, item?.Status ?? evaluation.ResultStatus, item);

    private static NoteManagementApplicationResult NotFound(NoteManagementOperation operation, string id) =>
        new(id, operation.ToString(), false, false, NoteManagementErrorCode.NoteNotFound.ToString(), "Note was not found.", null, null);
}
