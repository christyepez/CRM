using CRM.Domain.Enums;

namespace CRM.Domain.SegmentManagement;

public static class SegmentManagementPolicy
{
    public const int MaxNameLength = 160;
    public const int MaxCriteriaSummaryLength = 1000;

    public static SegmentManagementRuleResult Evaluate(SegmentManagementCommand command)
    {
        var id = Normalize(command.SegmentId);
        var name = Normalize(command.Name);
        var criteria = Normalize(command.CriteriaSummary);
        if (!Enum.IsDefined(command.Operation))
            return Reject(command, SegmentManagementErrorCode.InvalidOperation, "Segment operation is invalid.", id, name, criteria);
        if (command.Operation != SegmentManagementOperation.Create)
        {
            if (!ValidId(id)) return Reject(command, SegmentManagementErrorCode.InvalidSegmentId, "Segment id is required.", id, name, criteria);
            if (command.ExistingSegment is null) return Reject(command, SegmentManagementErrorCode.SegmentNotFound, "Existing segment snapshot is required.", id, name, criteria);
            if (!string.Equals(id, Normalize(command.ExistingSegment.SegmentId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, SegmentManagementErrorCode.InvalidSegmentId, "Segment id cannot change.", id, name, criteria, command.ExistingSegment.Status);
        }        if (command.Operation is SegmentManagementOperation.Create or SegmentManagementOperation.Update)
            return EvaluateProfile(command, id, name, criteria);
        return command.Operation switch
        {
            SegmentManagementOperation.Activate => EvaluateActivate(command, id!),
            SegmentManagementOperation.Deactivate => EvaluateDeactivate(command, id!),
            _ => Reject(command, SegmentManagementErrorCode.InvalidOperation, "Segment operation is invalid.", id, name, criteria)
        };
    }

    private static SegmentManagementRuleResult EvaluateProfile(SegmentManagementCommand command,string? id,string? name,string? criteria)
    {
        if (string.IsNullOrWhiteSpace(name)) return Reject(command,SegmentManagementErrorCode.NameRequired,"Segment name is required.",id,name,criteria);
        if (name.Length > MaxNameLength) return Reject(command,SegmentManagementErrorCode.NameTooLong,"Segment name exceeds the allowed length.",id,name,criteria);
        if (string.IsNullOrWhiteSpace(criteria)) return Reject(command,SegmentManagementErrorCode.CriteriaSummaryRequired,"Criteria summary is required.",id,name,criteria);
        if (criteria.Length > MaxCriteriaSummaryLength) return Reject(command,SegmentManagementErrorCode.CriteriaSummaryTooLong,"Criteria summary exceeds the allowed length.",id,name,criteria);
        var status = command.Operation == SegmentManagementOperation.Create ? SegmentStatus.Draft : command.ExistingSegment!.Status;
        var changed = command.Operation == SegmentManagementOperation.Create || HasChanged(command.ExistingSegment!,name,criteria);
        return Result(command,id,name,criteria,status,true,changed,SegmentManagementErrorCode.None,changed?"Segment management operation is valid.":"Segment update has no changes.");
    }    private static SegmentManagementRuleResult EvaluateActivate(SegmentManagementCommand command,string id)
    {
        var existing=command.ExistingSegment!;
        if(existing.Status==SegmentStatus.Active) return FromExisting(command,existing,id,SegmentStatus.Active,true,false,SegmentManagementErrorCode.None,"Segment is already active.");
        if(existing.Status is SegmentStatus.Draft or SegmentStatus.Inactive) return FromExisting(command,existing,id,SegmentStatus.Active,true,true,SegmentManagementErrorCode.None,"Segment activation is valid.");
        return FromExisting(command,existing,id,existing.Status,false,false,SegmentManagementErrorCode.InvalidStatusTransition,"Segment cannot be activated from its current status.");
    }

    private static SegmentManagementRuleResult EvaluateDeactivate(SegmentManagementCommand command,string id)
    {
        var existing=command.ExistingSegment!;
        if(existing.Status==SegmentStatus.Inactive) return FromExisting(command,existing,id,SegmentStatus.Inactive,true,false,SegmentManagementErrorCode.None,"Segment is already inactive.");
        if(existing.Status==SegmentStatus.Active) return FromExisting(command,existing,id,SegmentStatus.Inactive,true,true,SegmentManagementErrorCode.None,"Segment deactivation is valid.");
        return FromExisting(command,existing,id,existing.Status,false,false,SegmentManagementErrorCode.InvalidStatusTransition,"Only active segments can be deactivated.");
    }

    private static bool HasChanged(SegmentManagementSnapshot existing,string name,string criteria)=>
        !string.Equals(name,Normalize(existing.Name),StringComparison.Ordinal) || !string.Equals(criteria,Normalize(existing.CriteriaSummary),StringComparison.Ordinal);

    private static SegmentManagementRuleResult FromExisting(SegmentManagementCommand command,SegmentManagementSnapshot existing,string id,SegmentStatus status,bool allowed,bool changed,SegmentManagementErrorCode code,string message)=>
        new(id,command.Operation,allowed,changed,code,message,Normalize(existing.Name),Normalize(existing.CriteriaSummary),status);    private static SegmentManagementRuleResult Reject(SegmentManagementCommand command,SegmentManagementErrorCode code,string message,string? id,string? name,string? criteria,SegmentStatus status=SegmentStatus.Draft)=>
        Result(command,id,name,criteria,status,false,false,code,message);

    private static SegmentManagementRuleResult Result(SegmentManagementCommand command,string? id,string? name,string? criteria,SegmentStatus status,bool allowed,bool changed,SegmentManagementErrorCode code,string message)=>
        new(id,command.Operation,allowed,changed,code,message,name,criteria,status);

    private static string? Normalize(string? value)
    {
        var normalized=(value??string.Empty).Trim();
        return normalized.Length==0?null:normalized;
    }

    private static bool ValidId(string? value)=>Guid.TryParse(value,out var id)&&id!=Guid.Empty;
}