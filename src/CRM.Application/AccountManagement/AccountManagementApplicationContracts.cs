using CRM.Domain.Enums;

namespace CRM.Application.AccountManagement;

public sealed record AccountManagementCreateRequest(string? Name,string? TaxId,string? Industry,string? Segment);
public sealed record AccountManagementUpdateRequest(string? Name,string? TaxId,string? Industry,string? Segment);

public sealed record AccountManagementApplicationAccount(
    string Id,
    string Name,
    string? TaxId,
    string? Industry,
    string? Segment,
    AccountStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record AccountManagementApplicationResult(
    string? AccountId,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    AccountStatus? Status,
    AccountManagementApplicationAccount? Account)
{
    public bool Success => Allowed && ErrorCode == "None";
}
