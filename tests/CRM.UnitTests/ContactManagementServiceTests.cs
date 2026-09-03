using CRM.Application.ContactManagement;
using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class ContactManagementServiceTests
{
    [Fact]
    public async Task CreateAsync_WithValidContact_PersistsExactlyOnce()
    {
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.CreateAsync(new ContactManagementCreateApplicationRequest(
            "Ada Lovelace",
            "ada@example.test",
            "0999999999",
            "Buyer",
            null,
            PreferredContactMethod.Email));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(1, store.SaveCount);
        Assert.NotNull(result.Contact);
    }

    [Fact]
    public async Task CreateAsync_WithInvalidContact_ReturnsDeterministicResultAndDoesNotWrite()
    {
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.CreateAsync(new ContactManagementCreateApplicationRequest(
            " ",
            "ada@example.test",
            null,
            null,
            null,
            PreferredContactMethod.NotSpecified));

        Assert.False(result.Success);
        Assert.Equal("NameRequired", result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task CreateAsync_PropagatesNormalizedValuesFromDomainPolicy()
    {
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.CreateAsync(new ContactManagementCreateApplicationRequest(
            "  Ada Lovelace  ",
            " ADA@EXAMPLE.TEST ",
            " 0999999999 ",
            " Buyer ",
            null,
            PreferredContactMethod.Email));

        Assert.True(result.Success);
        Assert.Equal("Ada Lovelace", result.Contact!.Name);
        Assert.Equal("ada@example.test", result.Contact.Email);
        Assert.Equal("0999999999", result.Contact.Phone);
        Assert.Equal("Buyer", result.Contact.Role);
    }

    [Fact]
    public async Task UpdateAsync_WithChangedContact_PersistsExactlyOnce()
    {
        var id = Guid.NewGuid().ToString("D");
        var store = new CountingContactFoundationStore();
        await store.SeedAsync(Preview(id, "Ada Lovelace", "ada@example.test", "0999999999", "Buyer", null, PreferredContactMethod.Email));
        store.ResetCounts();
        var service = new ContactManagementService(store);

        var result = await service.UpdateAsync(id, new ContactManagementUpdateApplicationRequest(
            "Ada Byron",
            "ada.byron@example.test",
            "0999999999",
            "Decision Maker",
            null,
            PreferredContactMethod.Email));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal(1, store.SaveCount);
        Assert.Equal(1, store.GetByIdCount);
        Assert.Equal("Ada Byron", result.Contact!.Name);
    }

    [Fact]
    public async Task UpdateAsync_WhenNotFound_ReturnsDeterministicNotFoundAndDoesNotWrite()
    {
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.UpdateAsync(Guid.NewGuid().ToString("D"), new ContactManagementUpdateApplicationRequest(
            "Ada Lovelace",
            "ada@example.test",
            null,
            null,
            null,
            PreferredContactMethod.Email));

        Assert.False(result.Success);
        Assert.Equal("ContactNotFound", result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidContact_DoesNotWrite()
    {
        var id = Guid.NewGuid().ToString("D");
        var store = new CountingContactFoundationStore();
        await store.SeedAsync(Preview(id, "Ada Lovelace", "ada@example.test", null, "Buyer", null, PreferredContactMethod.Email));
        store.ResetCounts();
        var service = new ContactManagementService(store);

        var result = await service.UpdateAsync(id, new ContactManagementUpdateApplicationRequest(
            "Ada Lovelace",
            "not-an-email",
            null,
            "Buyer",
            null,
            PreferredContactMethod.Email));

        Assert.False(result.Success);
        Assert.Equal("InvalidEmail", result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task UpdateAsync_WithSameData_SuppressesPersistence()
    {
        var id = Guid.NewGuid().ToString("D");
        var store = new CountingContactFoundationStore();
        await store.SeedAsync(Preview(id, "Ada Lovelace", "ada@example.test", "0999999999", "Buyer", null, PreferredContactMethod.Email));
        store.ResetCounts();
        var service = new ContactManagementService(store);

        var result = await service.UpdateAsync(id, new ContactManagementUpdateApplicationRequest(
            " Ada Lovelace ",
            "ADA@EXAMPLE.TEST",
            "0999999999",
            "Buyer",
            null,
            PreferredContactMethod.Email));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task CreateAsync_PropagatesPreferredEmailRule()
    {
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.CreateAsync(new ContactManagementCreateApplicationRequest(
            "Ada Lovelace",
            null,
            null,
            null,
            null,
            PreferredContactMethod.Email));

        Assert.False(result.Success);
        Assert.Equal("PreferredContactMethodRequiresEmail", result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task CreateAsync_PropagatesPreferredPhoneRule()
    {
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.CreateAsync(new ContactManagementCreateApplicationRequest(
            "Ada Lovelace",
            null,
            null,
            null,
            null,
            PreferredContactMethod.Phone));

        Assert.False(result.Success);
        Assert.Equal("PreferredContactMethodRequiresPhone", result.ErrorCode);
        Assert.Equal(0, store.SaveCount);
    }

    [Fact]
    public async Task CreateAsync_SupportsOptionalAccountId()
    {
        var accountId = Guid.NewGuid().ToString("D");
        var store = new CountingContactFoundationStore();
        var service = new ContactManagementService(store);

        var result = await service.CreateAsync(new ContactManagementCreateApplicationRequest(
            "Ada Lovelace",
            "ada@example.test",
            null,
            null,
            accountId,
            PreferredContactMethod.Email));

        Assert.True(result.Success);
        Assert.Equal(accountId, result.Contact!.AccountId);
    }

    [Fact]
    public async Task UpdateAsync_PropagatesCancellationTokenToStore()
    {
        var id = Guid.NewGuid().ToString("D");
        using var source = new CancellationTokenSource();
        var store = new CountingContactFoundationStore();
        await store.SeedAsync(Preview(id, "Ada Lovelace", "ada@example.test", null, null, null, PreferredContactMethod.Email));
        var service = new ContactManagementService(store);

        await service.UpdateAsync(id, new ContactManagementUpdateApplicationRequest(
            "Ada Byron",
            "ada.byron@example.test",
            null,
            null,
            null,
            PreferredContactMethod.Email), source.Token);

        Assert.Equal(source.Token, store.LastGetByIdCancellationToken);
        Assert.Equal(source.Token, store.LastSaveCancellationToken);
    }

    private static CrmFoundationPreviewItemContract Preview(
        string id,
        string name,
        string? email,
        string? phone,
        string? role,
        string? accountId,
        PreferredContactMethod preferredContactMethod) =>
        new(
            id,
            "Contact",
            name,
            "PreviewOnly",
            DateTimeOffset.UtcNow,
            new Dictionary<string, string>
            {
                ["email"] = email ?? string.Empty,
                ["phone"] = phone ?? string.Empty,
                ["role"] = role ?? string.Empty,
                ["accountId"] = accountId ?? string.Empty,
                ["preferredContactMethod"] = preferredContactMethod.ToString()
            });

    private sealed class CountingContactFoundationStore : IContactFoundationStore
    {
        private readonly Dictionary<string, CrmFoundationPreviewItemContract> items = new(StringComparer.OrdinalIgnoreCase);

        public int SaveCount { get; private set; }

        public int GetByIdCount { get; private set; }

        public CancellationToken LastGetByIdCancellationToken { get; private set; }

        public CancellationToken LastSaveCancellationToken { get; private set; }

        public Task SeedAsync(CrmFoundationPreviewItemContract preview)
        {
            items[preview.Id] = preview;
            return Task.CompletedTask;
        }

        public void ResetCounts()
        {
            SaveCount = 0;
            GetByIdCount = 0;
            LastGetByIdCancellationToken = default;
            LastSaveCancellationToken = default;
        }

        public Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyCollection<CrmFoundationPreviewItemContract>>(items.Values.ToArray());

        public Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            GetByIdCount++;
            LastGetByIdCancellationToken = cancellationToken;
            items.TryGetValue(id, out var preview);
            return Task.FromResult(preview);
        }

        public Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            SaveCount++;
            LastSaveCancellationToken = cancellationToken;
            items[preview.Id] = preview;
            return Task.FromResult(preview);
        }

        public Task ClearPreviewAsync(CancellationToken cancellationToken = default)
        {
            items.Clear();
            return Task.CompletedTask;
        }

        public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(new CrmFoundationStoreStatusContract(
                "CountingContactFoundationStore",
                FoundationMode: true,
                FoundationStoreEnabled: true,
                DurablePersistence: false,
                PreviewCount: items.Count,
                "NonProductionSeam",
                "NonProduction",
                "Test store"));
    }
}
