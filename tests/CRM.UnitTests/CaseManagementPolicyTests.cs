using CRM.Domain.CaseManagement;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class CaseManagementPolicyTests
{
    private static readonly string Id = Guid.NewGuid().ToString();
    private static readonly string CustomerId = Guid.NewGuid().ToString();
    [Fact] public void Create_Valid_IsOpenChanged(){var r=CaseManagementPolicy.Evaluate(Cmd());Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(CaseStatus.Open,r.ResultStatus);}
    [Fact] public void Create_Normalizes(){var r=CaseManagementPolicy.Evaluate(Cmd(customerId:$"  {CustomerId}  ",title:"  Billing issue  ",summary:"  Invoice mismatch  "));Assert.Equal(CustomerId,r.NormalizedCustomerId);Assert.Equal("Billing issue",r.NormalizedTitle);Assert.Equal("Invoice mismatch",r.NormalizedSummary);}
    [Fact] public void Create_RejectsMissingCustomerId(){Assert.Equal(CaseManagementErrorCode.CustomerIdRequired,CaseManagementPolicy.Evaluate(Cmd(customerId:" ")).ErrorCode);}
    [Fact] public void Create_RejectsInvalidCustomerId(){Assert.Equal(CaseManagementErrorCode.InvalidCustomerId,CaseManagementPolicy.Evaluate(Cmd(customerId:"bad")).ErrorCode);}
    [Fact] public void Create_RejectsMissingTitle(){Assert.Equal(CaseManagementErrorCode.TitleRequired,CaseManagementPolicy.Evaluate(Cmd(title:" ")).ErrorCode);}
    [Fact] public void Create_RejectsLongTitle(){Assert.Equal(CaseManagementErrorCode.TitleTooLong,CaseManagementPolicy.Evaluate(Cmd(title:new string('x',161))).ErrorCode);}
    [Fact] public void Create_RejectsMissingSummary(){Assert.Equal(CaseManagementErrorCode.SummaryRequired,CaseManagementPolicy.Evaluate(Cmd(summary:" ")).ErrorCode);}
    [Fact] public void Create_RejectsLongSummary(){Assert.Equal(CaseManagementErrorCode.SummaryTooLong,CaseManagementPolicy.Evaluate(Cmd(summary:new string('x',1001))).ErrorCode);}
    [Fact] public void Create_RejectsInvalidPriority(){Assert.Equal(CaseManagementErrorCode.InvalidPriority,CaseManagementPolicy.Evaluate(Cmd(priority:(CasePriority)99)).ErrorCode);}
    [Fact] public void Update_Changed(){var r=CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Update,title:"Changed",existing:Snap()));Assert.True(r.Success);Assert.True(r.Changed);}
    [Fact] public void Update_NoChange(){var r=CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Update,existing:Snap()));Assert.True(r.Success);Assert.False(r.Changed);}
    [Theory][InlineData(CaseStatus.Open)][InlineData(CaseStatus.InProgress)] public void Update_WorkStatuses(CaseStatus s){Assert.True(CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Update,title:"Changed",existing:Snap(s))).Success);}
    [Fact] public void Update_ResolvedRejected(){Assert.Equal(CaseManagementErrorCode.ResolvedCaseCannotBeModified,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Update,title:"Changed",existing:Snap(CaseStatus.Resolved))).ErrorCode);}
    [Fact] public void Update_ClosedRejected(){Assert.Equal(CaseManagementErrorCode.ClosedCaseCannotBeModified,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Update,title:"Changed",existing:Snap(CaseStatus.Closed))).ErrorCode);}
    [Fact] public void Start_Open(){Assert.Equal(CaseStatus.InProgress,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Start,existing:Snap())).ResultStatus);}
    [Fact] public void RepeatStart_NoChange(){Assert.False(CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Start,existing:Snap(CaseStatus.InProgress))).Changed);}
    [Fact] public void Resolve_Open(){Assert.Equal(CaseStatus.Resolved,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Resolve,existing:Snap())).ResultStatus);}
    [Fact] public void Resolve_InProgress(){Assert.Equal(CaseStatus.Resolved,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Resolve,existing:Snap(CaseStatus.InProgress))).ResultStatus);}
    [Fact] public void RepeatResolve_NoChange(){Assert.False(CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Resolve,existing:Snap(CaseStatus.Resolved))).Changed);}
    [Fact] public void Close_Resolved(){Assert.Equal(CaseStatus.Closed,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Close,existing:Snap(CaseStatus.Resolved))).ResultStatus);}
    [Fact] public void RepeatClose_NoChange(){Assert.False(CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Close,existing:Snap(CaseStatus.Closed))).Changed);}
    [Fact] public void Close_InProgressRejected(){Assert.Equal(CaseManagementErrorCode.InvalidStatusTransition,CaseManagementPolicy.Evaluate(Cmd(CaseManagementOperation.Close,existing:Snap(CaseStatus.InProgress))).ErrorCode);}
    [Fact] public void NonCreate_RequiresGuid(){Assert.Equal(CaseManagementErrorCode.InvalidCaseId,CaseManagementPolicy.Evaluate(new(CaseManagementOperation.Update,"bad",CustomerId,"A","B",CasePriority.Medium,Snap())).ErrorCode);}
    [Fact] public void NonCreate_RequiresSnapshot(){Assert.Equal(CaseManagementErrorCode.CaseNotFound,CaseManagementPolicy.Evaluate(new(CaseManagementOperation.Update,Id,CustomerId,"A","B",CasePriority.Medium)).ErrorCode);}
    [Fact] public void NonCreate_RejectsMismatchedId(){Assert.Equal(CaseManagementErrorCode.InvalidCaseId,CaseManagementPolicy.Evaluate(new(CaseManagementOperation.Update,Guid.NewGuid().ToString(),CustomerId,"A","B",CasePriority.Medium,Snap())).ErrorCode);}
    [Fact] public void InvalidOperation_Rejected(){Assert.Equal(CaseManagementErrorCode.InvalidOperation,CaseManagementPolicy.Evaluate(new((CaseManagementOperation)99,null,CustomerId,"A","B",CasePriority.Medium)).ErrorCode);}

    private static CaseManagementCommand Cmd(CaseManagementOperation op=CaseManagementOperation.Create,string? customerId=null,string title="Billing issue",string summary="Invoice mismatch",CasePriority priority=CasePriority.Medium,CaseManagementSnapshot? existing=null)=>new(op,op==CaseManagementOperation.Create?null:Id,customerId??CustomerId,title,summary,priority,existing);
    private static CaseManagementSnapshot Snap(CaseStatus s=CaseStatus.Open)=>new(Id,CustomerId,"Billing issue","Invoice mismatch",CasePriority.Medium,s);
}
