using CRM.Application.NoteManagement;
using CRM.Domain.NoteManagement;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class NoteManagementServiceTests
{
    private const string SeedId = "77777777-7777-7777-7777-777777777777";
    private const string RelatedId = "11111111-1111-1111-1111-111111111111";

    [Fact]
    public async Task Store_HasDeterministicSyntheticSeed()
    {
        var service = new NoteManagementService(new InMemoryNoteFoundationStore());
        var all = await service.GetAllAsync();
        var seed = Assert.Single(all);
        Assert.Equal(SeedId, seed.Id);
        Assert.Equal(RelatedId, seed.RelatedEntityId);
        Assert.Equal(NoteStatus.Active, seed.Status);
        Assert.False(seed.ProductiveCrudEnabled);
    }

    [Fact]
    public async Task Create_NormalizesAndPersists()
    {
        var service = new NoteManagementService(new InMemoryNoteFoundationStore());
        var result = await service.CreateAsync(new(NoteRelatedEntityType.Contact, $" {Guid.NewGuid():D} ", "  Synthetic note  "));
        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal("Synthetic note", result.Note!.Text);
        Assert.Equal(NoteStatus.Active, result.Note.Status);
    }

    [Fact]
    public async Task Update_NoChange_DoesNotPersist()
    {
        var service = new NoteManagementService(new InMemoryNoteFoundationStore());
        var seed = await service.GetByIdAsync(SeedId);
        var result = await service.UpdateAsync(seed!.Id, new(seed.RelatedEntityType, seed.RelatedEntityId, seed.Text));
        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(NoteStatus.Active, result.Note!.Status);
    }

    [Fact]
    public async Task Archive_ChangesAndRepeatIsIdempotent()
    {
        var service = new NoteManagementService(new InMemoryNoteFoundationStore());
        var archived = await service.ArchiveAsync(SeedId);
        var repeated = await service.ArchiveAsync(SeedId);
        Assert.True(archived.Success);
        Assert.True(archived.Changed);
        Assert.Equal(NoteStatus.Archived, archived.Status);
        Assert.True(repeated.Success);
        Assert.False(repeated.Changed);
        Assert.Equal(NoteStatus.Archived, repeated.Note!.Status);
    }

    [Fact]
    public async Task InvalidMissingAndArchivedUpdate_DoNotPersist()
    {
        var service = new NoteManagementService(new InMemoryNoteFoundationStore());
        var before = (await service.GetAllAsync()).Count;
        var invalid = await service.CreateAsync(new(NoteRelatedEntityType.Customer, "bad", "Text"));
        var missing = await service.UpdateAsync(Guid.NewGuid().ToString("D"), new(NoteRelatedEntityType.Customer, RelatedId, "Text"));
        await service.ArchiveAsync(SeedId);
        var rejected = await service.UpdateAsync(SeedId, new(NoteRelatedEntityType.Customer, RelatedId, "Changed"));
        Assert.False(invalid.Success);
        Assert.False(missing.Success);
        Assert.False(rejected.Success);
        Assert.Equal(before, (await service.GetAllAsync()).Count);
    }
}
