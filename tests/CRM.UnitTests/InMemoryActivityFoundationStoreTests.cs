using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class InMemoryActivityFoundationStoreTests
{
    [Fact]
    public async Task SaveAsync_PersistsActivityAndGetByIdFindsCopy()
    {
        var store = new InMemoryActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Synthetic call", new DateTimeOffset(2026, 8, 27, 14, 0, 0, TimeSpan.Zero));

        var saved = await store.SaveAsync(activity);
        var found = await store.GetByIdAsync(saved.Id.ToString());

        Assert.NotNull(found);
        Assert.Equal(saved.Id, found!.Id);
        Assert.NotSame(saved, found);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsSyntheticSeedAndSavedActivity()
    {
        var store = new InMemoryActivityFoundationStore();
        var activity = Activity.ScheduleForContact(CrmId.New(), CrmId.New(), ActivityType.Email, "Synthetic email", new DateTimeOffset(2026, 8, 27, 14, 0, 0, TimeSpan.Zero));

        await store.SaveAsync(activity);
        var all = await store.GetAllAsync();

        Assert.Contains(all, item => item.Subject == "Synthetic follow-up call");
        Assert.Contains(all, item => item.Id == activity.Id);
    }

    [Fact]
    public async Task SaveAsync_UsesDefensiveCopy()
    {
        var store = new InMemoryActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Synthetic call", new DateTimeOffset(2026, 8, 27, 14, 0, 0, TimeSpan.Zero));

        var saved = await store.SaveAsync(activity);
        saved.Complete(new DateTimeOffset(2026, 8, 27, 15, 0, 0, TimeSpan.Zero));
        var found = await store.GetByIdAsync(activity.Id.ToString());

        Assert.Equal(ActivityStatus.Scheduled, found!.Status);
    }

    [Fact]
    public async Task Operations_HonorCancelledCancellationToken()
    {
        var store = new InMemoryActivityFoundationStore();
        using var source = new CancellationTokenSource();
        source.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() => store.GetAllAsync(source.Token));
        await Assert.ThrowsAsync<OperationCanceledException>(() => store.GetByIdAsync(Guid.NewGuid().ToString(), source.Token));
        await Assert.ThrowsAsync<OperationCanceledException>(() => store.SaveAsync(Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Call", DateTimeOffset.UtcNow), source.Token));
        await Assert.ThrowsAsync<OperationCanceledException>(() => store.GetStatusAsync(source.Token));
    }
}
