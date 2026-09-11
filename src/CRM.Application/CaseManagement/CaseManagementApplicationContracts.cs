using CRM.Domain.Enums;

namespace CRM.Application.CaseManagement;

public sealed record CaseManagementCreateRequest(
    string? CustomerId,
    string? Title,
    string? Summary,
    CasePriority Priority);

public sealed record CaseManagementUpdateRequest(
    string? CustomerId,
    string? Title,
    string? Summary,
    CasePriority Priority);

public sealed record CaseManagementApplicationCase(
    string Id,
    string CustomerId,
    string Title,
    string Summary,
    CasePriority Priority,
    CaseStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record CaseManagementApplicationResult(
    string? CaseId,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    CaseStatus? Status,
    CaseManagementApplicationCase? Case)
{
    public bool Success => Allowed && ErrorCode == "None";
}
