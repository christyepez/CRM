using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Entities;

namespace CRM.Runtime.Sql;

public abstract class SqlTypedStore<T> where T : class
{
    private readonly SqlRecordStore<T> records;
    protected SqlTypedStore(SqlRecordStore<T> records) => this.records = records;
    public Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default) => records.ReadAllAsync(cancellationToken);
    public Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default) => records.ReadByIdAsync(id, cancellationToken);
    public Task<T> SaveAsync(T item, CancellationToken cancellationToken = default) => records.SaveAsync(item, cancellationToken);
}

public abstract class SqlPreviewStore : SqlTypedStore<CrmFoundationPreviewItemContract>
{
    private readonly string storeName;
    protected SqlPreviewStore(Func<FoundationDbContext> db, string tenant, string kind, string storeName)
        : base(new(db, tenant, kind, x => x.Id)) => this.storeName = storeName;
    public Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default) => GetAllAsync(cancellationToken);
    public Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default) => GetByIdAsync(id, cancellationToken);
    public Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default) => SaveAsync(preview, cancellationToken);
    public Task ClearPreviewAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        throw new NotSupportedException("Bulk deletion is disabled in the SQL centralization candidate.");
    }
    public async Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default) =>
        new(storeName, true, true, true, (await GetAllAsync(cancellationToken)).Count,
            "SqlCandidate", "NonProduction", "SQL adapter candidate; activation and centralization acceptance remain pending.");
}

public sealed class SqlLeadFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlPreviewStore(db, tenant, "LeadPreview", "LeadFoundationStore"), ILeadFoundationStore;
public sealed class SqlContactFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlPreviewStore(db, tenant, "ContactPreview", "ContactFoundationStore"), IContactFoundationStore;
public sealed class SqlCampaignFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlPreviewStore(db, tenant, "CampaignPreview", "CampaignFoundationStore"), ICampaignFoundationStore;

public sealed class SqlAccountFoundationStore : SqlTypedStore<AccountFoundationRecord>, IAccountFoundationStore
{
    private sealed class Previews(Func<FoundationDbContext> db, string tenant)
        : SqlPreviewStore(db, tenant, "AccountPreview", "AccountFoundationStore");
    private readonly Previews previews;
    public SqlAccountFoundationStore(Func<FoundationDbContext> db, string tenant)
        : base(new(db, tenant, "Account", x => x.Id)) => previews = new(db, tenant);
    public Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken = default) => previews.GetPreviewAsync(cancellationToken);
    public Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id, CancellationToken cancellationToken = default) => previews.GetPreviewByIdAsync(id, cancellationToken);
    public Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview, CancellationToken cancellationToken = default) => previews.SavePreviewAsync(preview, cancellationToken);
    public Task ClearPreviewAsync(CancellationToken cancellationToken = default) => previews.ClearPreviewAsync(cancellationToken);
    public Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default) => previews.GetStatusAsync(cancellationToken);
}

public sealed class SqlActivityFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<Activity>(new(db, tenant, "Activity", x => x.Id.ToString(), ActivityCodec.Encode, ActivityCodec.Decode)), IActivityFoundationStore
{
    public async Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default) =>
        new("ActivityFoundationStore", true, true, true, (await GetAllAsync(cancellationToken)).Count,
            "SqlCandidate", "NonProduction", "SQL adapter candidate; activation and centralization acceptance remain pending.");
}
public sealed class SqlOpportunityFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<OpportunityFoundationRecord>(new(db, tenant, "Opportunity", x => x.Id)), IOpportunityFoundationStore;
public sealed class SqlCaseFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<CaseFoundationRecord>(new(db, tenant, "Case", x => x.Id)), ICaseFoundationStore;
public sealed class SqlInteractionFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<InteractionFoundationRecord>(new(db, tenant, "Interaction", x => x.Id)), IInteractionFoundationStore;
public sealed class SqlNoteFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<NoteFoundationRecord>(new(db, tenant, "Note", x => x.Id)), INoteFoundationStore;
public sealed class SqlSegmentFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<SegmentFoundationRecord>(new(db, tenant, "Segment", x => x.Id)), ISegmentFoundationStore;
public sealed class SqlAssignmentFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<AssignmentFoundationRecord>(new(db, tenant, "Assignment", x => x.Id)), IAssignmentFoundationStore;
public sealed class SqlDocumentMetadataFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<DocumentMetadataFoundationRecord>(new(db, tenant, "DocumentMetadata", x => x.Id)), IDocumentMetadataFoundationStore;
public sealed class SqlTagFoundationStore(Func<FoundationDbContext> db, string tenant)
    : SqlTypedStore<TagFoundationRecord>(new(db, tenant, "Tag", x => x.Id)), ITagFoundationStore;
