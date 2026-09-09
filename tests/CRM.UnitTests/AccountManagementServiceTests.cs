using CRM.Application.AccountManagement;
using CRM.Domain.Enums;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class AccountManagementServiceTests
{
    [Fact]
    public async Task Seed_ListAndDetail_AreAvailable()
    {
        var service = CreateService();
        var all = await service.GetAllAsync();
        Assert.Single(all);
        var detail = await service.GetByIdAsync(all.Single().Id);
        Assert.NotNull(detail);
        Assert.Equal(AccountStatus.Draft, detail!.Status);
        Assert.Equal("NonProductionSeam", detail.PersistenceMode);
        Assert.False(detail.ProductiveCrudEnabled);
    }

    [Fact]
    public async Task Create_NormalizesAndPersists()
    {
        var service = CreateService();
        var result = await service.CreateAsync(new("  Acme  ", " ec-1 ", " Technology ", " Enterprise "));
        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal("Acme", result.Account!.Name);
        Assert.Equal("EC-1", result.Account.TaxId);
        Assert.Equal(AccountStatus.Draft, result.Account.Status);
        Assert.NotNull(await service.GetByIdAsync(result.Account.Id));
    }

    [Fact]
    public async Task Update_NoChange_DoesNotAlterRecord()
    {
        var service = CreateService();
        var seed = (await service.GetAllAsync()).Single();
        var result = await service.UpdateAsync(seed.Id, new(seed.Name, seed.TaxId, seed.Industry, seed.Segment));
        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(seed, result.Account);
    }

    [Fact]
    public async Task ActivateThenDeactivate_UsesDomainLifecycle()
    {
        var service = CreateService();
        var seed = (await service.GetAllAsync()).Single();
        var active = await service.ActivateAsync(seed.Id);
        Assert.True(active.Success);
        Assert.Equal(AccountStatus.Active, active.Account!.Status);
        var repeat = await service.ActivateAsync(seed.Id);
        Assert.True(repeat.Success);
        Assert.False(repeat.Changed);
        var inactive = await service.DeactivateAsync(seed.Id);
        Assert.Equal(AccountStatus.Inactive, inactive.Account!.Status);
    }

    [Fact]
    public async Task MissingAccount_ReturnsNotFoundWithoutWrite()
    {
        var service = CreateService();
        var result = await service.UpdateAsync(Guid.NewGuid().ToString(), new("Acme", null, null, null));
        Assert.False(result.Success);
        Assert.Equal("AccountNotFound", result.ErrorCode);
    }

    private static AccountManagementService CreateService() => new(new InMemoryAccountFoundationStore());
}
