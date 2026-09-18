using System.Text.Json;
using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Migration.Application;
using CRM.Runtime.Sql;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRM.Migration.Tests;

public sealed class FoundationSqlTests
{
    private static readonly DateTimeOffset Utc = new(2026, 9, 18, 12, 0, 0, TimeSpan.Zero);
    private static CrmFoundationPreviewItemContract Preview(string id="preview-001") =>
        new(id,"Lead","Synthetic","Preview",Utc,new Dictionary<string,string>{{"internal","preserved"}});

    [Theory][InlineData("PortalSecurity")][InlineData("master")][InlineData("CrmMigration_")][InlineData("crmmigration_other")]
    public void SqlCandidateRejectsOtherDatabasesBeforeConnecting(string database)
    {
        var options=new DbContextOptionsBuilder<FoundationDbContext>().UseSqlServer(
            "Server=tcp:127.0.0.1,1433;Database="+database+";Integrated Security=true;Encrypt=true").Options;
        Assert.Throws<InvalidOperationException>(()=>new FoundationDbContext(options));
    }
    [Fact] public void SqlCandidateAllowsDedicatedDatabaseWithoutOpeningConnection()
    {
        var options=new DbContextOptionsBuilder<FoundationDbContext>().UseSqlServer(
            "Server=tcp:127.0.0.1,1433;Database=CrmMigration_Synthetic;Integrated Security=true;Encrypt=true").Options;
        using var db=new FoundationDbContext(options);
        Assert.Equal(System.Data.ConnectionState.Closed,db.Database.GetDbConnection().State);
    }

    [Fact] public async Task PreviewSurvivesNewContextsAndUpdateWithStatusAndBlockedClear()
    {
        await using var fixture = await Fixture.CreateAsync();
        var lead = new SqlLeadFoundationStore(fixture.Db,"default");
        Assert.Empty(await lead.GetPreviewAsync());
        Assert.Null(await lead.GetPreviewByIdAsync("missing"));
        await lead.SavePreviewAsync(Preview());
        var reopened = new SqlLeadFoundationStore(fixture.Db,"default");
        Assert.Equal("preserved",(await reopened.GetPreviewByIdAsync("PREVIEW-001"))!.Metadata["internal"]);
        await reopened.SavePreviewAsync(Preview() with {DisplayName="Updated"});
        Assert.Equal("Updated",Assert.Single(await reopened.GetPreviewAsync()).DisplayName);
        var status = await reopened.GetStatusAsync();
        Assert.True(status.DurablePersistence); Assert.Equal(1,status.PreviewCount);
        await Assert.ThrowsAsync<NotSupportedException>(()=>reopened.ClearPreviewAsync());
        Assert.Single(await reopened.GetPreviewAsync());
        await Assert.ThrowsAsync<ArgumentException>(()=>reopened.SavePreviewAsync(Preview("PREVIEW-001")));
        using var cancel = new CancellationTokenSource(); cancel.Cancel();
        await Assert.ThrowsAsync<OperationCanceledException>(()=>reopened.ClearPreviewAsync(cancel.Token));
    }

    [Fact] public async Task AllRecordAdaptersPreserveCompleteTypedContracts()
    {
        await using var f = await Fixture.CreateAsync();
        await RoundTrip(new SqlAccountFoundationStore(f.Db,"default"),new AccountFoundationRecord("account","A","tax","industry","segment",default),"account");
        await RoundTrip(new SqlOpportunityFoundationStore(f.Db,"default"),new OpportunityFoundationRecord("opportunity","A",12.34m,"USD",40,"pipeline","stage",default,"lead","contact","account","activity"),"opportunity");
        await RoundTrip(new SqlCaseFoundationStore(f.Db,"default"),new CaseFoundationRecord("case","customer","Title","Summary",default,default),"case");
        await RoundTrip(new SqlInteractionFoundationStore(f.Db,"default"),new InteractionFoundationRecord("interaction",default,"related",default,default,"Subject","Summary",Utc,default),"interaction");
        await RoundTrip(new SqlNoteFoundationStore(f.Db,"default"),new NoteFoundationRecord("note",default,"related","Text",default),"note");
        await RoundTrip(new SqlSegmentFoundationStore(f.Db,"default"),new SegmentFoundationRecord("segment","Name","Criteria",default),"segment");
        await RoundTrip(new SqlAssignmentFoundationStore(f.Db,"default"),new AssignmentFoundationRecord("assignment",default,"related","assignee","label",default),"assignment");
        await RoundTrip(new SqlDocumentMetadataFoundationStore(f.Db,"default"),new DocumentMetadataFoundationRecord("document",default,"related","reference","file.pdf","application/pdf","description",default),"document");
        await RoundTrip(new SqlTagFoundationStore(f.Db,"default"),new TagFoundationRecord("tag","Name","Description",default,null,null),"tag");
        var account = new SqlAccountFoundationStore(f.Db,"default");
        await account.SavePreviewAsync(Preview());
        Assert.Single(await account.GetPreviewAsync());
        Assert.NotNull(await account.GetPreviewByIdAsync("preview-001"));
        Assert.Equal(1,(await account.GetStatusAsync()).PreviewCount);
        await Assert.ThrowsAsync<NotSupportedException>(()=>account.ClearPreviewAsync());
        var contact = new SqlContactFoundationStore(f.Db,"default");
        var campaign = new SqlCampaignFoundationStore(f.Db,"default");
        await contact.SavePreviewAsync(Preview()); await campaign.SavePreviewAsync(Preview());
        Assert.Single(await contact.GetPreviewAsync()); Assert.Single(await campaign.GetPreviewAsync());
    }

