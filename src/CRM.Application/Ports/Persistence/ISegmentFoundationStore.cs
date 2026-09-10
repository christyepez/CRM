using CRM.Domain.Enums;

namespace CRM.Application.Ports.Persistence;

public sealed record SegmentFoundationRecord(
    string Id,
    string Name,
    string CriteriaSummary,
    SegmentStatus Status);

/// <summary>NonProductionPersistenceSeam: Segment synthetic foundation storage only.</summary>
public interface ISegmentFoundationStore
{
    Task<IReadOnlyCollection<SegmentFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<SegmentFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
    Task<SegmentFoundationRecord> SaveAsync(SegmentFoundationRecord segment,CancellationToken cancellationToken=default);
}