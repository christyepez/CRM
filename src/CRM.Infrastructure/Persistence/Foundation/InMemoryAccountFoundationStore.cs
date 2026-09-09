using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryAccountFoundationStore : InMemoryFoundationStoreBase, IAccountFoundationStore
{
    private readonly List<AccountFoundationRecord> accounts = [];
    private readonly object accountSync = new();

    public InMemoryAccountFoundationStore() : base("AccountFoundationStore")
    {
        var seed = new AccountFoundationRecord(
            "aaaaaaaa-1111-1111-1111-111111111111",
            "Contoso Foundation",
            "EC-179001",
            "Technology",
            "Enterprise",
            AccountStatus.Draft);
        accounts.Add(seed);

        AddSeed(seed.Id,"Account",seed.Name,seed.Status.ToString(),new Dictionary<string,string>
        {
            ["taxId"] = seed.TaxId ?? "",
            ["industry"] = seed.Industry ?? "",
            ["segment"] = seed.Segment ?? "",
            ["warning"] = "NonProductionSeam"
        });
    }

    public Task<IReadOnlyCollection<AccountFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock(accountSync) return Task.FromResult<IReadOnlyCollection<AccountFoundationRecord>>(accounts.ToArray());
    }

    public Task<AccountFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock(accountSync) return Task.FromResult(accounts.FirstOrDefault(x=>string.Equals(x.Id,id,StringComparison.OrdinalIgnoreCase)));
    }

    public Task<AccountFoundationRecord> SaveAsync(AccountFoundationRecord account,CancellationToken cancellationToken=default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock(accountSync)
        {
            accounts.RemoveAll(x=>string.Equals(x.Id,account.Id,StringComparison.OrdinalIgnoreCase));
            accounts.Add(account);
        }
        return Task.FromResult(account);
    }
}
