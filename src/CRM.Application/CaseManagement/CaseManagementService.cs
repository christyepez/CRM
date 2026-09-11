using CRM.Application.Ports.Persistence;
using CRM.Domain.CaseManagement;

namespace CRM.Application.CaseManagement;

public sealed class CaseManagementService(ICaseFoundationStore store) : ICaseManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<CaseManagementApplicationCase>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await store.GetAllAsync(cancellationToken)).Select(ToApplication).ToArray();

    public async Task<CaseManagementApplicationCase?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var item = await store.GetByIdAsync(id, cancellationToken);
        return item is null ? null : ToApplication(item);
    }

    public async Task<CaseManagementApplicationResult> CreateAsync(CaseManagementCreateRequest request, CancellationToken cancellationToken = default)
    {
        var evaluation = CaseManagementPolicy.Evaluate(new(
            CaseManagementOperation.Create,
            null,
            request.CustomerId,
            request.Title,
            request.Summary,
            request.Priority));

        if (!evaluation.Success)
            return ToResult(evaluation, null);

        var saved = await store.SaveAsync(new(
            Guid.NewGuid().ToString("D"),
            evaluation.NormalizedCustomerId!,
            evaluation.NormalizedTitle!,
            evaluation.NormalizedSummary!,
            evaluation.ResultPriority,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation with { CaseId = saved.Id }, ToApplication(saved));
    }

    public async Task<CaseManagementApplicationResult> UpdateAsync(string id, CaseManagementUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound(CaseManagementOperation.Update, id);

        var evaluation = CaseManagementPolicy.Evaluate(new(
            CaseManagementOperation.Update,
            id,
            request.CustomerId,
            request.Title,
            request.Summary,
            request.Priority,
            Snapshot(existing)));

        if (!evaluation.Success)
            return ToResult(evaluation, null);
        if (!evaluation.Changed)
            return ToResult(evaluation, ToApplication(existing));

        var saved = await store.SaveAsync(new(
            id,
            evaluation.NormalizedCustomerId!,
            evaluation.NormalizedTitle!,
            evaluation.NormalizedSummary!,
            evaluation.ResultPriority,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation, ToApplication(saved));
    }

    public Task<CaseManagementApplicationResult> StartAsync(string id, CancellationToken cancellationToken = default) =>
        TransitionAsync(id, CaseManagementOperation.Start, cancellationToken);

    public Task<CaseManagementApplicationResult> ResolveAsync(string id, CancellationToken cancellationToken = default) =>
        TransitionAsync(id, CaseManagementOperation.Resolve, cancellationToken);

    public Task<CaseManagementApplicationResult> CloseAsync(string id, CancellationToken cancellationToken = default) =>
        TransitionAsync(id, CaseManagementOperation.Close, cancellationToken);

    private async Task<CaseManagementApplicationResult> TransitionAsync(string id, CaseManagementOperation operation, CancellationToken cancellationToken)
    {
        var existing = await store.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound(operation, id);

        var evaluation = CaseManagementPolicy.Evaluate(new(
            operation,
            id,
            existing.CustomerId,
            existing.Title,
            existing.Summary,
            existing.Priority,
            Snapshot(existing)));

        if (!evaluation.Success)
            return ToResult(evaluation, null);
        if (!evaluation.Changed)
            return ToResult(evaluation, ToApplication(existing));

        var saved = await store.SaveAsync(new(
            id,
            evaluation.NormalizedCustomerId!,
            evaluation.NormalizedTitle!,
            evaluation.NormalizedSummary!,
            evaluation.ResultPriority,
            evaluation.ResultStatus), cancellationToken);

        return ToResult(evaluation, ToApplication(saved));
    }

    private static CaseManagementSnapshot Snapshot(CaseFoundationRecord item) =>
        new(item.Id, item.CustomerId, item.Title, item.Summary, item.Priority, item.Status);

    private static CaseManagementApplicationCase ToApplication(CaseFoundationRecord item) =>
        new(item.Id, item.CustomerId, item.Title, item.Summary, item.Priority, item.Status, PersistenceMode, false);

    private static CaseManagementApplicationResult ToResult(CaseManagementRuleResult evaluation, CaseManagementApplicationCase? item) =>
        new(item?.Id ?? evaluation.CaseId, evaluation.Operation.ToString(), evaluation.Allowed, evaluation.Changed, evaluation.ErrorCode.ToString(), evaluation.Message, item?.Status ?? evaluation.ResultStatus, item);

    private static CaseManagementApplicationResult NotFound(CaseManagementOperation operation, string id) =>
        new(id, operation.ToString(), false, false, CaseManagementErrorCode.CaseNotFound.ToString(), "Case was not found.", null, null);
}
