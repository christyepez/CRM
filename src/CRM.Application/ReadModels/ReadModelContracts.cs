using CRM.Domain.Enums;

namespace CRM.Application.ReadModels;

public sealed record LeadReadModel(
    string Id,
    string DisplayName,
    string Email,
    string? CompanyName,
    LeadStatus Status);

public sealed record AccountReadModel(
    string Id,
    string CompanyName,
    string? Industry,
    string? Segment,
    AccountStatus Status);

public sealed record ContactReadModel(
    string Id,
    string DisplayName,
    string? Email,
    string? Phone,
    string? Role,
    ContactStatus Status);

public sealed record CrmReadModelQuery(
    string? Search,
    int Page = 1,
    int PageSize = 25);

public sealed record CrmReadModelResponse<T>(
    string Module,
    bool FoundationMode,
    string Source,
    string Persistence,
    string RuntimeMode,
    string Warning,
    IReadOnlyCollection<T> Items,
    int TotalCount);

public sealed record CrmReadModelStatusResponse(
    string Module,
    bool FoundationMode,
    string Source,
    string Persistence,
    string RuntimeMode,
    string Warning,
    string StrategyStatus,
    IReadOnlyCollection<string> ActivationCriteria);
