namespace CRM.Application.SegmentManagement;

public interface ISegmentManagementService
{
    Task<IReadOnlyCollection<SegmentManagementApplicationSegment>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<SegmentManagementApplicationSegment?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
    Task<SegmentManagementApplicationResult> CreateAsync(SegmentManagementCreateRequest request,CancellationToken cancellationToken=default);
    Task<SegmentManagementApplicationResult> UpdateAsync(string id,SegmentManagementUpdateRequest request,CancellationToken cancellationToken=default);
    Task<SegmentManagementApplicationResult> ActivateAsync(string id,CancellationToken cancellationToken=default);
    Task<SegmentManagementApplicationResult> DeactivateAsync(string id,CancellationToken cancellationToken=default);
}