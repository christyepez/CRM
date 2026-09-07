using CRM.Domain.ActivityManagement;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class ActivityManagementPolicyTests
{
    private static readonly DateTimeOffset ScheduledAt = new(2026, 8, 27, 14, 0, 0, TimeSpan.Zero);
    private static readonly string LeadId = Guid.NewGuid().ToString();
    private static readonly string ContactId = Guid.NewGuid().ToString();
    private static readonly string ActivityId = Guid.NewGuid().ToString();

    [Fact]
    public void Create_AllowsValidLeadTargetedActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(leadId: LeadId));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(ActivityStatus.Scheduled, result.ResultStatus);
        Assert.Equal(LeadId, result.NormalizedLeadId);
        Assert.Null(result.NormalizedContactId);
    }

    [Fact]
    public void Create_AllowsValidContactTargetedActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(contactId: ContactId));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(ContactId, result.NormalizedContactId);
        Assert.Null(result.NormalizedLeadId);
    }

    [Fact]
    public void Create_RejectsMissingTarget()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand());

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.ActivityTargetRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsMultipleTargets()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(leadId: LeadId, contactId: ContactId));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.MultipleActivityTargetsNotAllowed, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidLeadId()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(leadId: "not-a-guid"));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.InvalidLeadId, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidContactId()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(contactId: Guid.Empty.ToString()));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.InvalidContactId, result.ErrorCode);
    }

    [Theory]
    [InlineData(ActivityType.Call)]
    [InlineData(ActivityType.Email)]
    [InlineData(ActivityType.Meeting)]
    [InlineData(ActivityType.Task)]
    public void Create_AcceptsCanonicalActivityTypes(ActivityType type)
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(type: type, leadId: LeadId));

        Assert.True(result.Success);
        Assert.Equal(type, result.Type);
    }

    [Fact]
    public void Create_RejectsInvalidActivityType()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(type: (ActivityType)99, leadId: LeadId));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.InvalidActivityType, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsMissingSubject()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(subject: "   ", leadId: LeadId));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.SubjectRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_NormalizesSubject()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(subject: "  Follow up call  ", leadId: LeadId));

        Assert.True(result.Success);
        Assert.Equal("Follow up call", result.NormalizedSubject);
    }

    [Fact]
    public void Create_RejectsSubjectAboveMaxLength()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(subject: new string('A', ActivityManagementPolicy.MaxSubjectLength + 1), leadId: LeadId));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.SubjectTooLong, result.ErrorCode);
    }

    [Fact]
    public void Create_AllowsPastScheduledAtForHistoricalRecording()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(scheduledAtUtc: new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero), leadId: LeadId));

        Assert.True(result.Success);
    }

    [Fact]
    public void Create_RejectsMissingScheduledAt()
    {
        var result = ActivityManagementPolicy.Evaluate(CreateCommand(scheduledAtUtc: DateTimeOffset.MinValue, leadId: LeadId));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.ScheduledAtRequired, result.ErrorCode);
    }

    [Fact]
    public void Update_AllowsChangedScheduledActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(UpdateCommand(subject: "Updated call", leadId: LeadId));

        Assert.True(result.Success);
        Assert.True(result.Changed);
    }

    [Fact]
    public void Update_ReturnsNoChangeForSameNormalizedState()
    {
        var result = ActivityManagementPolicy.Evaluate(UpdateCommand(subject: "  Follow up  ", leadId: LeadId));

        Assert.True(result.Success);
        Assert.False(result.Changed);
    }

    [Fact]
    public void Update_RejectsCompletedActivityMutation()
    {
        var result = ActivityManagementPolicy.Evaluate(UpdateCommand(leadId: LeadId, existingStatus: ActivityStatus.Completed));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.CompletedActivityCannotBeModified, result.ErrorCode);
    }

    [Fact]
    public void Update_RejectsCancelledActivityMutation()
    {
        var result = ActivityManagementPolicy.Evaluate(UpdateCommand(leadId: LeadId, existingStatus: ActivityStatus.Cancelled));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.CancelledActivityCannotBeModified, result.ErrorCode);
    }

    [Fact]
    public void Complete_AllowsScheduledActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CompleteCommand(existingStatus: ActivityStatus.Scheduled));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(ActivityStatus.Completed, result.ResultStatus);
    }

    [Fact]
    public void Complete_IsNoChangeForAlreadyCompletedActivity()
    {
        var completedAt = ScheduledAt.AddHours(1);
        var result = ActivityManagementPolicy.Evaluate(CompleteCommand(existingStatus: ActivityStatus.Completed, completedAtUtc: completedAt));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(completedAt, result.CompletedAtUtc);
    }

    [Fact]
    public void Complete_RejectsCancelledActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CompleteCommand(existingStatus: ActivityStatus.Cancelled));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.CancelledActivityCannotBeCompleted, result.ErrorCode);
    }

    [Fact]
    public void Cancel_AllowsScheduledActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CancelCommand(existingStatus: ActivityStatus.Scheduled));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(ActivityStatus.Cancelled, result.ResultStatus);
    }

    [Fact]
    public void Cancel_IsNoChangeForAlreadyCancelledActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CancelCommand(existingStatus: ActivityStatus.Cancelled));

        Assert.True(result.Success);
        Assert.False(result.Changed);
    }

    [Fact]
    public void Cancel_RejectsCompletedActivity()
    {
        var result = ActivityManagementPolicy.Evaluate(CancelCommand(existingStatus: ActivityStatus.Completed));

        Assert.False(result.Success);
        Assert.Equal(ActivityManagementErrorCode.CompletedActivityCannotBeCancelled, result.ErrorCode);
    }

    private static ActivityManagementCommand CreateCommand(
        ActivityType type = ActivityType.Call,
        string? subject = "Follow up",
        DateTimeOffset? scheduledAtUtc = null,
        string? leadId = null,
        string? contactId = null) =>
        new(
            ActivityManagementOperation.Create,
            ActivityId: null,
            type,
            subject,
            scheduledAtUtc ?? ScheduledAt,
            leadId,
            contactId);

    private static ActivityManagementCommand UpdateCommand(
        string? subject = "Follow up",
        string? leadId = null,
        string? contactId = null,
        ActivityStatus existingStatus = ActivityStatus.Scheduled) =>
        new(
            ActivityManagementOperation.Update,
            ActivityId,
            ActivityType.Call,
            subject,
            ScheduledAt,
            leadId,
            contactId,
            ExistingActivity: new ActivityManagementSnapshot(ActivityId, ActivityType.Call, "Follow up", ScheduledAt, LeadId, null, existingStatus));

    private static ActivityManagementCommand CompleteCommand(ActivityStatus existingStatus, DateTimeOffset? completedAtUtc = null) =>
        new(
            ActivityManagementOperation.Complete,
            ActivityId,
            ActivityType.Call,
            "Ignored by completion",
            ScheduledAt,
            LeadId,
            null,
            ExistingActivity: new ActivityManagementSnapshot(ActivityId, ActivityType.Call, "Follow up", ScheduledAt, LeadId, null, existingStatus, completedAtUtc));

    private static ActivityManagementCommand CancelCommand(ActivityStatus existingStatus) =>
        new(
            ActivityManagementOperation.Cancel,
            ActivityId,
            ActivityType.Call,
            "Ignored by cancellation",
            ScheduledAt,
            LeadId,
            null,
            ExistingActivity: new ActivityManagementSnapshot(ActivityId, ActivityType.Call, "Follow up", ScheduledAt, LeadId, null, existingStatus));
}
