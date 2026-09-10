using CRM.Application.SegmentManagement;
using CRM.Domain.SegmentManagement;

namespace CRM.Api.Foundation;

public sealed record FoundationSegmentManagementCreateRequest(string? Name, string? CriteriaSummary);
public sealed record FoundationSegmentManagementUpdateRequest(string? Name, string? CriteriaSummary);

public sealed record SegmentManagementApiResponse(
    string? Id,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    SegmentManagementApplicationSegment? Segment,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    string Warning)
{
    public static SegmentManagementApiResponse From(SegmentManagementApplicationResult result) =>
        new(result.SegmentId, result.Operation, result.Allowed, result.Changed, result.ErrorCode, result.Message,
            result.Segment, true, result.Segment?.PersistenceMode ?? "NonProductionSeam", false, false, false, false,
            "Foundation Segment API only; productive route remains locked");

    public static int ToStatusCode(SegmentManagementApplicationResult result) => result.ErrorCode switch
    {
        nameof(SegmentManagementErrorCode.None) => StatusCodes.Status200OK,
        nameof(SegmentManagementErrorCode.SegmentNotFound) => StatusCodes.Status404NotFound,
        nameof(SegmentManagementErrorCode.InvalidStatusTransition) => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };

    public static SegmentManagementCreateRequest ToApplication(FoundationSegmentManagementCreateRequest request) =>
        new(request.Name, request.CriteriaSummary);

    public static SegmentManagementUpdateRequest ToApplication(FoundationSegmentManagementUpdateRequest request) =>
        new(request.Name, request.CriteriaSummary);
}
