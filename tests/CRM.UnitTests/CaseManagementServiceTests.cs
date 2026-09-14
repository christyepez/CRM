using CRM.Application.CaseManagement;
using CRM.Domain.Enums;
using CRM.Infrastructure.Persistence.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CaseManagementServiceTests
{
    private const string SeedCaseId = "55555555-5555-5555-5555-555555555555";
    private const string SeedCustomerId = "11111111-1111-1111-1111-111111111111";

    [Fact]
    public async Task Store_HasDeterministicSyntheticSeed()
    {
        var store = new InMemoryCaseFoundationStore();

        var all = await store.GetAllAsync();

        var seed = Assert.Single(all);
        Assert.Equal(SeedCaseId, seed.Id);
        Assert.Equal(SeedCustomerId, seed.CustomerId);
        Assert.Equal(CasePriority.Medium, seed.Priority);
        Assert.Equal(CaseStatus.Open, seed.Status);
    }

    [Fact]
    public async Task Create_NormalizesAndPersists()
    {
        var service = new CaseManagementService(new InMemoryCaseFoundationStore());
        var customerId = Guid.NewGuid().ToString("D");

        var result = await service.CreateAsync(new(customerId, "  Warranty issue  ", "  Product failed after delivery.  ", CasePriority.High));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal("Warranty issue", result.Case!.Title);
        Assert.Equal("Product failed after delivery.", result.Case.Summary);
        Assert.Equal(CasePriority.High, result.Case.Priority);
        Assert.Equal(CaseStatus.Open, result.Status);
        Assert.False(result.Case.ProductiveCrudEnabled);
        Assert.Equal("NonProductionSeam", result.Case.PersistenceMode);
        Assert.NotNull(await service.GetByIdAsync(result.CaseId!));
    }

    [Fact]
    public async Task Update_NoChange_DoesNotPersist()
    {
        var service = new CaseManagementService(new InMemoryCaseFoundationStore());
        var seed = await service.GetByIdAsync(SeedCaseId);

        var result = await service.UpdateAsync(seed!.Id, new(seed.CustomerId, seed.Title, seed.Summary, seed.Priority));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(CaseStatus.Open, result.Case!.Status);
    }

    [Fact]
    public async Task Lifecycle_StartResolveClose_UsesPolicyTransitions()
    {
        var service = new CaseManagementService(new InMemoryCaseFoundationStore());

        var started = await service.StartAsync(SeedCaseId);
        var repeatedStart = await service.StartAsync(SeedCaseId);
        var resolved = await service.ResolveAsync(SeedCaseId);
        var repeatedResolve = await service.ResolveAsync(SeedCaseId);
        var closed = await service.CloseAsync(SeedCaseId);
        var repeatedClose = await service.CloseAsync(SeedCaseId);

        Assert.True(started.Success);
        Assert.True(started.Changed);
        Assert.Equal(CaseStatus.InProgress, started.Status);
        Assert.True(repeatedStart.Success);
        Assert.False(repeatedStart.Changed);
        Assert.True(resolved.Success);
        Assert.True(resolved.Changed);
        Assert.Equal(CaseStatus.Resolved, resolved.Status);
        Assert.True(repeatedResolve.Success);
        Assert.False(repeatedResolve.Changed);
        Assert.True(closed.Success);
        Assert.True(closed.Changed);
        Assert.Equal(CaseStatus.Closed, closed.Status);
        Assert.True(repeatedClose.Success);
        Assert.False(repeatedClose.Changed);
    }

    [Fact]
    public async Task InvalidMissingAndRejected_DoNotPersist()
    {
        var service = new CaseManagementService(new InMemoryCaseFoundationStore());
        var before = (await service.GetAllAsync()).Count;

        var invalid = await service.CreateAsync(new("bad", "Case", "Summary", CasePriority.Medium));
        var missing = await service.UpdateAsync(Guid.NewGuid().ToString("D"), new(SeedCustomerId, "Case", "Summary", CasePriority.Medium));
        var rejectedClose = await service.CloseAsync(SeedCaseId);

        Assert.False(invalid.Success);
        Assert.False(missing.Success);
        Assert.False(rejectedClose.Success);
        Assert.Equal(CaseStatus.Open, (await service.GetByIdAsync(SeedCaseId))!.Status);
        Assert.Equal(before, (await service.GetAllAsync()).Count);
    }
}
