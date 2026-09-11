using CRM.Domain.InteractionManagement;
using Xunit;

namespace CRM.UnitTests;

public sealed class InteractionManagementPolicyTests
{
    private static readonly string InteractionId = Guid.NewGuid().ToString();
    private static readonly string RelatedEntityId = Guid.NewGuid().ToString();
    private static readonly DateTimeOffset OccurredAt = new(2026, 9, 10, 15, 30, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset EvaluatedAt = new(2026, 9, 11, 15, 30, 0, TimeSpan.Zero);

    [Fact]
    public void Create_ValidInteraction_IsRecordedAndChanged()
    {
        var result = InteractionManagementPolicy.Evaluate(Command());

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(InteractionStatus.Recorded, result.ResultStatus);
    }

    [Fact]
    public void Create_NormalizesTextAndRelatedEntityId()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            relatedEntityId: $"  {RelatedEntityId}  ",
            subject: "  Discovery call  ",
            summary: "  Talked through onboarding  "));

        Assert.True(result.Success);
        Assert.Equal(RelatedEntityId, result.NormalizedRelatedEntityId);
        Assert.Equal("Discovery call", result.NormalizedSubject);
        Assert.Equal("Talked through onboarding", result.NormalizedSummary);
    }

    [Theory]
    [InlineData(InteractionRelatedEntityType.Customer)]
    [InlineData(InteractionRelatedEntityType.Contact)]
    [InlineData(InteractionRelatedEntityType.Lead)]
    [InlineData(InteractionRelatedEntityType.Opportunity)]
    [InlineData(InteractionRelatedEntityType.Case)]
    public void Create_AcceptsCanonicalRelatedEntityTypes(InteractionRelatedEntityType type)
    {
        var result = InteractionManagementPolicy.Evaluate(Command(relatedEntityType: type));

        Assert.True(result.Success);
        Assert.Equal(type, result.RelatedEntityType);
    }

    [Theory]
    [InlineData(InteractionChannel.Email)]
    [InlineData(InteractionChannel.Phone)]
    [InlineData(InteractionChannel.Meeting)]
    [InlineData(InteractionChannel.Chat)]
    [InlineData(InteractionChannel.Other)]
    public void Create_AcceptsCanonicalChannels(InteractionChannel channel)
    {
        var result = InteractionManagementPolicy.Evaluate(Command(channel: channel));

        Assert.True(result.Success);
        Assert.Equal(channel, result.Channel);
    }

    [Theory]
    [InlineData(InteractionDirection.Inbound)]
    [InlineData(InteractionDirection.Outbound)]
    public void Create_AcceptsCanonicalDirections(InteractionDirection direction)
    {
        var result = InteractionManagementPolicy.Evaluate(Command(direction: direction));

        Assert.True(result.Success);
        Assert.Equal(direction, result.Direction);
    }

    [Fact]
    public void Create_RejectsInvalidOperation()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(operation: (InteractionManagementOperation)99));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidOperation, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidRelatedEntityType()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(relatedEntityType: (InteractionRelatedEntityType)99));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidRelatedEntityType, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidChannel()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(channel: (InteractionChannel)99));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidChannel, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidDirection()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(direction: (InteractionDirection)99));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidDirection, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsMissingRelatedEntityId()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(relatedEntityId: " "));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.RelatedEntityIdRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidRelatedEntityId()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(relatedEntityId: "not-a-guid"));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidRelatedEntityId, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsEmptyRelatedEntityId()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(relatedEntityId: Guid.Empty.ToString()));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidRelatedEntityId, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsMissingSubject()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(subject: " "));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.SubjectRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsSubjectAboveMaxLength()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(subject: new string('S', InteractionManagementPolicy.MaxSubjectLength + 1)));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.SubjectTooLong, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsMissingSummary()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(summary: " "));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.SummaryRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsSummaryAboveMaxLength()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(summary: new string('S', InteractionManagementPolicy.MaxSummaryLength + 1)));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.SummaryTooLong, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsMissingOccurredAtUtc()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(occurredAtUtc: DateTimeOffset.MinValue));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.OccurredAtUtcRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsNonUtcOccurredAt()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(occurredAtUtc: new DateTimeOffset(2026, 9, 10, 15, 30, 0, TimeSpan.FromHours(-5))));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.OccurredAtUtcMustBeUtc, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsFutureOccurredAt()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(occurredAtUtc: EvaluatedAt.AddTicks(1)));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.OccurredAtUtcInFuture, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsNonUtcEvaluationTimestamp()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(evaluatedAtUtc: new DateTimeOffset(2026, 9, 11, 15, 30, 0, TimeSpan.FromHours(-5))));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.EvaluationTimestampMustBeUtc, result.ErrorCode);
    }

    [Fact]
    public void Update_RecordedInteraction_AllowsChangedFields()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Update,
            subject: "Updated subject",
            existing: Snapshot()));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(InteractionStatus.Recorded, result.ResultStatus);
    }

    [Fact]
    public void Update_RecordedInteraction_ReturnsNoChangeForSameNormalizedState()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Update,
            relatedEntityId: $" {RelatedEntityId} ",
            subject: " Subject ",
            summary: " Summary ",
            existing: Snapshot()));

        Assert.True(result.Success);
        Assert.False(result.Changed);
    }

    [Fact]
    public void Update_RejectsInvalidInteractionId()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Update,
            interactionId: "bad",
            existing: Snapshot()));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidInteractionId, result.ErrorCode);
    }

    [Fact]
    public void Update_RejectsMissingSnapshot()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(operation: InteractionManagementOperation.Update));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InteractionNotFound, result.ErrorCode);
    }

    [Fact]
    public void Update_RejectsMismatchedSnapshotId()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Update,
            existing: Snapshot(interactionId: Guid.NewGuid().ToString())));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidInteractionId, result.ErrorCode);
    }

    [Fact]
    public void Update_RejectsVoidedInteraction()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Update,
            existing: Snapshot(status: InteractionStatus.Voided)));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.VoidedInteractionCannotBeModified, result.ErrorCode);
        Assert.Equal(InteractionStatus.Voided, result.ResultStatus);
    }

    [Fact]
    public void Void_RecordedInteraction_ChangesToVoided()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Void,
            existing: Snapshot()));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(InteractionStatus.Voided, result.ResultStatus);
    }

    [Fact]
    public void Void_RepeatedOnVoidedInteraction_IsNoChange()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Void,
            existing: Snapshot(status: InteractionStatus.Voided)));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(InteractionStatus.Voided, result.ResultStatus);
    }

    [Fact]
    public void NonCreate_RejectsInvalidExistingStatus()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            operation: InteractionManagementOperation.Void,
            existing: Snapshot(status: (InteractionStatus)99)));

        Assert.False(result.Success);
        Assert.Equal(InteractionManagementErrorCode.InvalidStatus, result.ErrorCode);
    }

    [Fact]
    public void Create_AcceptsExactTextBoundaries()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(
            subject: new string('S', InteractionManagementPolicy.MaxSubjectLength),
            summary: new string('X', InteractionManagementPolicy.MaxSummaryLength)));
        Assert.True(result.Success);
    }

    [Fact]
    public void Create_AllowsOccurrenceExactlyAtEvaluationTimestamp()
    {
        var result = InteractionManagementPolicy.Evaluate(Command(occurredAtUtc: EvaluatedAt));
        Assert.True(result.Success);
        Assert.Equal(EvaluatedAt, result.OccurredAtUtc);
    }
    private static InteractionManagementCommand Command(
        InteractionManagementOperation operation = InteractionManagementOperation.Create,
        string? interactionId = null,
        InteractionRelatedEntityType relatedEntityType = InteractionRelatedEntityType.Customer,
        string? relatedEntityId = null,
        InteractionChannel channel = InteractionChannel.Phone,
        InteractionDirection direction = InteractionDirection.Outbound,
        string? subject = "Subject",
        string? summary = "Summary",
        DateTimeOffset? occurredAtUtc = null,
        DateTimeOffset? evaluatedAtUtc = null,
        InteractionManagementSnapshot? existing = null) =>
        new(
            operation,
            operation == InteractionManagementOperation.Create ? null : interactionId ?? InteractionId,
            relatedEntityType,
            relatedEntityId ?? RelatedEntityId,
            channel,
            direction,
            subject,
            summary,
            occurredAtUtc ?? OccurredAt,
            evaluatedAtUtc ?? EvaluatedAt,
            existing);

    private static InteractionManagementSnapshot Snapshot(
        string? interactionId = null,
        InteractionRelatedEntityType relatedEntityType = InteractionRelatedEntityType.Customer,
        string? relatedEntityId = null,
        InteractionChannel channel = InteractionChannel.Phone,
        InteractionDirection direction = InteractionDirection.Outbound,
        string subject = "Subject",
        string summary = "Summary",
        DateTimeOffset? occurredAtUtc = null,
        InteractionStatus status = InteractionStatus.Recorded) =>
        new(
            interactionId ?? InteractionId,
            relatedEntityType,
            relatedEntityId ?? RelatedEntityId,
            channel,
            direction,
            subject,
            summary,
            occurredAtUtc ?? OccurredAt,
            status);
}
