using CRM.Application.AssignmentManagement;
using CRM.Domain.AssignmentManagement;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class AssignmentManagementServiceTests
{
    [Fact]
    public async Task GetAll_ReturnsSyntheticMetadataWithRuntimeFlagsDisabled()
    {
        var service = NewService();
        var items = await service.GetAllAsync();
        var item = Assert.Single(items);
        Assert.Equal("NonProductionSeam", item.PersistenceMode);
        Assert.False(item.ProductiveCrudEnabled);
        Assert.False(item.PortalIdentityRuntimeEnabled);
    }

    [Fact]
    public async Task Create_NormalizesAndPersistsReferenceOnly()
    {
        var service = NewService();
        var result = await service.CreateAsync(new(AssignmentRelatedEntityType.Lead,
            Guid.NewGuid().ToString("D"), "  portal-user-42  ", "  Primary owner  "));
        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal("portal-user-42", result.Assignment!.AssigneeReferenceId);
        Assert.Equal("Primary owner", result.Assignment.AssignmentLabel);
    }

    [Fact]
    public async Task Update_NoChange_ReturnsChangedFalse()
    {
        var service = NewService();
        var item = Assert.Single(await service.GetAllAsync());
        var result = await service.UpdateAsync(item.Id, new(item.RelatedEntityType,
            item.RelatedEntityId, item.AssigneeReferenceId, item.AssignmentLabel));
        Assert.True(result.Success);
        Assert.False(result.Changed);
    }

    [Fact]
    public async Task Archive_IsIdempotent_AndArchivedUpdateConflictsAtDomainLevel()
    {
        var service = NewService();
        var item = Assert.Single(await service.GetAllAsync());
        var first = await service.ArchiveAsync(item.Id);
        var second = await service.ArchiveAsync(item.Id);
        Assert.True(first.Changed);
        Assert.False(second.Changed);
        var update = await service.UpdateAsync(item.Id, new(item.RelatedEntityType,
            item.RelatedEntityId, "portal-user-new", item.AssignmentLabel));
        Assert.False(update.Success);
        Assert.Equal(nameof(AssignmentErrorCode.ArchivedAssignmentCannotBeModified), update.ErrorCode);
    }

    private static AssignmentManagementService NewService() =>
        new(new InMemoryAssignmentFoundationStore());
}
