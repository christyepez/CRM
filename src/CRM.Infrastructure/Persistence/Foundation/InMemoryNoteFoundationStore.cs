using CRM.Application.Ports.Persistence;
using CRM.Domain.NoteManagement;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryNoteFoundationStore : INoteFoundationStore
{
    private readonly List<NoteFoundationRecord> notes =
    [
        new(
            "77777777-7777-7777-7777-777777777777",
            NoteRelatedEntityType.Contact,
            "11111111-1111-1111-1111-111111111111",
            "Synthetic CRM note for foundation validation.",
            NoteStatus.Active)
    ];

    private readonly object sync = new();

    public Task<IReadOnlyCollection<NoteFoundationRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
            return Task.FromResult<IReadOnlyCollection<NoteFoundationRecord>>(notes.ToArray());
    }
    public Task<NoteFoundationRecord?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
            return Task.FromResult(notes.FirstOrDefault(x => string.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<NoteFoundationRecord> SaveAsync(NoteFoundationRecord item, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            notes.RemoveAll(x => string.Equals(x.Id, item.Id, StringComparison.OrdinalIgnoreCase));
            notes.Add(item);
        }

        return Task.FromResult(item);
    }
}

