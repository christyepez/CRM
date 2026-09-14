using CRM.Application.DocumentMetadata;
using CRM.Application.Ports.Persistence;
using CRM.Domain.DocumentMetadata;
using Xunit;
namespace CRM.UnitTests;
public sealed class DocumentMetadataServiceTests
{
 private sealed class Store:IDocumentMetadataFoundationStore{public List<DocumentMetadataFoundationRecord> Items=[];public int Saves;public Task<IReadOnlyCollection<DocumentMetadataFoundationRecord>> GetAllAsync(CancellationToken c=default)=>Task.FromResult<IReadOnlyCollection<DocumentMetadataFoundationRecord>>(Items.ToArray());public Task<DocumentMetadataFoundationRecord?> GetByIdAsync(string id,CancellationToken c=default)=>Task.FromResult(Items.FirstOrDefault(x=>x.Id==id));public Task<DocumentMetadataFoundationRecord> SaveAsync(DocumentMetadataFoundationRecord item,CancellationToken c=default){Saves++;Items.RemoveAll(x=>x.Id==item.Id);Items.Add(item);return Task.FromResult(item);}}
 [Fact] public async Task Create_SavesFoundationMetadataOnly(){var s=new Store();var svc=new DocumentMetadataService(s);var r=await svc.CreateAsync(new(DocumentRelatedEntityType.Contact,Guid.NewGuid().ToString("D"),"portal-ref","file.pdf","application/pdf","desc"));Assert.True(r.Success);Assert.True(r.Changed);Assert.False(r.Document!.BinaryStorageEnabled);Assert.False(r.Document.PortalContentRuntimeEnabled);Assert.Equal(1,s.Saves);}
 [Fact] public async Task Update_NoChange_SuppressesPersistence(){var s=Seed();var svc=new DocumentMetadataService(s);var x=s.Items[0];var r=await svc.UpdateAsync(x.Id,new(x.RelatedEntityType,x.RelatedEntityId,x.FileReferenceId,x.FileName,x.ContentType,x.Description));Assert.True(r.Success);Assert.False(r.Changed);Assert.Equal(0,s.Saves);}
 [Fact] public async Task Archive_Repeated_IsIdempotent(){var s=Seed(DocumentMetadataStatus.Archived);var svc=new DocumentMetadataService(s);var r=await svc.ArchiveAsync(s.Items[0].Id);Assert.True(r.Success);Assert.False(r.Changed);Assert.Equal(0,s.Saves);}
 [Fact] public async Task Missing_ReturnsNotFoundError(){var svc=new DocumentMetadataService(new Store());var r=await svc.ArchiveAsync(Guid.NewGuid().ToString("D"));Assert.False(r.Success);Assert.Equal("DocumentNotFound",r.ErrorCode);}
 private static Store Seed(DocumentMetadataStatus status=DocumentMetadataStatus.Active){var s=new Store();s.Items.Add(new(Guid.NewGuid().ToString("D"),DocumentRelatedEntityType.Contact,Guid.NewGuid().ToString("D"),"portal-ref","file.pdf","application/pdf","desc",status));return s;}
}
