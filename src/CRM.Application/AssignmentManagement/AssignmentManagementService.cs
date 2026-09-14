using CRM.Application.Ports.Persistence;
using CRM.Domain.AssignmentManagement;

namespace CRM.Application.AssignmentManagement;

public sealed class AssignmentManagementService(IAssignmentFoundationStore store) : IAssignmentManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<AssignmentApplicationItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await store.GetAllAsync(cancellationToken)).Select(Map).ToArray();

    public async Task<AssignmentApplicationItem?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var item = await store.GetByIdAsync(id, cancellationToken);
        return item is null ? null : Map(item);
    }

    public async Task<AssignmentApplicationResult> CreateAsync(AssignmentCreateRequest request, CancellationToken cancellationToken = default)
    {
        var evaluation = AssignmentPolicy.Evaluate(new(
            AssignmentOperation.Create, null, request.RelatedEntityType, request.RelatedEntityId,
            request.AssigneeReferenceId, request.AssignmentLabel));
        if (!evaluation.Success) return Result(evaluation, null);
        var saved = await store.SaveAsync(ToRecord(Guid.NewGuid().ToString("D"), evaluation), cancellationToken);
        return Result(evaluation with { AssignmentId = saved.Id }, Map(saved));
    }

    public async Task<AssignmentApplicationResult> UpdateAsync(string id, AssignmentUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null) return NotFound(AssignmentOperation.Update, id);
        var evaluation = AssignmentPolicy.Evaluate(new(
            AssignmentOperation.Update, id, request.RelatedEntityType, request.RelatedEntityId,
            request.AssigneeReferenceId, request.AssignmentLabel, Snapshot(existing)));
        if (!evaluation.Success) return Result(evaluation, null);
        if (!evaluation.Changed) return Result(evaluation, Map(existing));
        var saved = await store.SaveAsync(ToRecord(id, evaluation), cancellationToken);
        return Result(evaluation, Map(saved));
    }

    public async Task<AssignmentApplicationResult> ArchiveAsync(string id, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null) return NotFound(AssignmentOperation.Archive, id);
        var evaluation = AssignmentPolicy.Evaluate(new(
            AssignmentOperation.Archive, id, existing.RelatedEntityType, existing.RelatedEntityId,
            existing.AssigneeReferenceId, existing.AssignmentLabel, Snapshot(existing)));
        if (!evaluation.Success) return Result(evaluation, null);
        if (!evaluation.Changed) return Result(evaluation, Map(existing));
        var saved = await store.SaveAsync(existing with { Status = AssignmentStatus.Archived }, cancellationToken);
        return Result(evaluation, Map(saved));
    }

    private static AssignmentSnapshot Snapshot(AssignmentFoundationRecord x) =>
        new(x.Id, x.RelatedEntityType, x.RelatedEntityId, x.AssigneeReferenceId, x.AssignmentLabel, x.Status);

    private static AssignmentFoundationRecord ToRecord(string id, AssignmentRuleResult e) =>
        new(id, e.RelatedEntityType, e.NormalizedRelatedEntityId!, e.NormalizedAssigneeReferenceId!,
            e.NormalizedAssignmentLabel, e.ResultStatus);

    private static AssignmentApplicationItem Map(AssignmentFoundationRecord x) =>
        new(x.Id, x.RelatedEntityType, x.RelatedEntityId, x.AssigneeReferenceId, x.AssignmentLabel,
            x.Status, PersistenceMode, ProductiveCrudEnabled: false, PortalIdentityRuntimeEnabled: false);

    private static AssignmentApplicationResult Result(AssignmentRuleResult e, AssignmentApplicationItem? x) =>
        new(x?.Id ?? e.AssignmentId, e.Operation.ToString(), e.Allowed, e.Changed,
            e.ErrorCode.ToString(), e.Message, x?.Status ?? e.ResultStatus, x);

    private static AssignmentApplicationResult NotFound(AssignmentOperation op, string id) =>
        new(id, op.ToString(), false, false, AssignmentErrorCode.AssignmentNotFound.ToString(),
            "Assignment was not found.", null, null);
}
