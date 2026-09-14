using CRM.Domain.Enums;

namespace CRM.Application.Ports.Persistence;

public sealed record CaseFoundationRecord(
    string Id,
    string CustomerId,
    string Title,
    string Summary,
    CasePriority Priority,
    CaseStatus Status);

/// <summary>NonProductionPersistenceSeam: Case synthetic foundation storage only.</summary>
public interface ICaseFoundationStore
{
    Task<IReadOnlyCollection<CaseFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CaseFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<CaseFoundationRecord> SaveAsync(CaseFoundationRecord item, CancellationToken cancellationToken = default);
}
