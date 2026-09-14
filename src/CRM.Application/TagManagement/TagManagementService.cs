using CRM.Application.Ports.Persistence; using CRM.Domain.TagManagement;
namespace CRM.Application.TagManagement;
public sealed class TagManagementService(ITagFoundationStore store):ITagManagementService
{
 const string Mode="NonProductionSeam";
 public async Task<IReadOnlyCollection<TagApplicationItem>> GetAllAsync(CancellationToken ct=default)=>(await store.GetAllAsync(ct)).Select(Map).ToArray();
 public async Task<TagApplicationItem?> GetByIdAsync(string id,CancellationToken ct=default){var x=await store.GetByIdAsync(id,ct);return x is null?null:Map(x);}
 public async Task<TagApplicationResult> CreateAsync(TagCreateRequest r,CancellationToken ct=default){var e=TagPolicy.Evaluate(new(TagOperation.Create,null,r.Name,r.Description,A(r.RelatedEntityType,r.RelatedEntityId)));if(!e.Success)return Result(e,null);var s=await store.SaveAsync(new(Guid.NewGuid().ToString("D"),e.NormalizedName!,e.NormalizedDescription,e.ResultStatus,e.Assignment?.RelatedEntityType,e.Assignment?.RelatedEntityId),ct);return Result(e with{TagId=s.Id},Map(s));}
 public async Task<TagApplicationResult> UpdateAsync(string id,TagUpdateRequest r,CancellationToken ct=default){var x=await store.GetByIdAsync(id,ct);if(x is null)return NotFound(TagOperation.Update,id);var e=TagPolicy.Evaluate(new(TagOperation.Update,id,r.Name,r.Description,A(r.RelatedEntityType,r.RelatedEntityId),Snap(x)));if(!e.Success)return Result(e,null);if(!e.Changed)return Result(e,Map(x));var s=await store.SaveAsync(new(id,e.NormalizedName!,e.NormalizedDescription,e.ResultStatus,e.Assignment?.RelatedEntityType,e.Assignment?.RelatedEntityId),ct);return Result(e,Map(s));}
 public async Task<TagApplicationResult> ArchiveAsync(string id,CancellationToken ct=default){var x=await store.GetByIdAsync(id,ct);if(x is null)return NotFound(TagOperation.Archive,id);var e=TagPolicy.Evaluate(new(TagOperation.Archive,id,x.Name,x.Description,A(x.RelatedEntityType,x.RelatedEntityId),Snap(x)));if(!e.Success)return Result(e,null);if(!e.Changed)return Result(e,Map(x));var s=await store.SaveAsync(x with{Status=TagStatus.Archived},ct);return Result(e,Map(s));}
 static TagAssignment? A(TagRelatedEntityType? t,string? id)=>t is null&&string.IsNullOrWhiteSpace(id)?null:new(t??(TagRelatedEntityType)99,id??string.Empty);
 static TagSnapshot Snap(TagFoundationRecord x)=>new(x.Id,x.Name,x.Description,x.Status,A(x.RelatedEntityType,x.RelatedEntityId));
 static TagApplicationItem Map(TagFoundationRecord x)=>new(x.Id,x.Name,x.Description,x.Status,x.RelatedEntityType,x.RelatedEntityId,Mode,false,false);
 static TagApplicationResult Result(TagRuleResult e,TagApplicationItem? x)=>new(x?.Id??e.TagId,e.Operation.ToString(),e.Allowed,e.Changed,e.ErrorCode.ToString(),e.Message,x?.Status??e.ResultStatus,x);
 static TagApplicationResult NotFound(TagOperation op,string id)=>new(id,op.ToString(),false,false,TagErrorCode.TagNotFound.ToString(),"Tag was not found.",null,null);
}
