using CRM.Application.InteractionManagement;
using CRM.Domain.InteractionManagement;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class InteractionManagementServiceTests
{
    private const string SeedInteractionId = "66666666-6666-6666-6666-666666666666";
    private const string SeedRelatedEntityId = "22222222-2222-2222-2222-222222222222";

    [Fact]
    public async Task Store_HasDeterministicSyntheticSeed()
    {
        var store = new InMemoryInteractionFoundationStore();

        var all = await store.GetAllAsync();

        var seed = Assert.Single(all);
        Assert.Equal(SeedInteractionId, seed.Id);
        Assert.Equal(InteractionRelatedEntityType.Contact, seed.RelatedEntityType);
        Assert.Equal(SeedRelatedEntityId, seed.RelatedEntityId);
        Assert.Equal(InteractionChannel.Phone, seed.Channel);
        Assert.Equal(InteractionDirection.Outbound, seed.Direction);
        Assert.Equal(InteractionStatus.Recorded, seed.Status);
    }

    [Fact]
    public async Task Create_NormalizesAndPersists()
    {
        var service = new InteractionManagementService(new InMemoryInteractionFoundationStore());
        var relatedEntityId = Guid.NewGuid().ToString("D");
        var occurredAtUtc = DateTimeOffset.UtcNow.AddMinutes(-5);

        var result = await service.CreateAsync(new(
            InteractionRelatedEntityType.Lead,
            $"  {relatedEntityId}  ",
            InteractionChannel.Email,
            InteractionDirection.Inbound,
            "  Pricing question  ",
            "  Synthetic lead asked about pricing.  ",
            occurredAtUtc));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(relatedEntityId, result.Interaction!.RelatedEntityId);
        Assert.Equal("Pricing question", result.Interaction.Subject);
        Assert.Equal("Synthetic lead asked about pricing.", result.Interaction.Summary);
        Assert.Equal(InteractionStatus.Recorded, result.Status);
        Assert.False(result.Interaction.ProductiveCrudEnabled);
        Assert.Equal("NonProductionSeam", result.Interaction.PersistenceMode);
        Assert.NotNull(await service.GetByIdAsync(result.InteractionId!));
    }

    [Fact]
    public async Task Update_NoChange_DoesNotPersist()
    {
        var service = new InteractionManagementService(new InMemoryInteractionFoundationStore());
        var seed = await service.GetByIdAsync(SeedInteractionId);

        var result = await service.UpdateAsync(seed!.Id, new(
            seed.RelatedEntityType,
            seed.RelatedEntityId,
            seed.Channel,
            seed.Direction,
            seed.Subject,
            seed.Summary,
            seed.OccurredAtUtc));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(InteractionStatus.Recorded, result.Interaction!.Status);
    }

    [Fact]
    public async Task Void_ChangesRecordedAndRepeatIsNoChange()
    {
        var service = new InteractionManagementService(new InMemoryInteractionFoundationStore());

        var voided = await service.VoidAsync(SeedInteractionId);
        var repeated = await service.VoidAsync(SeedInteractionId);

        Assert.True(voided.Success);
        Assert.True(voided.Changed);
        Assert.Equal(InteractionStatus.Voided, voided.Status);
        Assert.True(repeated.Success);
        Assert.False(repeated.Changed);
        Assert.Equal(InteractionStatus.Voided, repeated.Interaction!.Status);
    }

    [Fact]
    public async Task InvalidMissingChangedFalseAndRejected_DoNotPersist()
    {
        var service = new InteractionManagementService(new InMemoryInteractionFoundationStore());
        var before = (await service.GetAllAsync()).Count;

        var invalid = await service.CreateAsync(new(
            InteractionRelatedEntityType.Contact,
            "bad",
            InteractionChannel.Phone,
            InteractionDirection.Outbound,
            "Subject",
            "Summary",
            DateTimeOffset.UtcNow.AddMinutes(-5)));
        var missing = await service.UpdateAsync(Guid.NewGuid().ToString("D"), new(
            InteractionRelatedEntityType.Contact,
            SeedRelatedEntityId,
            InteractionChannel.Phone,
            InteractionDirection.Outbound,
            "Subject",
            "Summary",
            DateTimeOffset.UtcNow.AddMinutes(-5)));
        var noChange = await service.UpdateAsync(SeedInteractionId, new(
            InteractionRelatedEntityType.Contact,
            SeedRelatedEntityId,
            InteractionChannel.Phone,
            InteractionDirection.Outbound,
            "Synthetic follow-up call",
            "Synthetic contact confirmed a follow-up discussion for foundation validation.",
            new DateTimeOffset(2026, 1, 15, 15, 0, 0, TimeSpan.Zero)));
        var voided = await service.VoidAsync(SeedInteractionId);
        var rejectedUpdate = await service.UpdateAsync(SeedInteractionId, new(
            InteractionRelatedEntityType.Contact,
            SeedRelatedEntityId,
            InteractionChannel.Phone,
            InteractionDirection.Outbound,
            "Changed after void",
            "This rejected update must not be persisted.",
            DateTimeOffset.UtcNow.AddMinutes(-5)));

        Assert.False(invalid.Success);
        Assert.False(missing.Success);
        Assert.True(noChange.Success);
        Assert.False(noChange.Changed);
        Assert.True(voided.Success);
        Assert.False(rejectedUpdate.Success);
        Assert.Equal(InteractionStatus.Voided, (await service.GetByIdAsync(SeedInteractionId))!.Status);
        Assert.Equal(before, (await service.GetAllAsync()).Count);
    }
}
