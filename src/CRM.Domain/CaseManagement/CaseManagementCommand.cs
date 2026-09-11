using CRM.Domain.Enums;

namespace CRM.Domain.CaseManagement;

public sealed record CaseManagementCommand(
    CaseManagementOperation Operation,
    string? CaseId,
    string? CustomerId,
    string? Title,
    string? Summary,
    CasePriority Priority,
    CaseManagementSnapshot? ExistingCase = null);

public sealed record CaseManagementSnapshot(
    string CaseId,
    string CustomerId,
    string Title,
    string Summary,
    CasePriority Priority,
    CaseStatus Status);
