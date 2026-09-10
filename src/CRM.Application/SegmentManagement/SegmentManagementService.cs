using CRM.Application.Ports.Persistence;
using CRM.Domain.SegmentManagement;

namespace CRM.Application.SegmentManagement;

public sealed class SegmentManagementService(ISegmentFoundationStore store) : ISegmentManagementService
{
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<IReadOnlyCollection<SegmentManagementApplicationSegment>> GetAllAsync(CancellationToken ct=default) =>
        (await store.GetAllAsync(ct)).Select(ToApplication).ToArray();

    public async Task<SegmentManagementApplicationSegment?> GetByIdAsync(string id,CancellationToken ct=default)
    {
        var x=await store.GetByIdAsync(id,ct); return x is null?null:ToApplication(x);
    }

    public async Task<SegmentManagementApplicationResult> CreateAsync(SegmentManagementCreateRequest request,CancellationToken ct=default)
    {
        var e=SegmentManagementPolicy.Evaluate(new(SegmentManagementOperation.Create,null,request.Name,request.CriteriaSummary));
        if(!e.Success) return Result(e,null);
        var saved=await store.SaveAsync(new(Guid.NewGuid().ToString(),e.NormalizedName!,e.NormalizedCriteriaSummary!,e.ResultStatus),ct);
        return Result(e with { SegmentId=saved.Id },ToApplication(saved));
    }
    public async Task<SegmentManagementApplicationResult> UpdateAsync(string id,SegmentManagementUpdateRequest request,CancellationToken ct=default)
    {
        var existing=await store.GetByIdAsync(id,ct); if(existing is null) return NotFound(SegmentManagementOperation.Update,id);
        var e=SegmentManagementPolicy.Evaluate(new(SegmentManagementOperation.Update,id,request.Name,request.CriteriaSummary,Snapshot(existing)));
        if(!e.Success) return Result(e,null); if(!e.Changed) return Result(e,ToApplication(existing));
        var saved=await store.SaveAsync(new(id,e.NormalizedName!,e.NormalizedCriteriaSummary!,e.ResultStatus),ct);
        return Result(e,ToApplication(saved));
    }

    public Task<SegmentManagementApplicationResult> ActivateAsync(string id,CancellationToken ct=default)=>ChangeStatusAsync(id,SegmentManagementOperation.Activate,ct);
    public Task<SegmentManagementApplicationResult> DeactivateAsync(string id,CancellationToken ct=default)=>ChangeStatusAsync(id,SegmentManagementOperation.Deactivate,ct);

    private async Task<SegmentManagementApplicationResult> ChangeStatusAsync(string id,SegmentManagementOperation operation,CancellationToken ct)
    {
        var existing=await store.GetByIdAsync(id,ct); if(existing is null) return NotFound(operation,id);
        var e=SegmentManagementPolicy.Evaluate(new(operation,id,existing.Name,existing.CriteriaSummary,Snapshot(existing)));
        if(!e.Success) return Result(e,null); if(!e.Changed) return Result(e,ToApplication(existing));
        var saved=await store.SaveAsync(new(id,e.NormalizedName!,e.NormalizedCriteriaSummary!,e.ResultStatus),ct);
        return Result(e,ToApplication(saved));
    }
    private static SegmentManagementSnapshot Snapshot(SegmentFoundationRecord x)=>new(x.Id,x.Name,x.CriteriaSummary,x.Status);
    private static SegmentManagementApplicationSegment ToApplication(SegmentFoundationRecord x)=>new(x.Id,x.Name,x.CriteriaSummary,x.Status,PersistenceMode,false);
    private static SegmentManagementApplicationResult Result(SegmentManagementRuleResult e,SegmentManagementApplicationSegment? segment)=>
        new(segment?.Id??e.SegmentId,e.Operation.ToString(),e.Allowed,e.Changed,e.ErrorCode.ToString(),e.Message,segment?.Status??e.ResultStatus,segment);
    private static SegmentManagementApplicationResult NotFound(SegmentManagementOperation operation,string id)=>
        new(id,operation.ToString(),false,false,SegmentManagementErrorCode.SegmentNotFound.ToString(),"Segment was not found.",null,null);
}