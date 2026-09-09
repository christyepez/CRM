using CRM.Application.Ports.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryCampaignFoundationStore : InMemoryFoundationStoreBase, ICampaignFoundationStore
{
    public InMemoryCampaignFoundationStore() : base("CampaignFoundationStore")
    {
        AddSeed("44444444-4444-4444-4444-444444444444", "Campaign", "Foundation Campaign Preview", "Draft",
            new Dictionary<string, string>
            {
                ["startDate"] = "2026-09-01",
                ["endDate"] = "2026-09-30",
                ["warning"] = "NonProductionSeam"
            });
    }
}