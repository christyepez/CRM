using CRM.Domain.NoteManagement;

namespace CRM.Application.Ports.Persistence;

public sealed record NoteFoundationRecord(
    string Id,
    NoteRelatedEntityType RelatedEntityType,
    string RelatedEntityId,
    string Text,
    NoteStatus Status);

/// <summary>NonProductionPersistenceSeam: Note synthetic foundation storage only.</summary>
public interface INoteFoundationStore
{
    Task<IReadOnlyCollection<NoteFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NoteFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<NoteFoundationRecord> SaveAsync(NoteFoundationRecord item, CancellationToken cancellationToken = default);
}
