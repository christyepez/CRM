namespace CRM.Application.Financial;

public sealed record CustomerConvertedForFinancialIntegration(string Name, string Status, string IntegrationMode);

public sealed record OpportunityWonForFinancialIntegration(string Name, string Status, string IntegrationMode);

public sealed record InvoiceCreatedFinancialSignal(string Name, string Status, string IntegrationMode);

public sealed record PaymentOverdueFinancialSignal(string Name, string Status, string IntegrationMode);

public sealed record CollectionsRiskRaisedFinancialSignal(string Name, string Status, string IntegrationMode);
