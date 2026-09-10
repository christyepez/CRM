using CRM.Domain.Enums;

namespace CRM.Application.SegmentManagement;

public sealed record SegmentManagementCreateRequest(string? Name, string? CriteriaSummary);
public sealed record SegmentManagementUpdateRequest(string? Name, string? CriteriaSummary);

public sealed record SegmentManagementApplicationSegment(
    string Id,
    string Name,
    string CriteriaSummary,
    SegmentStatus Status,
    string PersistenceMode,
    bool ProductiveCrudEnabled);

public sealed record SegmentManagementApplicationResult(
    string? SegmentId,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    SegmentStatus? Status,
    SegmentManagementApplicationSegment? Segment)
{
    public bool Success => Allowed && ErrorCode == "None";
}