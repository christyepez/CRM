using CRM.Domain.CampaignManagement;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class CampaignManagementPolicyTests
{
    private static readonly string CampaignId = Guid.NewGuid().ToString();
    private static readonly DateOnly Start = new(2026, 9, 1);
    private static readonly DateOnly End = new(2026, 9, 30);

    [Fact] public void Create_Valid_IsDraftAndChanged(){var r=CampaignManagementPolicy.Evaluate(Command());Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(CampaignStatus.Draft,r.ResultStatus);}
    [Fact] public void Create_NormalizesName(){var r=CampaignManagementPolicy.Evaluate(Command(name:"  September Campaign  "));Assert.Equal("September Campaign",r.NormalizedName);}
    [Fact] public void Create_RejectsMissingName(){var r=CampaignManagementPolicy.Evaluate(Command(name:"   "));Assert.Equal(CampaignManagementErrorCode.NameRequired,r.ErrorCode);}
    [Fact] public void Create_RejectsLongName(){var r=CampaignManagementPolicy.Evaluate(Command(name:new string('x',161)));Assert.Equal(CampaignManagementErrorCode.NameTooLong,r.ErrorCode);}
    [Fact] public void Create_RejectsMissingStart(){var r=CampaignManagementPolicy.Evaluate(new CampaignManagementCommand(CampaignManagementOperation.Create,null,"Campaign",default,End));Assert.Equal(CampaignManagementErrorCode.StartDateRequired,r.ErrorCode);}
    [Fact] public void Create_RejectsMissingEnd(){var r=CampaignManagementPolicy.Evaluate(new CampaignManagementCommand(CampaignManagementOperation.Create,null,"Campaign",Start,default));Assert.Equal(CampaignManagementErrorCode.EndDateRequired,r.ErrorCode);}
    [Fact] public void Create_RejectsInvalidDateRange(){var r=CampaignManagementPolicy.Evaluate(Command(start:End,end:Start));Assert.Equal(CampaignManagementErrorCode.InvalidDateRange,r.ErrorCode);}
    [Fact] public void Update_Draft_Changed(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Update,name:"Changed",existing:Snapshot()));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(CampaignStatus.Draft,r.ResultStatus);}
    [Fact] public void Update_Draft_NoChange(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Update,existing:Snapshot()));Assert.True(r.Success);Assert.False(r.Changed);}
    [Theory]
    [InlineData(CampaignStatus.Active, CampaignManagementErrorCode.DraftCampaignRequired)]
    [InlineData(CampaignStatus.Completed, CampaignManagementErrorCode.CompletedCampaignCannotBeModified)]
    [InlineData(CampaignStatus.Cancelled, CampaignManagementErrorCode.CancelledCampaignCannotBeModified)]
    public void Update_NonDraft_Rejected(CampaignStatus status,CampaignManagementErrorCode code){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Update,existing:Snapshot(status)));Assert.Equal(code,r.ErrorCode);Assert.False(r.Changed);}
    [Fact] public void Activate_Draft_Succeeds(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Activate,existing:Snapshot()));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(CampaignStatus.Active,r.ResultStatus);}
    [Fact] public void RepeatActivate_IsNoChange(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Activate,existing:Snapshot(CampaignStatus.Active)));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void Complete_Active_Succeeds(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Complete,existing:Snapshot(CampaignStatus.Active)));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(CampaignStatus.Completed,r.ResultStatus);}
    [Fact] public void Complete_Draft_Rejected(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Complete,existing:Snapshot()));Assert.Equal(CampaignManagementErrorCode.ActiveCampaignRequired,r.ErrorCode);}
    [Fact] public void RepeatComplete_IsNoChange(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Complete,existing:Snapshot(CampaignStatus.Completed)));Assert.True(r.Success);Assert.False(r.Changed);}
    [Theory]
    [InlineData(CampaignStatus.Draft)]
    [InlineData(CampaignStatus.Active)]
    public void Cancel_DraftOrActive_Succeeds(CampaignStatus status){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Cancel,existing:Snapshot(status)));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(CampaignStatus.Cancelled,r.ResultStatus);}
    [Fact] public void RepeatCancel_IsNoChange(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Cancel,existing:Snapshot(CampaignStatus.Cancelled)));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void Activate_Completed_Rejected(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Activate,existing:Snapshot(CampaignStatus.Completed)));Assert.Equal(CampaignManagementErrorCode.CompletedCampaignCannotBeModified,r.ErrorCode);}
    [Fact] public void Activate_Cancelled_Rejected(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Activate,existing:Snapshot(CampaignStatus.Cancelled)));Assert.Equal(CampaignManagementErrorCode.CancelledCampaignCannotBeModified,r.ErrorCode);}
    [Fact] public void Complete_Cancelled_Rejected(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Complete,existing:Snapshot(CampaignStatus.Cancelled)));Assert.Equal(CampaignManagementErrorCode.CancelledCampaignCannotBeModified,r.ErrorCode);}
    [Fact] public void Cancel_Completed_Rejected(){var r=CampaignManagementPolicy.Evaluate(Command(CampaignManagementOperation.Cancel,existing:Snapshot(CampaignStatus.Completed)));Assert.Equal(CampaignManagementErrorCode.CompletedCampaignCannotBeModified,r.ErrorCode);}
    [Fact] public void NonCreate_RequiresGuidId(){var r=CampaignManagementPolicy.Evaluate(new CampaignManagementCommand(CampaignManagementOperation.Update,"bad","Campaign",Start,End,Snapshot()));Assert.Equal(CampaignManagementErrorCode.InvalidCampaignId,r.ErrorCode);}
    [Fact] public void NonCreate_RequiresExistingSnapshot(){var r=CampaignManagementPolicy.Evaluate(new CampaignManagementCommand(CampaignManagementOperation.Update,CampaignId,"Campaign",Start,End));Assert.Equal(CampaignManagementErrorCode.CampaignNotFound,r.ErrorCode);}

    private static CampaignManagementCommand Command(CampaignManagementOperation op=CampaignManagementOperation.Create,string name="Campaign",DateOnly? start=null,DateOnly? end=null,CampaignManagementSnapshot? existing=null)=>new(op,op==CampaignManagementOperation.Create?null:CampaignId,name,start??Start,end??End,existing);
    private static CampaignManagementSnapshot Snapshot(CampaignStatus status=CampaignStatus.Draft)=>new(CampaignId,"Campaign",Start,End,status);
}

