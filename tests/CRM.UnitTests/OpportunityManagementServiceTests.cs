using CRM.Application.OpportunityManagement;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;
using CRM.Domain.OpportunityManagement;
using Xunit;

namespace CRM.UnitTests;

public sealed class OpportunityManagementServiceTests
{
    private static readonly string PipelineId=Guid.NewGuid().ToString();
    private static readonly string Stage1=Guid.NewGuid().ToString();
    private static readonly string Stage2=Guid.NewGuid().ToString();
    private static readonly OpportunityPipelineStageDefinition[] Stages=[new(Stage1,"Discovery",1),new(Stage2,"Proposal",2)];

    [Fact] public async Task Create_Valid_WritesOnce(){var s=new FakeStore();var service=new OpportunityManagementService(s);var r=await service.CreateAsync(Create());Assert.True(r.Success);Assert.Equal(1,s.SaveCount);Assert.NotNull(r.Opportunity);}
    [Fact] public async Task Create_Invalid_WritesZero(){var s=new FakeStore();var service=new OpportunityManagementService(s);var r=await service.CreateAsync(Create(expected:-1));Assert.False(r.Success);Assert.Equal(0,s.SaveCount);}
    [Fact] public async Task Update_NoChange_WritesZero(){var s=Seeded();var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.UpdateAsync(x.Id,Update());Assert.True(r.Success);Assert.False(r.Changed);Assert.Equal(0,s.SaveCount);}
    [Fact] public async Task Update_Changed_WritesOnce(){var s=Seeded();var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.UpdateAsync(x.Id,Update(expected:2000));Assert.True(r.Success);Assert.True(r.Changed);Assert.Equal(1,s.SaveCount);Assert.Equal(2000,r.Opportunity!.ExpectedValue);}
    [Fact] public async Task Update_NotFound_IsSafe(){var s=new FakeStore();var service=new OpportunityManagementService(s);var r=await service.UpdateAsync(Guid.NewGuid().ToString(),Update());Assert.Equal(OpportunityPipelineErrorCode.OpportunityNotFound.ToString(),r.ErrorCode);Assert.Equal(0,s.SaveCount);}
    [Fact] public async Task Progress_NextStage_WritesOnce(){var s=Seeded();var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.ProgressAsync(x.Id,new(Stage2,Stages));Assert.True(r.Success);Assert.Equal(Stage2,r.Opportunity!.StageId);Assert.Equal(1,s.SaveCount);}
    [Fact] public async Task Win_WritesOnceAndProbability100(){var s=Seeded();var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.WinAsync(x.Id);Assert.True(r.Success);Assert.Equal(OpportunityStatus.Won,r.Status);Assert.Equal(100,r.Opportunity!.Probability);Assert.Equal(1,s.SaveCount);}
    [Fact] public async Task RepeatWin_WritesZero(){var s=Seeded(OpportunityStatus.Won,100);var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.WinAsync(x.Id);Assert.True(r.Success);Assert.False(r.Changed);Assert.Equal(0,s.SaveCount);}
    [Fact] public async Task Lose_WritesOnce(){var s=Seeded();var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.LoseAsync(x.Id);Assert.Equal(OpportunityStatus.Lost,r.Status);Assert.Equal(1,s.SaveCount);}
    [Fact] public async Task Cancel_WritesOnce(){var s=Seeded();var service=new OpportunityManagementService(s);var x=s.Items.Single();var r=await service.CancelAsync(x.Id);Assert.Equal(OpportunityStatus.Cancelled,r.Status);Assert.Equal(1,s.SaveCount);}
    [Fact] public async Task GetAll_ReportsNonProductionSeam(){var s=Seeded();var service=new OpportunityManagementService(s);var all=await service.GetAllAsync();Assert.Single(all);Assert.Equal("NonProductionSeam",all.Single().PersistenceMode);Assert.False(all.Single().ProductiveCrudEnabled);}
    [Fact] public async Task CancellationToken_IsHonored(){var s=Seeded();var service=new OpportunityManagementService(s);using var cts=new CancellationTokenSource();cts.Cancel();await Assert.ThrowsAsync<OperationCanceledException>(()=>service.GetAllAsync(cts.Token));}

    private static OpportunityManagementCreateRequest Create(decimal expected=1000)=>new("Acme",expected,"USD",25,PipelineId,Stage1,Stages);
    private static OpportunityManagementUpdateRequest Update(decimal expected=1000)=>new("Acme",expected,"USD",25,PipelineId,Stage1,Stages);
    private static FakeStore Seeded(OpportunityStatus status=OpportunityStatus.Open,int probability=25){var s=new FakeStore();s.Items.Add(new(Guid.NewGuid().ToString(),"Acme",1000,"USD",probability,PipelineId,Stage1,status));return s;}

    private sealed class FakeStore:IOpportunityFoundationStore
    {
        public List<OpportunityFoundationRecord> Items {get;}=[]; public int SaveCount {get;private set;}
        public Task<IReadOnlyCollection<OpportunityFoundationRecord>> GetAllAsync(CancellationToken ct=default){ct.ThrowIfCancellationRequested();return Task.FromResult<IReadOnlyCollection<OpportunityFoundationRecord>>(Items.ToArray());}
        public Task<OpportunityFoundationRecord?> GetByIdAsync(string id,CancellationToken ct=default){ct.ThrowIfCancellationRequested();return Task.FromResult(Items.FirstOrDefault(x=>x.Id==id));}
        public Task<OpportunityFoundationRecord> SaveAsync(OpportunityFoundationRecord o,CancellationToken ct=default){ct.ThrowIfCancellationRequested();SaveCount++;Items.RemoveAll(x=>x.Id==o.Id);Items.Add(o);return Task.FromResult(o);}
    }
}
