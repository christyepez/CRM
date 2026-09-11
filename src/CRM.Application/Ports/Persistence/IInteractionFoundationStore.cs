using CRM.Domain.InteractionManagement;

namespace CRM.Application.Ports.Persistence;

public sealed record InteractionFoundationRecord(
    string Id,
    InteractionRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    InteractionChannel Channel,
    InteractionDirection Direction,
    string Subject,
    string Summary,
    DateTimeOffset OccurredAtUtc,
    InteractionStatus Status);

/// <summary>NonProductionPersistenceSeam: Interaction synthetic foundation storage only.</summary>
public interface IInteractionFoundationStore
{
    Task<IReadOnlyCollection<InteractionFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InteractionFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<InteractionFoundationRecord> SaveAsync(InteractionFoundationRecord item, CancellationToken cancellationToken = default);
}
