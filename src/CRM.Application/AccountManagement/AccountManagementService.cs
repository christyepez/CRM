using CRM.Application.Ports.Persistence;
using CRM.Domain.AccountManagement;

namespace CRM.Application.AccountManagement;

public sealed class AccountManagementService(IAccountFoundationStore store) : IAccountManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<AccountManagementApplicationAccount>> GetAllAsync(CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        return (await store.GetAllAsync(ct)).Select(ToApplication).ToArray();
    }

    public async Task<AccountManagementApplicationAccount?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        var record = await store.GetByIdAsync(id, ct);
        return record is null ? null : ToApplication(record);
    }

    public async Task<AccountManagementApplicationResult> CreateAsync(AccountManagementCreateRequest request, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        var evaluation = AccountManagementPolicy.Evaluate(new(AccountManagementOperation.Create, null, request.Name, request.TaxId, request.Industry, request.Segment));
        if (!evaluation.Success) return Result(evaluation, null);
        var saved = await store.SaveAsync(FromEvaluation(Guid.NewGuid().ToString(), evaluation), ct);
        return Result(evaluation with { AccountId = saved.Id }, ToApplication(saved));
    }
    public async Task<AccountManagementApplicationResult> UpdateAsync(string id, AccountManagementUpdateRequest request, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();
        var existing = await store.GetByIdAsync(id, ct);
        if (existing is null) return NotFound(AccountManagementOperation.Update, id);
        var evaluation = AccountManagementPolicy.Evaluate(new(AccountManagementOperation.Update, id, request.Name, request.TaxId, request.Industry, request.Segment, Snapshot(existing)));
        if (!evaluation.Success) return Result(evaluation, null);
        if (!evaluation.Changed) return Result(evaluation, ToApplication(existing));
        var saved = await store.SaveAsync(FromEvaluation(id, evaluation), ct);
        return Result(evaluation, ToApplication(saved));
    }

    public Task<AccountManagementApplicationResult> ActivateAsync(string id, CancellationToken ct = default) => ChangeStatusAsync(id, AccountManagementOperation.Activate, ct);
    public Task<AccountManagementApplicationResult> DeactivateAsync(string id, CancellationToken ct = default) => ChangeStatusAsync(id, AccountManagementOperation.Deactivate, ct);

    private async Task<AccountManagementApplicationResult> ChangeStatusAsync(string id, AccountManagementOperation operation, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var existing = await store.GetByIdAsync(id, ct);
        if (existing is null) return NotFound(operation, id);
        var evaluation = AccountManagementPolicy.Evaluate(new(operation, id, existing.Name, existing.TaxId, existing.Industry, existing.Segment, Snapshot(existing)));
        if (!evaluation.Success) return Result(evaluation, null);
        if (!evaluation.Changed) return Result(evaluation, ToApplication(existing));
        var saved = await store.SaveAsync(FromEvaluation(id, evaluation), ct);
        return Result(evaluation, ToApplication(saved));
    }
    private static AccountManagementSnapshot Snapshot(AccountFoundationRecord x) => new(x.Id, x.Name, x.TaxId, x.Industry, x.Segment, x.Status);

    private static AccountFoundationRecord FromEvaluation(string id, AccountManagementRuleResult e) =>
        new(id, e.NormalizedName!, e.NormalizedTaxId, e.NormalizedIndustry, e.NormalizedSegment, e.ResultStatus);

    private static AccountManagementApplicationAccount ToApplication(AccountFoundationRecord x) =>
        new(x.Id, x.Name, x.TaxId, x.Industry, x.Segment, x.Status, PersistenceMode, false);

    private static AccountManagementApplicationResult Result(AccountManagementRuleResult e, AccountManagementApplicationAccount? account) =>
        new(account?.Id ?? e.AccountId, e.Operation.ToString(), e.Allowed, e.Changed, e.ErrorCode.ToString(), e.Message, account?.Status ?? e.ResultStatus, account);

    private static AccountManagementApplicationResult NotFound(AccountManagementOperation operation, string id) =>
        new(id, operation.ToString(), false, false, AccountManagementErrorCode.AccountNotFound.ToString(), "Account was not found.", null, null);
}
