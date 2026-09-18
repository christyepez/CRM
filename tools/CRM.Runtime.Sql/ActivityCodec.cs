using System.Text.Json;
using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Migration.Application;

namespace CRM.Runtime.Sql;

public sealed record ActivityState(string Id, ActivityType Type, string Subject, DateTimeOffset ScheduledAtUtc,
    string? LeadId, string? ContactId, ActivityStatus Status, DateTimeOffset? CompletedAtUtc);

public static class ActivityCodec
{
    public static JsonElement Encode(Activity item) => JsonSerializer.SerializeToElement(new ActivityState(
        item.Id.ToString(), item.Type, item.Subject, item.ScheduledAtUtc,
        item.LeadId?.ToString(), item.ContactId?.ToString(), item.Status, item.CompletedAtUtc), SnapshotValidation.JsonOptions);

    public static Activity Decode(JsonElement payload)
    {
        var state = payload.Deserialize<ActivityState>(SnapshotValidation.JsonOptions)
            ?? throw new ArgumentException("Invalid activity state.");
        if (!Enum.IsDefined(state.Type) || !Enum.IsDefined(state.Status) || string.IsNullOrWhiteSpace(state.Subject) ||
            state.ScheduledAtUtc == default || state.ScheduledAtUtc.Offset != TimeSpan.Zero ||
            (state.LeadId is not null && state.ContactId is not null) ||
            (state.Status == ActivityStatus.Completed) != state.CompletedAtUtc.HasValue ||
            (state.CompletedAtUtc is { } completed && (completed == default || completed.Offset != TimeSpan.Zero)))
            throw new ArgumentException("Invalid activity lifecycle state.");
        return Activity.Restore(Parse(state.Id), state.Type, state.Subject, state.ScheduledAtUtc,
            state.LeadId is null ? null : Parse(state.LeadId), state.ContactId is null ? null : Parse(state.ContactId),
            state.Status, state.CompletedAtUtc);
    }

    private static CrmId Parse(string id) => CrmId.From(Guid.ParseExact(id, "D"));
}
