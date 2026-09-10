using CRM.Application.SegmentManagement;
using CRM.Infrastructure.Persistence.Foundation;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class SegmentManagementServiceTests
{
    [Fact]
    public async Task Store_HasDeterministicSeed()
    {
        var store=new InMemorySegmentFoundationStore();
        var all=await store.GetAllAsync();
        var seed=Assert.Single(all);
        Assert.Equal("bbbbbbbb-2222-2222-2222-222222222222",seed.Id);
        Assert.Equal(SegmentStatus.Draft,seed.Status);
    }

    [Fact]
    public async Task Create_Normalizes_AndPersists()
    {
        var service=new SegmentManagementService(new InMemorySegmentFoundationStore());
        var result=await service.CreateAsync(new("  Priority Customers  ","  Customers with priority profile.  "));
        Assert.True(result.Success); Assert.True(result.Changed);
        Assert.Equal("Priority Customers",result.Segment!.Name);
        Assert.Equal("Customers with priority profile.",result.Segment.CriteriaSummary);
        Assert.Equal(SegmentStatus.Draft,result.Status);
        Assert.NotNull(await service.GetByIdAsync(result.SegmentId!));
    }
    [Fact]
    public async Task Update_NoChange_DoesNotChangeState()
    {
        var service=new SegmentManagementService(new InMemorySegmentFoundationStore());
        var seed=await service.GetByIdAsync("bbbbbbbb-2222-2222-2222-222222222222");
        var result=await service.UpdateAsync(seed!.Id,new(seed.Name,seed.CriteriaSummary));
        Assert.True(result.Success); Assert.False(result.Changed);
    }

    [Fact]
    public async Task Lifecycle_IsIdempotent()
    {
        var service=new SegmentManagementService(new InMemorySegmentFoundationStore());
        const string id="bbbbbbbb-2222-2222-2222-222222222222";
        var active=await service.ActivateAsync(id); Assert.True(active.Changed); Assert.Equal(SegmentStatus.Active,active.Status);
        var repeatActive=await service.ActivateAsync(id); Assert.False(repeatActive.Changed);
        var inactive=await service.DeactivateAsync(id); Assert.True(inactive.Changed); Assert.Equal(SegmentStatus.Inactive,inactive.Status);
        var repeatInactive=await service.DeactivateAsync(id); Assert.False(repeatInactive.Changed);
    }

    [Fact]
    public async Task InvalidAndMissing_DoNotPersist()
    {
        var service=new SegmentManagementService(new InMemorySegmentFoundationStore());
        var before=(await service.GetAllAsync()).Count;
        var invalid=await service.CreateAsync(new(" ","criteria")); Assert.False(invalid.Success);
        var missing=await service.UpdateAsync(Guid.NewGuid().ToString(),new("Name","Criteria")); Assert.False(missing.Success);
        Assert.Equal(before,(await service.GetAllAsync()).Count);
    }
}