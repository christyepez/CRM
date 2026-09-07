using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryActivityFoundationStore : IActivityFoundationStore
{
    private readonly List<Activity> activities = [];
    private readonly object sync = new();

    public InMemoryActivityFoundationStore()
    {
        activities.Add(Activity.ScheduleForLead(
            CrmId.From(Guid.Parse("11111111-1111-1111-1111-111111111111")),
            CrmId.From(Guid.Parse("22222222-2222-2222-2222-222222222222")),
            ActivityType.Call,
            "Synthetic follow-up call",
            new DateTimeOffset(2026, 8, 27, 14, 0, 0, TimeSpan.Zero)));
    }

    public Task<IReadOnlyCollection<Activity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult<IReadOnlyCollection<Activity>>(activities.Select(Clone).ToArray());
        }
    }

    public Task<Activity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            var activity = activities.FirstOrDefault(item => string.Equals(item.Id.ToString(), id, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(activity is null ? null : Clone(activity));
        }
    }

    public Task<Activity> SaveAsync(Activity activity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var stored = Clone(activity);
        lock (sync)
        {
            activities.RemoveAll(item => item.Id == stored.Id);
            activities.Add(stored);
        }

        return Task.FromResult(Clone(stored));
    }

    public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            return Task.FromResult(new CrmFoundationStoreStatusContract(
                "ActivityFoundationStore",
                true,
                true,
                false,
                activities.Count,
                "NonProductionSeam",
                "NonProduction",
                CrmPersistenceSeamStatusService.WarningText));
        }
    }

    private static Activity Clone(Activity activity) =>
        Activity.Restore(
            activity.Id,
            activity.Type,
            activity.Subject,
            activity.ScheduledAtUtc,
            activity.LeadId,
            activity.ContactId,
            activity.Status,
            activity.CompletedAtUtc);
}
