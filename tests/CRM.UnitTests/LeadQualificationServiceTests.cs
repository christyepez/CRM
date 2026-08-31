using CRM.Application.Foundation;
using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;
using CRM.Domain.LeadQualification;
using Xunit;

namespace CRM.UnitTests;

public sealed class LeadQualificationServiceTests
{
    [Fact]
    public async Task QualifyAsync_WhenLeadExistsAndTransitionChanges_PersistsOnce()
    {
        var store = new CountingLeadFoundationStore(new CrmFoundationPreviewItemContract("lead-001", "Lead", "Ada Preview", "New", DateTimeOffset.UtcNow, new Dictionary<string, string>()));
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Qualify, null, null, "qualified by intake"));

        Assert.True(result.Allowed);
        Assert.True(result.Changed);
        Assert.Equal(LeadStatus.Qualified, result.CurrentStatus);
        Assert.Equal(1, store.SaveCount);
        Assert.Equal("Qualified", store.SavedPreview!.Status);
    }

    [Fact]
    public async Task QualifyAsync_WhenLeadExistsAndDisqualifies_PersistsOnce()
    {
        var store = new CountingLeadFoundationStore(new CrmFoundationPreviewItemContract("lead-001", "Lead", "Ada Preview", "Contacted", DateTimeOffset.UtcNow, new Dictionary<string, string>()));
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Disqualify, LeadDisqualificationReasonCode.Duplicate, null, null));

        Assert.True(result.Allowed);
        Assert.True(result.Changed);
        Assert.Equal(LeadStatus.Disqualified, result.CurrentStatus);
        Assert.Equal(1, store.SaveCount);
        Assert.Equal("Duplicate", store.SavedPreview!.Metadata["disqualificationReasonCode"]);
    }

    [Fact]
    public async Task QualifyAsync_WhenLeadNotFound_ReturnsDeterministicResultAndDoesNotWrite()
    {
        var store = new CountingLeadFoundationStore();
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("missing-lead", LeadQualificationDecision.Qualify, null, null, null));

        Assert.False(result.Allowed);
        Assert.False(result.Changed);
        Assert.Equal(LeadQualificationErrorCode.LeadNotFound, result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task QualifyAsync_WhenTransitionRejected_DoesNotWrite()
    {
        var store = new CountingLeadFoundationStore(new CrmFoundationPreviewItemContract("lead-001", "Lead", "Ada Preview", "Converted", DateTimeOffset.UtcNow, new Dictionary<string, string>()));
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Disqualify, LeadDisqualificationReasonCode.NoInterest, null, null));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.InvalidTransition, result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task QualifyAsync_WhenValidationFails_DoesNotWrite()
    {
        var store = new CountingLeadFoundationStore(new CrmFoundationPreviewItemContract("lead-001", "Lead", "Ada Preview", "New", DateTimeOffset.UtcNow, new Dictionary<string, string>()));
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Disqualify, null, null, null));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.DisqualificationReasonRequired, result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task QualifyAsync_WhenRequestIsIdempotent_DoesNotWrite()
    {
        var store = new CountingLeadFoundationStore(new CrmFoundationPreviewItemContract("lead-001", "Lead", "Ada Preview", "Qualified", DateTimeOffset.UtcNow, new Dictionary<string, string>()));
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Qualify, null, null, null));

        Assert.True(result.Allowed);
        Assert.False(result.Changed);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task QualifyAsync_PropagatesPolicyResultMetadata()
    {
        var store = new CountingLeadFoundationStore(new CrmFoundationPreviewItemContract("lead-001", "Lead", "Ada Preview", "New", DateTimeOffset.UtcNow, new Dictionary<string, string>()));
        var service = new LeadQualificationService(store);

        var result = await service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Qualify, null, null, null));

        Assert.True(result.FoundationMode);
        Assert.Equal("NonProductionSeam", result.PersistenceMode);
        Assert.False(result.ProductiveLeadRouteUnlocked);
        Assert.False(result.PortalAuthRuntimeEnabled);
        Assert.False(result.CommonDbRuntimeEnabled);
    }

    [Fact]
    public async Task QualifyAsync_PropagatesCancellation()
    {
        var store = new CountingLeadFoundationStore();
        var service = new LeadQualificationService(store);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => service.QualifyAsync(new LeadQualificationRequest("lead-001", LeadQualificationDecision.Qualify, null, null, null), cts.Token));
    }

    private sealed class CountingLeadFoundationStore(params CrmFoundationPreviewItemContract[] seeds) : ILeadFoundationStore
    {
        private readonly Dictionary<string, CrmFoundationPreviewItemContract> items = seeds.ToDictionary(item => item.Id, StringComparer.OrdinalIgnoreCase);

        public int SaveCount { get; private set; }

        public CrmFoundationPreviewItemContract? SavedPreview { get; private set; }

        public Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult<IReadOnlyCollection<CrmFoundationPreviewItemContract>>(items.Values.ToArray());
        }

        public Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            items.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }

        public Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SaveCount++;
            SavedPreview = preview;
            items[preview.Id] = preview;
            return Task.FromResult(preview);
        }

        public Task ClearPreviewAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            items.Clear();
            return Task.CompletedTask;
        }

        public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(new CrmFoundationStoreStatusContract("CountingLeadFoundationStore", true, true, false, items.Count, "NonProductionSeam", "NonProduction", "Test double only."));
        }
    }
}

