using CRM.Domain.AccountManagement;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class AccountManagementPolicyTests
{
    private static readonly string AccountId = Guid.NewGuid().ToString();

    [Fact] public void Create_Valid_IsDraftAndChanged(){var r=AccountManagementPolicy.Evaluate(Command());Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(AccountStatus.Draft,r.ResultStatus);}
    [Fact] public void Create_NormalizesFields(){var r=AccountManagementPolicy.Evaluate(Command(name:"  Acme  ",taxId:"  ec-123  ",industry:"  Technology ",segment:" Enterprise "));Assert.Equal("Acme",r.NormalizedName);Assert.Equal("EC-123",r.NormalizedTaxId);Assert.Equal("Technology",r.NormalizedIndustry);Assert.Equal("Enterprise",r.NormalizedSegment);}
    [Fact] public void Create_RejectsMissingName(){var r=AccountManagementPolicy.Evaluate(Command(name:"   "));Assert.Equal(AccountManagementErrorCode.NameRequired,r.ErrorCode);}
    [Fact] public void Create_RejectsLongName(){var r=AccountManagementPolicy.Evaluate(Command(name:new string('x',161)));Assert.Equal(AccountManagementErrorCode.NameTooLong,r.ErrorCode);}
    [Fact] public void Create_RejectsLongTaxId(){var r=AccountManagementPolicy.Evaluate(Command(taxId:new string('x',65)));Assert.Equal(AccountManagementErrorCode.TaxIdTooLong,r.ErrorCode);}
    [Fact] public void Create_RejectsLongIndustry(){var r=AccountManagementPolicy.Evaluate(Command(industry:new string('x',121)));Assert.Equal(AccountManagementErrorCode.IndustryTooLong,r.ErrorCode);}
    [Fact] public void Create_RejectsLongSegment(){var r=AccountManagementPolicy.Evaluate(Command(segment:new string('x',81)));Assert.Equal(AccountManagementErrorCode.SegmentTooLong,r.ErrorCode);}
    [Fact] public void Update_Changed_Succeeds(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Update,name:"Acme Updated",existing:Snapshot()));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(AccountStatus.Draft,r.ResultStatus);}
    [Fact] public void Update_NoChange_IsSuppressed(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Update,existing:Snapshot()));Assert.True(r.Success);Assert.False(r.Changed);}
    [Theory]
    [InlineData(AccountStatus.Draft)]
    [InlineData(AccountStatus.Active)]
    [InlineData(AccountStatus.Inactive)]
    public void Update_AllStatuses_Allowed(AccountStatus status){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Update,name:"Changed",existing:Snapshot(status)));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(status,r.ResultStatus);}
    [Fact] public void Activate_Draft_Succeeds(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Activate,existing:Snapshot()));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(AccountStatus.Active,r.ResultStatus);}
    [Fact] public void Activate_Inactive_Succeeds(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Activate,existing:Snapshot(AccountStatus.Inactive)));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(AccountStatus.Active,r.ResultStatus);}
    [Fact] public void RepeatActivate_IsNoChange(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Activate,existing:Snapshot(AccountStatus.Active)));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void Deactivate_Active_Succeeds(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Deactivate,existing:Snapshot(AccountStatus.Active)));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(AccountStatus.Inactive,r.ResultStatus);}
    [Fact] public void RepeatDeactivate_IsNoChange(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Deactivate,existing:Snapshot(AccountStatus.Inactive)));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void Deactivate_Draft_Rejected(){var r=AccountManagementPolicy.Evaluate(Command(AccountManagementOperation.Deactivate,existing:Snapshot()));Assert.Equal(AccountManagementErrorCode.InvalidStatusTransition,r.ErrorCode);Assert.False(r.Changed);}
    [Fact] public void NonCreate_RequiresGuidId(){var r=AccountManagementPolicy.Evaluate(new AccountManagementCommand(AccountManagementOperation.Update,"bad","Acme","EC-1","Tech","Enterprise",Snapshot()));Assert.Equal(AccountManagementErrorCode.InvalidAccountId,r.ErrorCode);}
    [Fact] public void NonCreate_RequiresExistingSnapshot(){var r=AccountManagementPolicy.Evaluate(new AccountManagementCommand(AccountManagementOperation.Update,AccountId,"Acme","EC-1","Tech","Enterprise"));Assert.Equal(AccountManagementErrorCode.AccountNotFound,r.ErrorCode);}
    [Fact] public void NonCreate_RejectsMismatchedId(){var r=AccountManagementPolicy.Evaluate(new AccountManagementCommand(AccountManagementOperation.Update,Guid.NewGuid().ToString(),"Acme","EC-1","Tech","Enterprise",Snapshot()));Assert.Equal(AccountManagementErrorCode.InvalidAccountId,r.ErrorCode);}
    [Fact] public void InvalidOperation_Rejected(){var r=AccountManagementPolicy.Evaluate(new AccountManagementCommand((AccountManagementOperation)99,null,"Acme",null,null,null));Assert.Equal(AccountManagementErrorCode.InvalidOperation,r.ErrorCode);}

    private static AccountManagementCommand Command(AccountManagementOperation op=AccountManagementOperation.Create,string name="Acme",string? taxId="EC-1",string? industry="Tech",string? segment="Enterprise",AccountManagementSnapshot? existing=null)=>new(op,op==AccountManagementOperation.Create?null:AccountId,name,taxId,industry,segment,existing);
    private static AccountManagementSnapshot Snapshot(AccountStatus status=AccountStatus.Draft)=>new(AccountId,"Acme","EC-1","Tech","Enterprise",status);
}
