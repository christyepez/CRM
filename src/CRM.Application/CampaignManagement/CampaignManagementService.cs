using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.CampaignManagement;
using CRM.Domain.Enums;

namespace CRM.Application.CampaignManagement;

public sealed class CampaignManagementService(ICampaignFoundationStore store) : ICampaignManagementService
{
    private const string EntityName = "Campaign";
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<CampaignManagementApplicationCampaign>> ListAsync(CancellationToken cancellationToken = default) =>
        (await store.GetPreviewAsync(cancellationToken)).Select(ToCampaign).ToArray();

    public async Task<CampaignManagementApplicationCampaign?> GetAsync(string campaignId, CancellationToken cancellationToken = default)
    {
        var item = await store.GetPreviewByIdAsync(campaignId, cancellationToken);
        return item is null ? null : ToCampaign(item);
    }

    public async Task<CampaignManagementApplicationResult> CreateAsync(CampaignManagementCreateRequest request, CancellationToken cancellationToken = default)
    {
        var evaluation = CampaignManagementPolicy.Evaluate(new(CampaignManagementOperation.Create, null, request.Name, request.StartDate, request.EndDate));
        if (!evaluation.Success) return ToResult(evaluation, null);
        var id = Guid.NewGuid().ToString("D");
        var saved = await store.SavePreviewAsync(ToPreview(id, evaluation), cancellationToken);
        return ToResult(evaluation with { CampaignId = id }, ToCampaign(saved));
    }
    public async Task<CampaignManagementApplicationResult> UpdateAsync(string campaignId, CampaignManagementUpdateRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await store.GetPreviewByIdAsync(campaignId, cancellationToken);
        if (existing is null) return NotFound(CampaignManagementOperation.Update, campaignId, request.Name, request.StartDate, request.EndDate);
        var evaluation = CampaignManagementPolicy.Evaluate(new(CampaignManagementOperation.Update, campaignId, request.Name, request.StartDate, request.EndDate, ToSnapshot(existing)));
        if (!evaluation.Success || !evaluation.Changed) return ToResult(evaluation, evaluation.Success ? ToCampaign(existing) : null);
        var saved = await store.SavePreviewAsync(ToPreview(campaignId, evaluation), cancellationToken);
        return ToResult(evaluation, ToCampaign(saved));
    }

    public Task<CampaignManagementApplicationResult> ActivateAsync(string campaignId, CancellationToken cancellationToken = default) =>
        TransitionAsync(campaignId, CampaignManagementOperation.Activate, cancellationToken);

    public Task<CampaignManagementApplicationResult> CompleteAsync(string campaignId, CancellationToken cancellationToken = default) =>
        TransitionAsync(campaignId, CampaignManagementOperation.Complete, cancellationToken);

    public Task<CampaignManagementApplicationResult> CancelAsync(string campaignId, CancellationToken cancellationToken = default) =>
        TransitionAsync(campaignId, CampaignManagementOperation.Cancel, cancellationToken);

    private async Task<CampaignManagementApplicationResult> TransitionAsync(string campaignId, CampaignManagementOperation operation, CancellationToken cancellationToken)
    {
        var existing = await store.GetPreviewByIdAsync(campaignId, cancellationToken);
        if (existing is null) return NotFound(operation, campaignId, null, default, default);
        var snapshot = ToSnapshot(existing);
        var evaluation = CampaignManagementPolicy.Evaluate(new(operation, campaignId, snapshot.Name, snapshot.StartDate, snapshot.EndDate, snapshot));
        if (!evaluation.Success || !evaluation.Changed) return ToResult(evaluation, evaluation.Success ? ToCampaign(existing) : null);
        var saved = await store.SavePreviewAsync(ToPreview(campaignId, evaluation), cancellationToken);
        return ToResult(evaluation, ToCampaign(saved));
    }
    private static CampaignManagementApplicationResult NotFound(CampaignManagementOperation operation, string id, string? name, DateOnly start, DateOnly end) =>
        ToResult(new(id, operation, false, false, CampaignManagementErrorCode.CampaignNotFound, "Campaign was not found.", name, start, end, CampaignStatus.Draft), null);

    private static CampaignManagementApplicationResult ToResult(CampaignManagementRuleResult evaluation, CampaignManagementApplicationCampaign? campaign) =>
        new(campaign?.Id ?? evaluation.CampaignId, evaluation.Operation, evaluation.Allowed, evaluation.Changed, evaluation.ErrorCode.ToString(), evaluation.Message, campaign);

    private static CrmFoundationPreviewItemContract ToPreview(string id, CampaignManagementRuleResult evaluation) =>
        new(id, EntityName, evaluation.NormalizedName ?? string.Empty, evaluation.ResultStatus.ToString(), DateTimeOffset.UtcNow,
            new Dictionary<string, string>
            {
                ["startDate"] = evaluation.StartDate.ToString("yyyy-MM-dd"),
                ["endDate"] = evaluation.EndDate.ToString("yyyy-MM-dd"),
                ["warning"] = PersistenceMode
            });

    private static CampaignManagementApplicationCampaign ToCampaign(CrmFoundationPreviewItemContract preview) =>
        new(preview.Id, preview.DisplayName, ParseDate(preview.Metadata.GetValueOrDefault("startDate")), ParseDate(preview.Metadata.GetValueOrDefault("endDate")), ParseStatus(preview.Status), PersistenceMode, false);

    private static CampaignManagementSnapshot ToSnapshot(CrmFoundationPreviewItemContract preview) =>
        new(preview.Id, preview.DisplayName, ParseDate(preview.Metadata.GetValueOrDefault("startDate")), ParseDate(preview.Metadata.GetValueOrDefault("endDate")), ParseStatus(preview.Status));

    private static DateOnly ParseDate(string? value) => DateOnly.TryParse(value, out var parsed) ? parsed : default;
    private static CampaignStatus ParseStatus(string? value) => Enum.TryParse<CampaignStatus>(value, true, out var parsed) ? parsed : CampaignStatus.Draft;
}