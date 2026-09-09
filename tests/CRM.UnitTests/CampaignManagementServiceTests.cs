using CRM.Application.CampaignManagement;
using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using Xunit;

namespace CRM.UnitTests;

public sealed class CampaignManagementServiceTests
{
    private static readonly DateOnly Start = new(2026, 9, 1);
    private static readonly DateOnly End = new(2026, 9, 30);

    [Fact]
    public async Task Create_Valid_PersistsExactlyOnce()
    {
        var store = new CountingStore();
        var service = new CampaignManagementService(store);
        var result = await service.CreateAsync(new("  Campaign A  ", Start, End));
        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(1, store.SaveCount);
        Assert.Equal("Campaign A", result.Campaign!.Name);
    }

    [Fact]
    public async Task Create_Invalid_DoesNotWrite()
    {
        var store = new CountingStore();
        var result = await new CampaignManagementService(store).CreateAsync(new(" ", Start, End));
        Assert.False(result.Success);
        Assert.Equal(0, store.SaveCount);
    }
    [Fact]
    public async Task Update_NoChange_WritesZero()
    {
        var store = new CountingStore();
        await store.SeedAsync(Item("55555555-5555-5555-5555-555555555555", "Draft"));
        store.Reset();
        var service = new CampaignManagementService(store);
        var result = await service.UpdateAsync("55555555-5555-5555-5555-555555555555", new("Campaign A", Start, End));
        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task Activate_ThenComplete_PersistsEachChangedTransition()
    {
        var store = new CountingStore();
        var id = "66666666-6666-6666-6666-666666666666";
        await store.SeedAsync(Item(id, "Draft"));
        store.Reset();
        var service = new CampaignManagementService(store);
        var active = await service.ActivateAsync(id);
        var completed = await service.CompleteAsync(id);
        Assert.True(active.Success);
        Assert.True(completed.Success);
        Assert.Equal(2, store.SaveCount);
        Assert.Equal("Completed", completed.Campaign!.Status.ToString());
    }

    [Fact]
    public async Task RepeatCancel_IsIdempotentAndDoesNotWrite()
    {
        var store = new CountingStore();
        var id = "77777777-7777-7777-7777-777777777777";
        await store.SeedAsync(Item(id, "Cancelled"));
        store.Reset();
        var result = await new CampaignManagementService(store).CancelAsync(id);
        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(0, store.SaveCount);
    }
    private static CrmFoundationPreviewItemContract Item(string id, string status) =>
        new(id, "Campaign", "Campaign A", status, DateTimeOffset.UtcNow,
            new Dictionary<string, string> { ["startDate"] = "2026-09-01", ["endDate"] = "2026-09-30" });

    private sealed class CountingStore : ICampaignFoundationStore
    {
        private readonly Dictionary<string, CrmFoundationPreviewItemContract> items = new(StringComparer.OrdinalIgnoreCase);
        public int SaveCount { get; private set; }
        public Task SeedAsync(CrmFoundationPreviewItemContract item) { items[item.Id] = item; return Task.CompletedTask; }
        public void Reset() => SaveCount = 0;
        public Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyCollection<CrmFoundationPreviewItemContract>>(items.Values.ToArray());
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
            items[preview.Id] = preview;
            return Task.FromResult(preview);
        }
        public Task ClearPreviewAsync(CancellationToken cancellationToken = default) { items.Clear(); return Task.CompletedTask; }
        public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(new CrmFoundationStoreStatusContract("Campaign", true, true, false, items.Count, "NonProductionSeam", "NonProduction", "Test"));
    }
}