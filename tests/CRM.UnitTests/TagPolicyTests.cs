using CRM.Domain.TagManagement; using Xunit;
namespace CRM.UnitTests;
public sealed class TagPolicyTests
{
 static readonly string Id=Guid.NewGuid().ToString("D"), Related=Guid.NewGuid().ToString("D");
 static TagCommand C(TagOperation op=TagOperation.Create,string? name="tag",string? desc="desc",TagAssignment? a=null,TagSnapshot? x=null,string? id=null)=>new(op,op==TagOperation.Create?null:id??Id,name,desc,a,x);
 static TagSnapshot S(TagStatus s=TagStatus.Active,string name="tag",string? d="desc")=>new(Id,name,d,s);
 [Fact] public void Create_NormalizesAndStartsActive(){var r=TagPolicy.Evaluate(C(name:"  VIP  ",desc:" note "));Assert.True(r.Success);Assert.Equal("VIP",r.NormalizedName);Assert.Equal(TagStatus.Active,r.ResultStatus);}
 [Fact] public void Create_RejectsNameBoundaries(){Assert.Equal(TagErrorCode.NameRequired,TagPolicy.Evaluate(C(name:" ")).ErrorCode);Assert.Equal(TagErrorCode.NameTooLong,TagPolicy.Evaluate(C(name:new string('x',81))).ErrorCode);}
 [Fact] public void Create_AcceptsExactBoundaries(){Assert.True(TagPolicy.Evaluate(C(name:new string('x',80),desc:new string('y',500))).Success);}
 [Fact] public void Assignment_ValidatesStructuralReference(){Assert.True(TagPolicy.Evaluate(C(a:new(TagRelatedEntityType.Contact,Related))).Success);Assert.Equal(TagErrorCode.InvalidRelatedEntityId,TagPolicy.Evaluate(C(a:new(TagRelatedEntityType.Contact,"bad"))).ErrorCode);}
 [Fact] public void Update_NoChange_IsIdempotent(){var r=TagPolicy.Evaluate(C(TagOperation.Update,x:S()));Assert.True(r.Success);Assert.False(r.Changed);}
 [Fact] public void Update_Archived_IsRejected(){Assert.Equal(TagErrorCode.ArchivedTagCannotBeModified,TagPolicy.Evaluate(C(TagOperation.Update,x:S(TagStatus.Archived))).ErrorCode);}
 [Fact] public void Archive_IsIdempotent(){Assert.True(TagPolicy.Evaluate(C(TagOperation.Archive,x:S())).Changed);Assert.False(TagPolicy.Evaluate(C(TagOperation.Archive,x:S(TagStatus.Archived))).Changed);}
 [Theory][InlineData(TagRelatedEntityType.Customer)][InlineData(TagRelatedEntityType.Contact)][InlineData(TagRelatedEntityType.Lead)][InlineData(TagRelatedEntityType.Opportunity)][InlineData(TagRelatedEntityType.Case)] public void Assignment_AcceptsCanonicalTypes(TagRelatedEntityType t)=>Assert.True(TagPolicy.Evaluate(C(a:new(t,Related))).Success);
}
