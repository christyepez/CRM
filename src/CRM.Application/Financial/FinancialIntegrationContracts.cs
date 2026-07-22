namespace CRM.Application.Financial;

public sealed record FinancialCustomerReferenceContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns);

public sealed record FinancialAccountStatusContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns);

public sealed record FinancialInvoiceSummaryContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns);

public sealed record FinancialPaymentStatusContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns);

public sealed record FinancialCollectionsSignalContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns);

public sealed record FinancialIntegrationEventContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns);

public sealed record CrmFinancialIntegrationStatusResponse(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> IntegrationPatterns,
    bool FoundationMode);
