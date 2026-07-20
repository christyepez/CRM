using CRM.Domain.Foundation;

namespace CRM.Application.Contracts;

public sealed class CrmDomainCatalogService
{
    public CrmDomainCatalogResponse GetCatalog() =>
        new(
            CrmFoundationStatus.Module,
            CrmFoundationStatus.RuntimeMode,
            "Draft",
            CrmFoundationStatus.PortalIntegration,
            CrmFoundationStatus.FinancialIntegration,
            BuildEntities(),
            BuildRelationships(),
            BuildEvents());

    public object GetContracts() => new
    {
        module = CrmFoundationStatus.Module,
        runtimeMode = CrmFoundationStatus.RuntimeMode,
        contractStatus = "Draft",
        portalIntegration = CrmFoundationStatus.PortalIntegration,
        financialIntegration = CrmFoundationStatus.FinancialIntegration,
        apiVersion = "v1-draft",
        endpoints = new[]
        {
            "GET /api/crm/readiness",
            "GET /api/crm/domain-catalog",
            "GET /api/crm/contracts",
            "GET /api/crm/integration-boundaries"
        }
    };

    public object GetIntegrationBoundaries() => new
    {
        module = CrmFoundationStatus.Module,
        runtimeMode = CrmFoundationStatus.RuntimeMode,
        contractStatus = "Draft",
        portalIntegration = CrmFoundationStatus.PortalIntegration,
        financialIntegration = CrmFoundationStatus.FinancialIntegration,
        boundaries = new[]
        {
            "Portal owns security, menu, audit, notification, configuration and gateway capabilities.",
            "Financiero integration is planned and must remain event/contract based.",
            "CRM owns customer relationship domain concepts only."
        }
    };

    private static IReadOnlyCollection<CrmEntityContract> BuildEntities() =>
    [
        new("Lead", "Potential customer before qualification.", ["Id", "Name", "Email", "Status"], ["Qualify"]),
        new("Account", "Organization or customer account container.", ["Id", "CompanyName"], ["ReadContract"]),
        new("Contact", "Person related to an account or lead.", ["Id", "PersonName", "Email"], ["ReadContract"]),
        new("Opportunity", "Potential revenue tracked through pipeline.", ["Id", "AccountName", "ExpectedValue", "Status"], ["MarkWon"]),
        new("Pipeline", "Sales process grouping ordered stages.", ["Id", "Name"], ["ReadContract"]),
        new("PipelineStage", "Ordered stage within a pipeline.", ["Id", "Name", "Order"], ["ReadContract"]),
        new("Activity", "Scheduled commercial follow-up.", ["Id", "Type", "Subject", "Status"], ["Complete"]),
        new("Note", "Internal note linked to a CRM entity.", ["Id", "RelatedEntityId"], ["ReadContract"]),
        new("Campaign", "Marketing campaign concept.", ["Id", "Name", "ActiveRange"], ["ReadContract"]),
        new("Segment", "Audience or customer segmentation concept.", ["Id", "Name"], ["ReadContract"])
    ];

    private static IReadOnlyCollection<CrmRelationshipContract> BuildRelationships() =>
    [
        new("Lead", "Opportunity", "A qualified lead may become an opportunity.", "Draft"),
        new("Account", "Contact", "An account may have many contacts.", "Draft"),
        new("Pipeline", "PipelineStage", "A pipeline contains ordered stages.", "Draft"),
        new("Opportunity", "Activity", "An opportunity may have follow-up activities.", "Draft")
    ];

    private static IReadOnlyCollection<CrmEventContract> BuildEvents() =>
    [
        new("LeadCreatedDomainEvent", "Lead conceptual creation.", "Internal domain event; audit/outbox delegated to Portal later."),
        new("OpportunityWonDomainEvent", "Opportunity marked as won.", "Future integration event candidate."),
        new("CustomerConvertedDomainEvent", "Lead/customer conversion accepted.", "Future Portal/Financiero boundary candidate."),
        new("FollowUpScheduledDomainEvent", "Follow-up activity scheduled.", "Future notification adapter candidate.")
    ];
}
