using CRM.Domain.DocumentMetadata;
using Xunit;

namespace CRM.UnitTests;

public sealed class DocumentMetadataPolicyTests
{
    private static readonly string DocumentId = Guid.NewGuid().ToString("D");
    private static readonly string RelatedId = Guid.NewGuid().ToString("D");

    [Fact] public void Create_Valid_NormalizesAndStartsActive(){var r=DocumentMetadataPolicy.Evaluate(Cmd(fileName:"  file.pdf  ",fileRef:"  portal-ref-1  "));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal("file.pdf",r.NormalizedFileName);Assert.Equal("portal-ref-1",r.NormalizedFileReferenceId);Assert.Equal(DocumentMetadataStatus.Active,r.ResultStatus);}
    [Fact] public void Create_RejectsInvalidRelatedId()=>Assert.Equal(DocumentMetadataErrorCode.InvalidRelatedEntityId,DocumentMetadataPolicy.Evaluate(Cmd(relatedId:"bad")).ErrorCode);
    [Fact] public void Create_RejectsMissingFileReference()=>Assert.Equal(DocumentMetadataErrorCode.FileReferenceIdRequired,DocumentMetadataPolicy.Evaluate(Cmd(fileRef:" ")).ErrorCode);
    [Fact] public void Create_RejectsLongFileReference()=>Assert.Equal(DocumentMetadataErrorCode.FileReferenceIdTooLong,DocumentMetadataPolicy.Evaluate(Cmd(fileRef:new string('r',301))).ErrorCode);
    [Fact] public void Create_RejectsMissingFileName()=>Assert.Equal(DocumentMetadataErrorCode.FileNameRequired,DocumentMetadataPolicy.Evaluate(Cmd(fileName:" ")).ErrorCode);
    [Fact] public void Create_AcceptsFileNameBoundary()=>Assert.True(DocumentMetadataPolicy.Evaluate(Cmd(fileName:new string('f',255))).Success);
    [Fact] public void Create_RejectsLongFileName()=>Assert.Equal(DocumentMetadataErrorCode.FileNameTooLong,DocumentMetadataPolicy.Evaluate(Cmd(fileName:new string('f',256))).ErrorCode);
    [Fact] public void Create_RejectsLongContentType()=>Assert.Equal(DocumentMetadataErrorCode.ContentTypeTooLong,DocumentMetadataPolicy.Evaluate(Cmd(contentType:new string('c',121))).ErrorCode);
    [Fact] public void Create_RejectsLongDescription()=>Assert.Equal(DocumentMetadataErrorCode.DescriptionTooLong,DocumentMetadataPolicy.Evaluate(Cmd(description:new string('d',1001))).ErrorCode);
    [Fact] public void Update_NoChange_ReturnsChangedFalse(){var r=DocumentMetadataPolicy.Evaluate(Cmd(DocumentMetadataOperation.Update,existing:Snap()));Assert.True(r.Success);Assert.False(r.Changed);}
    [Fact] public void Update_Archived_IsRejected()=>Assert.Equal(DocumentMetadataErrorCode.ArchivedDocumentCannotBeModified,DocumentMetadataPolicy.Evaluate(Cmd(DocumentMetadataOperation.Update,existing:Snap(DocumentMetadataStatus.Archived))).ErrorCode);
    [Fact] public void Archive_Active_ChangesToArchived(){var r=DocumentMetadataPolicy.Evaluate(Cmd(DocumentMetadataOperation.Archive,existing:Snap()));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(DocumentMetadataStatus.Archived,r.ResultStatus);}
    [Fact] public void Archive_Repeated_IsIdempotent(){var r=DocumentMetadataPolicy.Evaluate(Cmd(DocumentMetadataOperation.Archive,existing:Snap(DocumentMetadataStatus.Archived)));Assert.True(r.Success);Assert.False(r.Changed);}

    private static DocumentMetadataCommand Cmd(DocumentMetadataOperation op=DocumentMetadataOperation.Create,string? relatedId=null,string fileRef="portal-ref",string fileName="file.pdf",string? contentType="application/pdf",string? description="Document metadata",DocumentMetadataSnapshot? existing=null)=>
        new(op,op==DocumentMetadataOperation.Create?null:DocumentId,DocumentRelatedEntityType.Contact,relatedId??RelatedId,fileRef,fileName,contentType,description,existing);

    private static DocumentMetadataSnapshot Snap(DocumentMetadataStatus status=DocumentMetadataStatus.Active)=>
        new(DocumentId,DocumentRelatedEntityType.Contact,RelatedId,"portal-ref","file.pdf","application/pdf","Document metadata",status);
}
