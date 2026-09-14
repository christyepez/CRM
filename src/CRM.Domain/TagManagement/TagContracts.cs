namespace CRM.Domain.TagManagement;
public sealed record TagSnapshot(string TagId,string Name,string? Description,TagStatus Status);
public sealed record TagAssignment(TagRelatedEntityType RelatedEntityType,string RelatedEntityId);
public sealed record TagCommand(TagOperation Operation,string? TagId,string? Name,string? Description,TagAssignment? Assignment=null,TagSnapshot? ExistingTag=null);
public sealed record TagRuleResult(string? TagId,TagOperation Operation,bool Allowed,bool Changed,TagErrorCode ErrorCode,string Message,string? NormalizedName,string? NormalizedDescription,TagAssignment? Assignment,TagStatus ResultStatus){public bool Success=>Allowed&&ErrorCode==TagErrorCode.None;}
