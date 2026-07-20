namespace CRM.Application.Contracts;

public sealed record CrmReadinessResponse(
    string Module,
    string Status,
    string PortalIntegration,
    string FinancialIntegration,
    string RuntimeMode,
    string ContractStatus);

public sealed record CrmDomainCatalogResponse(
    string Module,
    string RuntimeMode,
    string ContractStatus,
    string PortalIntegration,
    string FinancialIntegration,
    IReadOnlyCollection<CrmEntityContract> Entities,
    IReadOnlyCollection<CrmRelationshipContract> Relationships,
    IReadOnlyCollection<CrmEventContract> Events);

public sealed record CrmEntityContract(
    string Name,
    string Responsibility,
    IReadOnlyCollection<string> KeyFields,
    IReadOnlyCollection<string> AllowedOperations);

public sealed record CrmRelationshipContract(
    string Source,
    string Target,
    string Relationship,
    string Status);

public sealed record CrmEventContract(
    string Name,
    string Trigger,
    string IntegrationPolicy);
