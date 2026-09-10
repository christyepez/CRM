using CRM.Domain.Enums;
namespace CRM.Domain.SegmentManagement;
public sealed record SegmentManagementRuleResult(string? SegmentId,SegmentManagementOperation Operation,bool Allowed,bool Changed,SegmentManagementErrorCode ErrorCode,string Message,string? NormalizedName,string? NormalizedCriteriaSummary,SegmentStatus ResultStatus){public bool Success=>Allowed&&ErrorCode==SegmentManagementErrorCode.None;}
