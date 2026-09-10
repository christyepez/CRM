using CRM.Domain.Enums;
using CRM.Domain.SegmentManagement;
using Xunit;
namespace CRM.UnitTests;
public sealed class SegmentManagementPolicyTests
{
    private static readonly string Id=Guid.NewGuid().ToString();
    [Fact] public void Create_Valid_IsDraftChanged(){var r=SegmentManagementPolicy.Evaluate(Cmd());Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(SegmentStatus.Draft,r.ResultStatus);}
    [Fact] public void Create_Normalizes(){var r=SegmentManagementPolicy.Evaluate(Cmd(name:"  Priority  ",criteria:"  Revenue > 1000  "));Assert.Equal("Priority",r.NormalizedName);Assert.Equal("Revenue > 1000",r.NormalizedCriteriaSummary);}
    [Fact] public void Create_RejectsMissingName(){Assert.Equal(SegmentManagementErrorCode.NameRequired,SegmentManagementPolicy.Evaluate(Cmd(name:" ")).ErrorCode);}
    [Fact] public void Create_RejectsLongName(){Assert.Equal(SegmentManagementErrorCode.NameTooLong,SegmentManagementPolicy.Evaluate(Cmd(name:new string('x',161))).ErrorCode);}
    [Fact] public void Create_RejectsMissingCriteria(){Assert.Equal(SegmentManagementErrorCode.CriteriaSummaryRequired,SegmentManagementPolicy.Evaluate(Cmd(criteria:" ")).ErrorCode);}
    [Fact] public void Create_RejectsLongCriteria(){Assert.Equal(SegmentManagementErrorCode.CriteriaSummaryTooLong,SegmentManagementPolicy.Evaluate(Cmd(criteria:new string('x',1001))).ErrorCode);}
    [Fact] public void Update_Changed(){var r=SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Update,name:"Changed",existing:Snap()));Assert.True(r.Success);Assert.True(r.Changed);}
    [Fact] public void Update_NoChange(){var r=SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Update,existing:Snap()));Assert.True(r.Success);Assert.False(r.Changed);}
    [Theory][InlineData(SegmentStatus.Draft)][InlineData(SegmentStatus.Active)][InlineData(SegmentStatus.Inactive)] public void Update_AllStatuses(SegmentStatus s){Assert.True(SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Update,name:"Changed",existing:Snap(s))).Success);}
    [Fact] public void Activate_Draft(){Assert.Equal(SegmentStatus.Active,SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Activate,existing:Snap())).ResultStatus);}
    [Fact] public void Activate_Inactive(){Assert.Equal(SegmentStatus.Active,SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Activate,existing:Snap(SegmentStatus.Inactive))).ResultStatus);}
    [Fact] public void RepeatActivate_NoChange(){Assert.False(SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Activate,existing:Snap(SegmentStatus.Active))).Changed);}
    [Fact] public void Deactivate_Active(){Assert.Equal(SegmentStatus.Inactive,SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Deactivate,existing:Snap(SegmentStatus.Active))).ResultStatus);}
    [Fact] public void RepeatDeactivate_NoChange(){Assert.False(SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Deactivate,existing:Snap(SegmentStatus.Inactive))).Changed);}
    [Fact] public void Deactivate_Draft_Rejected(){Assert.Equal(SegmentManagementErrorCode.InvalidStatusTransition,SegmentManagementPolicy.Evaluate(Cmd(SegmentManagementOperation.Deactivate,existing:Snap())).ErrorCode);}    [Fact] public void NonCreate_RequiresGuid(){Assert.Equal(SegmentManagementErrorCode.InvalidSegmentId,SegmentManagementPolicy.Evaluate(new(SegmentManagementOperation.Update,"bad","A","B",Snap())).ErrorCode);}
    [Fact] public void NonCreate_RequiresSnapshot(){Assert.Equal(SegmentManagementErrorCode.SegmentNotFound,SegmentManagementPolicy.Evaluate(new(SegmentManagementOperation.Update,Id,"A","B")).ErrorCode);}
    [Fact] public void NonCreate_RejectsMismatchedId(){Assert.Equal(SegmentManagementErrorCode.InvalidSegmentId,SegmentManagementPolicy.Evaluate(new(SegmentManagementOperation.Update,Guid.NewGuid().ToString(),"A","B",Snap())).ErrorCode);}
    [Fact] public void InvalidOperation_Rejected(){Assert.Equal(SegmentManagementErrorCode.InvalidOperation,SegmentManagementPolicy.Evaluate(new((SegmentManagementOperation)99,null,"A","B")).ErrorCode);}
    private static SegmentManagementCommand Cmd(SegmentManagementOperation op=SegmentManagementOperation.Create,string name="Priority",string criteria="Revenue > 1000",SegmentManagementSnapshot? existing=null)=>new(op,op==SegmentManagementOperation.Create?null:Id,name,criteria,existing);
    private static SegmentManagementSnapshot Snap(SegmentStatus s=SegmentStatus.Draft)=>new(Id,"Priority","Revenue > 1000",s);
}