using CRM.Domain.Enums;

namespace CRM.Domain.AccountManagement;

public sealed record AccountManagementCommand(
    AccountManagementOperation Operation,
    string? AccountId,
    string? Name,
    string? TaxId,
    string? Industry,
    string? Segment,
    AccountManagementSnapshot? ExistingAccount = null);

public sealed record AccountManagementSnapshot(
    string AccountId,
    string Name,
    string? TaxId,
    string? Industry,
    string? Segment,
    AccountStatus Status);
