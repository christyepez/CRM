using CRM.Application.CaseManagement;
using CRM.Domain.CaseManagement;
using CRM.Domain.Enums;

namespace CRM.Api.Foundation;

public sealed record FoundationCaseManagementCreateRequest(
    string? CustomerId,
    string? Title,
    string? Summary,
    CasePriority Priority);

public sealed record FoundationCaseManagementUpdateRequest(
    string? CustomerId,
    string? Title,
    string? Summary,
    CasePriority Priority);

public sealed record CaseManagementApiCase(
    string Id,
    string CustomerId,
    string Title,
    string Summary,
    CasePriority Priority,
    CaseStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record CaseManagementApiResponse(
    string? Id,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    CaseStatus? Status,
    CaseManagementApiCase? Case,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    string Warning)
{
    public static CaseManagementApiResponse From(CaseManagementApplicationResult result) =>
        new(result.CaseId, result.Operation, result.Allowed, result.Changed, result.ErrorCode, result.Message,
            result.Status, result.Case is null ? null : new(result.Case.Id, result.Case.CustomerId, result.Case.Title,
            result.Case.Summary, result.Case.Priority, result.Case.Status, result.Case.PersistenceMode, result.Case.ProductiveCrudEnabled),
            true, result.Case?.PersistenceMode ?? "NonProductionSeam", false, false, false, false,
            "Foundation Case API only; productive route remains locked");

    public static int ToStatusCode(CaseManagementApplicationResult result) => result.ErrorCode switch
    {
        nameof(CaseManagementErrorCode.None) => StatusCodes.Status200OK,
        nameof(CaseManagementErrorCode.CaseNotFound) => StatusCodes.Status404NotFound,
        nameof(CaseManagementErrorCode.InvalidStatusTransition) => StatusCodes.Status409Conflict,
        nameof(CaseManagementErrorCode.ResolvedCaseCannotBeModified) => StatusCodes.Status409Conflict,
        nameof(CaseManagementErrorCode.ClosedCaseCannotBeModified) => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };

    public static CaseManagementCreateRequest ToApplication(FoundationCaseManagementCreateRequest request) =>
        new(request.CustomerId, request.Title, request.Summary, request.Priority);

    public static CaseManagementUpdateRequest ToApplication(FoundationCaseManagementUpdateRequest request) =>
        new(request.CustomerId, request.Title, request.Summary, request.Priority);
}
