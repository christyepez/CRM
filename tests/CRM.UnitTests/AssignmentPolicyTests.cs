using CRM.Domain.AssignmentManagement;
using Xunit;

namespace CRM.UnitTests;

public sealed class AssignmentPolicyTests
{
    private static readonly string RelatedId = Guid.NewGuid().ToString("D");

    [Fact]
    public void Create_NormalizesAndAcceptsValidAssignment()
    {
        var result = AssignmentPolicy.Evaluate(new(
            AssignmentOperation.Create, null, AssignmentRelatedEntityType.Contact,
            $"  {RelatedId}  ", "  portal-user-ref-1  ", "  Owner  "));
        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(RelatedId, result.NormalizedRelatedEntityId);
        Assert.Equal("portal-user-ref-1", result.NormalizedAssigneeReferenceId);
        Assert.Equal("Owner", result.NormalizedAssignmentLabel);
        Assert.Equal(AssignmentStatus.Active, result.ResultStatus);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_RequiresAssigneeReference(string assignee)
    {
        var result = AssignmentPolicy.Evaluate(new(AssignmentOperation.Create, null,
            AssignmentRelatedEntityType.Contact, RelatedId, assignee, null));
        Assert.Equal(AssignmentErrorCode.AssigneeReferenceIdRequired, result.ErrorCode);
    }

    [Fact]
    public void Update_NoChange_ReturnsChangedFalse()
    {
        var id = Guid.NewGuid().ToString("D");
        var existing = new AssignmentSnapshot(id, AssignmentRelatedEntityType.Case, RelatedId,
            "portal-user-ref-2", "Reviewer", AssignmentStatus.Active);
        var result = AssignmentPolicy.Evaluate(new(AssignmentOperation.Update, id,
            AssignmentRelatedEntityType.Case, RelatedId, "portal-user-ref-2", "Reviewer", existing));
        Assert.True(result.Success);
        Assert.False(result.Changed);
    }

    [Fact]
    public void Archive_IsIdempotent_AndArchivedUpdateIsRejected()
    {
        var id = Guid.NewGuid().ToString("D");
        var archived = new AssignmentSnapshot(id, AssignmentRelatedEntityType.Lead, RelatedId,
            "portal-user-ref-3", null, AssignmentStatus.Archived);
        var repeat = AssignmentPolicy.Evaluate(new(AssignmentOperation.Archive, id,
            archived.RelatedEntityType, archived.RelatedEntityId, archived.AssigneeReferenceId, null, archived));
        var update = AssignmentPolicy.Evaluate(new(AssignmentOperation.Update, id,
            archived.RelatedEntityType, archived.RelatedEntityId, archived.AssigneeReferenceId, "x", archived));
        Assert.True(repeat.Success);
        Assert.False(repeat.Changed);
        Assert.Equal(AssignmentErrorCode.ArchivedAssignmentCannotBeModified, update.ErrorCode);
    }
}
