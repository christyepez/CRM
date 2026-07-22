using CRM.Application.Ports.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryAccountFoundationStore : InMemoryFoundationStoreBase, IAccountFoundationStore
{
    public InMemoryAccountFoundationStore()
        : base("AccountFoundationStore")
    {
        AddSeed("account-preview-001", "Account", "Contoso Preview", "PreviewOnly", new Dictionary<string, string>
        {
            ["segment"] = "FoundationSeed",
            ["warning"] = "NonProductionSeam"
        });
    }
}
