using CRM.Domain.NoteManagement;
using Xunit;

namespace CRM.UnitTests;

public sealed class NoteManagementPolicyTests
{
    private static readonly string NoteId = Guid.NewGuid().ToString();
    private static readonly string RelatedId = Guid.NewGuid().ToString();

    [Fact] public void Create_Valid_IsActiveChanged(){var r=NoteManagementPolicy.Evaluate(Cmd());Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(NoteStatus.Active,r.ResultStatus);}
    [Fact] public void Create_Normalizes(){var r=NoteManagementPolicy.Evaluate(Cmd(relatedId:$"  {RelatedId}  ",text:"  hello  "));Assert.Equal(RelatedId,r.NormalizedRelatedEntityId);Assert.Equal("hello",r.NormalizedText);}
    [Fact] public void Create_RejectsMissingRelatedId(){Assert.Equal(NoteManagementErrorCode.RelatedEntityIdRequired,NoteManagementPolicy.Evaluate(Cmd(relatedId:" ")).ErrorCode);}
    [Fact] public void Create_RejectsInvalidRelatedId(){Assert.Equal(NoteManagementErrorCode.InvalidRelatedEntityId,NoteManagementPolicy.Evaluate(Cmd(relatedId:"bad")).ErrorCode);}
    [Fact] public void Create_RejectsMissingText(){Assert.Equal(NoteManagementErrorCode.TextRequired,NoteManagementPolicy.Evaluate(Cmd(text:" ")).ErrorCode);}
    [Fact] public void Create_RejectsLongText(){Assert.Equal(NoteManagementErrorCode.TextTooLong,NoteManagementPolicy.Evaluate(Cmd(text:new string('x',4001))).ErrorCode);}
    [Fact] public void Create_AcceptsExactBoundary(){Assert.True(NoteManagementPolicy.Evaluate(Cmd(text:new string('x',4000))).Success);}
    [Fact] public void Create_RejectsInvalidType(){Assert.Equal(NoteManagementErrorCode.InvalidRelatedEntityType,NoteManagementPolicy.Evaluate(Cmd(type:(NoteRelatedEntityType)99)).ErrorCode);}
    [Fact] public void Create_RejectsInvalidOperation(){Assert.Equal(NoteManagementErrorCode.InvalidOperation,NoteManagementPolicy.Evaluate(Cmd(op:(NoteManagementOperation)99)).ErrorCode);}
    [Fact] public void Update_Changed(){var r=NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Update,text:"changed",existing:Snap()));Assert.True(r.Success);Assert.True(r.Changed);}
    [Fact] public void Update_NoChange(){var r=NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Update,existing:Snap()));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void Update_ArchivedRejected(){Assert.Equal(NoteManagementErrorCode.ArchivedNoteCannotBeModified,NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Update,text:"changed",existing:Snap(NoteStatus.Archived))).ErrorCode);}
    [Fact] public void Archive_Active(){var r=NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Archive,existing:Snap()));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(NoteStatus.Archived,r.ResultStatus);}
    [Fact] public void Archive_RepeatedNoChange(){var r=NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Archive,existing:Snap(NoteStatus.Archived)));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void NonCreate_RequiresValidId(){Assert.Equal(NoteManagementErrorCode.InvalidNoteId,NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Update,noteId:"bad",existing:Snap())).ErrorCode);}
    [Fact] public void NonCreate_RequiresSnapshot(){Assert.Equal(NoteManagementErrorCode.NoteNotFound,NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Update)).ErrorCode);}
    [Fact] public void NonCreate_RejectsMismatchedSnapshotId(){Assert.Equal(NoteManagementErrorCode.InvalidNoteId,NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Update,existing:Snap(noteId:Guid.NewGuid().ToString()))).ErrorCode);}
    [Fact] public void NonCreate_RejectsInvalidStatus(){Assert.Equal(NoteManagementErrorCode.InvalidStatus,NoteManagementPolicy.Evaluate(Cmd(NoteManagementOperation.Archive,existing:Snap((NoteStatus)99))).ErrorCode);}

    [Theory]
    [InlineData(NoteRelatedEntityType.Customer)]
    [InlineData(NoteRelatedEntityType.Contact)]
    [InlineData(NoteRelatedEntityType.Lead)]
    [InlineData(NoteRelatedEntityType.Opportunity)]
    [InlineData(NoteRelatedEntityType.Case)]
    public void Create_AcceptsCanonicalTypes(NoteRelatedEntityType type)=>Assert.True(NoteManagementPolicy.Evaluate(Cmd(type:type)).Success);

    private static NoteManagementCommand Cmd(NoteManagementOperation op=NoteManagementOperation.Create,string? noteId=null,
        NoteRelatedEntityType type=NoteRelatedEntityType.Customer,string? relatedId=null,string text="note",
        NoteManagementSnapshot? existing=null)=>new(op,op==NoteManagementOperation.Create?null:noteId??NoteId,type,relatedId??RelatedId,text,existing);

    private static NoteManagementSnapshot Snap(NoteStatus status=NoteStatus.Active,string? noteId=null,
        NoteRelatedEntityType type=NoteRelatedEntityType.Customer,string? relatedId=null,string text="note")=>
        new(noteId??NoteId,type,relatedId??RelatedId,text,status);
}