    [Theory][InlineData("scheduled")][InlineData("completed")][InlineData("cancelled")]
    public async Task ActivityRestoresLifecycleAndRelationships(string lifecycle)
    {
        await using var f = await Fixture.CreateAsync();
        var id=CrmId.New(); var lead=CrmId.New();
        var activity=Activity.ScheduleForLead(id,lead,Enum.GetValues<ActivityType>()[0],"Synthetic",Utc);
        if(lifecycle=="completed") activity.Complete(Utc.AddHours(1));
        if(lifecycle=="cancelled") activity.Cancel();
        await new SqlActivityFoundationStore(f.Db,"default").SaveAsync(activity);
        var reopened=new SqlActivityFoundationStore(f.Db,"default");
        var restored=(await reopened.GetByIdAsync(id.ToString()))!;
        Assert.Equal(activity.Status,restored.Status); Assert.Equal(activity.CompletedAtUtc,restored.CompletedAtUtc);
        Assert.Equal(lead,restored.LeadId); Assert.Null(restored.ContactId);
        Assert.Equal(activity.ScheduledAtUtc,restored.ScheduledAtUtc);
        Assert.Equal(1,(await reopened.GetStatusAsync()).PreviewCount);
    }

    [Fact] public async Task TenantStoreAndIdentityAreIsolatedAndCancellationWritesNothing()
    {
        await using var f=await Fixture.CreateAsync();
        await new SqlLeadFoundationStore(f.Db,"Tenant").SavePreviewAsync(Preview());
        Assert.Empty(await new SqlLeadFoundationStore(f.Db,"tenant").GetPreviewAsync());
        Assert.Empty(await new SqlContactFoundationStore(f.Db,"Tenant").GetPreviewAsync());
        using var cancel=new CancellationTokenSource(); cancel.Cancel();
        await Assert.ThrowsAsync<OperationCanceledException>(()=>new SqlLeadFoundationStore(f.Db,"tenant").SavePreviewAsync(Preview(),cancel.Token));
        Assert.Empty(await new SqlLeadFoundationStore(f.Db,"tenant").GetPreviewAsync());
        Assert.Throws<ArgumentException>(()=>new SqlRecordStore<CrmFoundationPreviewItemContract>(f.Db,"default","Unknown",x=>x.Id));
        await Assert.ThrowsAsync<ArgumentException>(()=>new SqlLeadFoundationStore(f.Db,"Tenant").GetPreviewByIdAsync(" "));
    }

    [Fact] public void ActivityCodecRejectsInvalidLifecycleAndRestoresContact()
    {
        var activity=Activity.ScheduleForContact(CrmId.New(),CrmId.New(),Enum.GetValues<ActivityType>()[0],"Synthetic",Utc);
        Assert.Equal(activity.ContactId,ActivityCodec.Decode(ActivityCodec.Encode(activity)).ContactId);
        var state=new ActivityState(activity.Id.ToString(),activity.Type,"Synthetic",Utc,null,null,ActivityStatus.Completed,null);
        Assert.Throws<ArgumentException>(()=>ActivityCodec.Decode(JsonSerializer.SerializeToElement(state,SnapshotValidation.JsonOptions)));
        Assert.Throws<ArgumentException>(()=>ActivityCodec.Decode(JsonSerializer.SerializeToElement(state with {Status=ActivityStatus.Scheduled,Type=(ActivityType)999},SnapshotValidation.JsonOptions)));
    }

    private static async Task RoundTrip<T>(SqlTypedStore<T> store,T item,string id) where T:class
    {
        await store.SaveAsync(item);
        var actual=await store.GetByIdAsync(id);
        Assert.Equal(JsonSerializer.Serialize(item,SnapshotValidation.JsonOptions),JsonSerializer.Serialize(actual,SnapshotValidation.JsonOptions));
        Assert.Single(await store.GetAllAsync());
    }

    private sealed class Fixture(SqliteConnection connection,DbContextOptions<FoundationDbContext> options):IAsyncDisposable
    {
        public FoundationDbContext Db()=>new(options);
        public static async Task<Fixture> CreateAsync()
        {
            var connection=new SqliteConnection("Data Source=:memory:"); await connection.OpenAsync();
            connection.CreateCollation("Latin1_General_100_BIN2",StringComparer.Ordinal.Compare);
            connection.CreateCollation("Latin1_General_100_CI_AS",StringComparer.OrdinalIgnoreCase.Compare);
            var options=new DbContextOptionsBuilder<FoundationDbContext>().UseSqlite(connection).Options;
            await using var db=new FoundationDbContext(options); await db.Database.EnsureCreatedAsync();
            return new(connection,options);
        }
        public ValueTask DisposeAsync()=>connection.DisposeAsync();
    }
}
