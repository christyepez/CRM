using CRM.Application.ActivityManagement;
using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.ActivityManagement;
using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class ActivityManagementServiceTests
{
    private static readonly DateTimeOffset ScheduledAt = new(2026, 8, 27, 14, 0, 0, TimeSpan.Zero);

    [Fact]
    public async Task CreateAsync_WithValidLeadTarget_WritesOnce()
    {
        var activityStore = new CountingActivityFoundationStore();
        var leadStore = new InMemoryLeadFoundationStore();
        var leadId = Guid.NewGuid().ToString();
        await SaveTargetAsync(leadStore, leadId, "Lead");
        var service = new ActivityManagementService(activityStore, leadStore, new InMemoryContactFoundationStore());

        var result = await service.CreateAsync(new ActivityManagementCreateApplicationRequest(ActivityType.Call, " Follow up ", ScheduledAt, leadId, null));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(1, activityStore.SaveCount);
        Assert.Equal("Follow up", result.Activity!.Subject);
        Assert.Equal(leadId, result.Activity.LeadId);
    }

    [Fact]
    public async Task CreateAsync_WithValidContactTarget_WritesOnce()
    {
        var activityStore = new CountingActivityFoundationStore();
        var contactStore = new InMemoryContactFoundationStore();
        var contactId = Guid.NewGuid().ToString();
        await SaveTargetAsync(contactStore, contactId, "Contact");
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), contactStore);

        var result = await service.CreateAsync(new ActivityManagementCreateApplicationRequest(ActivityType.Email, "Email contact", ScheduledAt, null, contactId));

        Assert.True(result.Success);
        Assert.Equal(1, activityStore.SaveCount);
        Assert.Equal(contactId, result.Activity!.ContactId);
    }

    [Fact]
    public async Task CreateAsync_InvalidDomainRequest_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CreateAsync(new ActivityManagementCreateApplicationRequest(ActivityType.Call, "", ScheduledAt, null, null));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.ActivityTargetRequired.ToString(), result.ErrorCode);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task CreateAsync_MissingLeadTarget_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CreateAsync(new ActivityManagementCreateApplicationRequest(ActivityType.Call, "Call", ScheduledAt, Guid.NewGuid().ToString(), null));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.ActivityNotFound.ToString(), result.ErrorCode);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task UpdateAsync_ChangedActivity_WritesOnce()
    {
        var activityStore = new CountingActivityFoundationStore();
        var leadStore = new InMemoryLeadFoundationStore();
        var leadId = Guid.NewGuid().ToString();
        await SaveTargetAsync(leadStore, leadId, "Lead");
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.From(Guid.Parse(leadId)), ActivityType.Call, "Call", ScheduledAt);
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, leadStore, new InMemoryContactFoundationStore());

        var result = await service.UpdateAsync(activity.Id.ToString(), new ActivityManagementUpdateApplicationRequest(ActivityType.Meeting, "Meeting", ScheduledAt.AddDays(1), leadId, null));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(1, activityStore.SaveCount);
        Assert.Equal(ActivityType.Meeting, result.Activity!.Type);
    }

    [Fact]
    public async Task UpdateAsync_NoChange_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var leadStore = new InMemoryLeadFoundationStore();
        var leadId = Guid.NewGuid().ToString();
        await SaveTargetAsync(leadStore, leadId, "Lead");
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.From(Guid.Parse(leadId)), ActivityType.Call, "Call", ScheduledAt);
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, leadStore, new InMemoryContactFoundationStore());

        var result = await service.UpdateAsync(activity.Id.ToString(), new ActivityManagementUpdateApplicationRequest(ActivityType.Call, "  Call  ", ScheduledAt, leadId, null));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task UpdateAsync_MissingActivity_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.UpdateAsync(Guid.NewGuid().ToString(), new ActivityManagementUpdateApplicationRequest(ActivityType.Call, "Call", ScheduledAt, Guid.NewGuid().ToString(), null));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.ActivityNotFound.ToString(), result.ErrorCode);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task CompleteAsync_ScheduledActivity_WritesOnceAndSetsCompletedAt()
    {
        var activityStore = new CountingActivityFoundationStore();
        var leadId = Guid.NewGuid();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.From(leadId), ActivityType.Call, "Call", ScheduledAt);
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CompleteAsync(activity.Id.ToString());

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(1, activityStore.SaveCount);
        Assert.Equal(ActivityStatus.Completed, result.Activity!.Status);
        Assert.NotNull(result.Activity.CompletedAtUtc);
    }

    [Fact]
    public async Task CompleteAsync_AlreadyCompleted_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Call", ScheduledAt);
        activity.Complete(ScheduledAt.AddHours(1));
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CompleteAsync(activity.Id.ToString());

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task CompleteAsync_CancelledActivity_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Call", ScheduledAt);
        activity.Cancel();
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CompleteAsync(activity.Id.ToString());

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.CancelledActivityCannotBeCompleted.ToString(), result.ErrorCode);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task CancelAsync_ScheduledActivity_WritesOnce()
    {
        var activityStore = new CountingActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Call", ScheduledAt);
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CancelAsync(activity.Id.ToString());

        Assert.True(result.Success);
        Assert.Equal(1, activityStore.SaveCount);
        Assert.Equal(ActivityStatus.Cancelled, result.Activity!.Status);
    }

    [Fact]
    public async Task CancelAsync_AlreadyCancelled_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Call", ScheduledAt);
        activity.Cancel();
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CancelAsync(activity.Id.ToString());

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(0, activityStore.SaveCount);
    }

    [Fact]
    public async Task CancelAsync_CompletedActivity_WritesZero()
    {
        var activityStore = new CountingActivityFoundationStore();
        var activity = Activity.ScheduleForLead(CrmId.New(), CrmId.New(), ActivityType.Call, "Call", ScheduledAt);
        activity.Complete(ScheduledAt.AddHours(1));
        await activityStore.SaveAsync(activity);
        activityStore.Reset();
        var service = new ActivityManagementService(activityStore, new InMemoryLeadFoundationStore(), new InMemoryContactFoundationStore());

        var result = await service.CancelAsync(activity.Id.ToString());

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.CompletedActivityCannotBeCancelled.ToString(), result.ErrorCode);
        Assert.Equal(0, activityStore.SaveCount);
    }

    private static Task SaveTargetAsync(ILeadFoundationStore store, string id, string entity) =>
        store.SavePreviewAsync(new CrmFoundationPreviewItemContract(id, entity, "Synthetic Target", "PreviewOnly", ScheduledAt, new Dictionary<string, string>()));

    private static Task SaveTargetAsync(IContactFoundationStore store, string id, string entity) =>
        store.SavePreviewAsync(new CrmFoundationPreviewItemContract(id, entity, "Synthetic Target", "PreviewOnly", ScheduledAt, new Dictionary<string, string>()));

    private sealed class CountingActivityFoundationStore : IActivityFoundationStore
    {
        private readonly List<Activity> activities = [];

        public int SaveCount { get; private set; }

        public Task<IReadOnlyCollection<Activity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult<IReadOnlyCollection<Activity>>(activities.ToArray());
        }

        public Task<Activity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(activities.FirstOrDefault(activity => string.Equals(activity.Id.ToString(), id, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<Activity> SaveAsync(Activity activity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SaveCount++;
            activities.RemoveAll(item => item.Id == activity.Id);
            activities.Add(activity);
            return Task.FromResult(activity);
        }

        public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(new CrmFoundationStoreStatusContract("ActivityFoundationStore", true, true, false, activities.Count, "NonProductionSeam", "NonProduction", "Synthetic"));

        public void Reset() => SaveCount = 0;
    }
}
