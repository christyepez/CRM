using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryLeadFoundationStore : InMemoryFoundationStoreBase, ILeadFoundationStore
{
    public InMemoryLeadFoundationStore()
        : base("LeadFoundationStore")
    {
        AddSeed("lead-preview-001", "Lead", "Ada Preview", "PreviewOnly", new Dictionary<string, string>
        {
            ["source"] = "FoundationSeed",
            ["warning"] = "NonProductionSeam"
        });
    }
}
