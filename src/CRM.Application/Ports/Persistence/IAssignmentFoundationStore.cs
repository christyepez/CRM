using CRM.Domain.AssignmentManagement;

namespace CRM.Application.Ports.Persistence;

public sealed record AssignmentFoundationRecord(
    string Id,
    AssignmentRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string AssigneeReferenceId,
    string? AssignmentLabel,
    AssignmentStatus Status);

/// <summary>NonProductionPersistenceSeam: synthetic CRM assignment references only.</summary>
public interface IAssignmentFoundationStore
{
    Task<IReadOnlyCollection<AssignmentFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AssignmentFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<AssignmentFoundationRecord> SaveAsync(AssignmentFoundationRecord item, CancellationToken cancellationToken = default);
}
