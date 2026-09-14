namespace CRM.Domain.TagManagement;
public static class TagPolicy
{
 public const int MaxNameLength=80; public const int MaxDescriptionLength=500;
 public static TagRuleResult Evaluate(TagCommand c)
 {
  var id=N(c.TagId);var name=N(c.Name);var desc=N(c.Description);
  if(!Enum.IsDefined(c.Operation))return R(c,TagErrorCode.InvalidOperation,"Invalid operation.",id,name,desc);
  if(c.Operation!=TagOperation.Create){if(!ValidId(id))return R(c,TagErrorCode.InvalidTagId,"Tag id must be a non-empty GUID.",id,name,desc);if(c.ExistingTag is null)return R(c,TagErrorCode.TagNotFound,"Tag was not found.",id,name,desc);if(!string.Equals(c.ExistingTag.TagId,id,StringComparison.OrdinalIgnoreCase))return R(c,TagErrorCode.InvalidTagId,"Tag id mismatch.",id,name,desc);if(!Enum.IsDefined(c.ExistingTag.Status))return R(c,TagErrorCode.InvalidStatus,"Invalid status.",id,name,desc);if(c.Operation==TagOperation.Update&&c.ExistingTag.Status==TagStatus.Archived)return R(c,TagErrorCode.ArchivedTagCannotBeModified,"Archived tags are read-only.",id,name,desc,TagStatus.Archived);}
  if(c.Operation==TagOperation.Archive){var archiveChanged=c.ExistingTag!.Status!=TagStatus.Archived;return new(id,c.Operation,true,archiveChanged,TagErrorCode.None,archiveChanged?"Tag archived.":"No changes were necessary.",c.ExistingTag.Name,c.ExistingTag.Description,c.Assignment,TagStatus.Archived);}
  if(name is null)return R(c,TagErrorCode.NameRequired,"Tag name is required.",id,name,desc);
  if(name.Length>MaxNameLength)return R(c,TagErrorCode.NameTooLong,"Tag name is too long.",id,name,desc);
  if(desc?.Length>MaxDescriptionLength)return R(c,TagErrorCode.DescriptionTooLong,"Description is too long.",id,name,desc);
  if(c.Assignment is not null){if(!Enum.IsDefined(c.Assignment.RelatedEntityType))return R(c,TagErrorCode.InvalidRelatedEntityType,"Invalid related entity type.",id,name,desc);var rid=N(c.Assignment.RelatedEntityId);if(rid is null)return R(c,TagErrorCode.RelatedEntityIdRequired,"Related entity id is required.",id,name,desc);if(!ValidId(rid))return R(c,TagErrorCode.InvalidRelatedEntityId,"Related entity id must be a non-empty GUID.",id,name,desc);c=c with{Assignment=new(c.Assignment.RelatedEntityType,rid)};}
  var changed=c.Operation==TagOperation.Create||c.ExistingTag!.Name!=name||c.ExistingTag.Description!=desc||!Equals(c.ExistingTag.Assignment,c.Assignment);
  return new(id,c.Operation,true,changed,TagErrorCode.None,changed?"Tag accepted.":"No changes were necessary.",name,desc,c.Assignment,TagStatus.Active);
 }
 static TagRuleResult R(TagCommand c,TagErrorCode e,string m,string? id,string? n,string? d,TagStatus s=TagStatus.Active)=>new(id,c.Operation,false,false,e,m,n,d,c.Assignment,s);
 static string? N(string? v){var x=(v??string.Empty).Trim();return x.Length==0?null:x;}
 static bool ValidId(string? v)=>Guid.TryParse(v,out var g)&&g!=Guid.Empty;
}
