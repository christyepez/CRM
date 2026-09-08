using CRM.Application.Ports.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryContactFoundationStore : InMemoryFoundationStoreBase, IContactFoundationStore
{
    public InMemoryContactFoundationStore()
        : base("ContactFoundationStore")
    {
        AddSeed("contact-preview-001", "Contact", "Grace Preview", "PreviewOnly", new Dictionary<string, string>
        {
            ["preferredContactMethod"] = "Email",
            ["warning"] = "NonProductionSeam"
        });
        AddSeed("33333333-3333-3333-3333-333333333333", "Contact", "Activity Contact Target Preview", "PreviewOnly", new Dictionary<string, string>
        {
            ["preferredContactMethod"] = "Email",
            ["warning"] = "NonProductionSeam",
            ["usage"] = "ActivityFoundationTargetValidation"
        });
    }
}
