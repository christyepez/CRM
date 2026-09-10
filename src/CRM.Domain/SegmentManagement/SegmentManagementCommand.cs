using CRM.Domain.Enums;
namespace CRM.Domain.SegmentManagement;
public sealed record SegmentManagementCommand(SegmentManagementOperation Operation,string? SegmentId,string? Name,string? CriteriaSummary,SegmentManagementSnapshot? ExistingSegment=null);
public sealed record SegmentManagementSnapshot(string SegmentId,string Name,string CriteriaSummary,SegmentStatus Status);
