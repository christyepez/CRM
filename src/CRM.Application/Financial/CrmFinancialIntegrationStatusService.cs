namespace CRM.Application.Financial;

public sealed class CrmFinancialIntegrationStatusService
{
    public const string WarningText = "Financial integration contracts only; no runtime calls configured";

    private static readonly string[] Capabilities =
    [
        "CustomerReference",
        "AccountStatus",
        "InvoiceSummary",
        "PaymentStatus",
        "CollectionsSignal",
        "FinancialEvents"
    ];

    private static readonly string[] Patterns =
    [
        "API",
        "Events",
        "NoSharedDatabase"
    ];

    public CrmFinancialIntegrationStatusResponse GetStatus() =>
        new(
            "CRM",
            "FinancialIntegrationPlanned",
            "Planned",
            "NonProduction",
            "Financiero",
            false,
            WarningText,
            Capabilities,
            Patterns,
            true);

    public object GetContracts() => new
    {
        foundationMode = true,
        module = "CRM",
        status = "FinancialIntegrationPlanned",
        integrationMode = "Planned",
        runtimeMode = "NonProduction",
        capabilityOwner = "Financiero",
        connected = false,
        warning = WarningText,
        contracts = new[]
        {
            nameof(FinancialCustomerReferenceContract),
            nameof(FinancialAccountStatusContract),
            nameof(FinancialInvoiceSummaryContract),
            nameof(FinancialPaymentStatusContract),
            nameof(FinancialCollectionsSignalContract),
            nameof(FinancialIntegrationEventContract)
        },
        requiredCapabilities = Capabilities,
        integrationPatterns = Patterns
    };

    public object GetRequiredCapabilities() => new
    {
        foundationMode = true,
        module = "CRM",
        status = "FinancialIntegrationPlanned",
        integrationMode = "Planned",
        runtimeMode = "NonProduction",
        capabilityOwner = "Financiero",
        connected = false,
        warning = WarningText,
        requiredCapabilities = Capabilities.Select(capability => new
        {
            capability,
            owner = "Financiero",
            crmPolicy = "External"
        }).ToArray(),
        integrationPatterns = Patterns
    };

    public object GetConceptualEvents() => new
    {
        foundationMode = true,
        module = "CRM",
        status = "FinancialIntegrationPlanned",
        integrationMode = "Planned",
        runtimeMode = "NonProduction",
        capabilityOwner = "Financiero",
        connected = false,
        warning = WarningText,
        integrationPatterns = Patterns,
        events = new[]
        {
            nameof(CustomerConvertedForFinancialIntegration),
            nameof(OpportunityWonForFinancialIntegration),
            nameof(InvoiceCreatedFinancialSignal),
            nameof(PaymentOverdueFinancialSignal),
            nameof(CollectionsRiskRaisedFinancialSignal)
        }
    };
}
