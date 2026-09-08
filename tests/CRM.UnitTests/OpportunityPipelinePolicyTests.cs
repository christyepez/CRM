using CRM.Domain.Enums;
using CRM.Domain.OpportunityManagement;
using Xunit;

namespace CRM.UnitTests;

public sealed class OpportunityPipelinePolicyTests
{
    private static readonly string OpportunityId = Guid.NewGuid().ToString();
    private static readonly string PipelineId = Guid.NewGuid().ToString();
    private static readonly string Stage1 = Guid.NewGuid().ToString();
    private static readonly string Stage2 = Guid.NewGuid().ToString();
    private static IReadOnlyCollection<OpportunityPipelineStageDefinition> Stages => new[] { new OpportunityPipelineStageDefinition(Stage1, "Discovery", 1), new OpportunityPipelineStageDefinition(Stage2, "Proposal", 2) };

    [Fact] public void Create_Valid_IsAllowed() { var r = OpportunityPipelinePolicy.Evaluate(Command()); Assert.True(r.Success); Assert.True(r.Changed); Assert.Equal(OpportunityStatus.Open, r.ResultStatus); }
    [Fact] public void Create_NormalizesNameAndCurrency() { var r = OpportunityPipelinePolicy.Evaluate(Command(accountName:"  Acme  ", currency:" usd ")); Assert.Equal("Acme", r.NormalizedAccountName); Assert.Equal("USD", r.NormalizedCurrency); }
    [Fact] public void Create_RejectsNegativeValue() { var r=OpportunityPipelinePolicy.Evaluate(Command(expectedValue:-1)); Assert.Equal(OpportunityPipelineErrorCode.InvalidExpectedValue,r.ErrorCode); }
    [Fact] public void Create_RejectsProbabilityOutOfRange() { var r=OpportunityPipelinePolicy.Evaluate(Command(probability:101)); Assert.Equal(OpportunityPipelineErrorCode.InvalidProbability,r.ErrorCode); }
    [Fact] public void Create_RejectsDuplicateStageOrder() { var stages=new[]{new OpportunityPipelineStageDefinition(Stage1,"A",1),new OpportunityPipelineStageDefinition(Stage2,"B",1)}; var r=OpportunityPipelinePolicy.Evaluate(Command(stages:stages)); Assert.Equal(OpportunityPipelineErrorCode.DuplicateStageOrder,r.ErrorCode); }
    [Fact] public void Create_RejectsDuplicateStageName() { var stages=new[]{new OpportunityPipelineStageDefinition(Stage1,"A",1),new OpportunityPipelineStageDefinition(Stage2,"a",2)}; var r=OpportunityPipelinePolicy.Evaluate(Command(stages:stages)); Assert.Equal(OpportunityPipelineErrorCode.DuplicateStageName,r.ErrorCode); }
    [Fact] public void Create_RejectsStageOutsidePipeline() { var r=OpportunityPipelinePolicy.Evaluate(Command(stageId:Guid.NewGuid().ToString())); Assert.Equal(OpportunityPipelineErrorCode.CurrentStageNotInPipeline,r.ErrorCode); }
    [Fact] public void Update_NoChange_IsAllowedWithoutChange() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Update, existing:Snapshot())); Assert.True(r.Success); Assert.False(r.Changed); }
    [Fact] public void Progress_OnlyAllowsNextStage() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Progress, stageId:Stage2, existing:Snapshot())); Assert.True(r.Success); Assert.True(r.Changed); }
    [Fact] public void Progress_RejectsSkippingOrSameStage() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Progress, stageId:Stage1, existing:Snapshot())); Assert.Equal(OpportunityPipelineErrorCode.InvalidStageProgression,r.ErrorCode); }
    [Fact] public void Win_SetsTerminalAndProbability100() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Win, existing:Snapshot())); Assert.True(r.Success); Assert.Equal(OpportunityStatus.Won,r.ResultStatus); Assert.Equal(100,r.Probability); }
    [Fact] public void Lose_SetsLost() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Lose, existing:Snapshot())); Assert.True(r.Success); Assert.Equal(OpportunityStatus.Lost,r.ResultStatus); }
    [Fact] public void Cancel_SetsCancelled() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Cancel, existing:Snapshot())); Assert.True(r.Success); Assert.Equal(OpportunityStatus.Cancelled,r.ResultStatus); }
    [Fact] public void RepeatTerminal_IsIdempotent() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Win, existing:Snapshot(OpportunityStatus.Won,100))); Assert.True(r.Success); Assert.False(r.Changed); }
    [Fact] public void Won_CannotBeLost() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Lose, existing:Snapshot(OpportunityStatus.Won,100))); Assert.Equal(OpportunityPipelineErrorCode.WonOpportunityCannotBeLost,r.ErrorCode); }
    [Fact] public void Lost_CannotBeWon() { var r=OpportunityPipelinePolicy.Evaluate(Command(OpportunityPipelineOperation.Win, existing:Snapshot(OpportunityStatus.Lost))); Assert.Equal(OpportunityPipelineErrorCode.LostOpportunityCannotBeWon,r.ErrorCode); }
    [Fact] public void SyntheticReference_InvalidGuidRejected() { var r=OpportunityPipelinePolicy.Evaluate(Command(leadId:"bad")); Assert.Equal(OpportunityPipelineErrorCode.InvalidLeadId,r.ErrorCode); }

    private static OpportunityPipelineCommand Command(OpportunityPipelineOperation op=OpportunityPipelineOperation.Create,string accountName="Acme",decimal expectedValue=1000,string currency="USD",int probability=25,string? stageId=null,IReadOnlyCollection<OpportunityPipelineStageDefinition>? stages=null,OpportunityPipelineSnapshot? existing=null,string? leadId=null) => new(op, op==OpportunityPipelineOperation.Create?null:OpportunityId, accountName, expectedValue, currency, probability, PipelineId, stageId??Stage1, stages??Stages, leadId, ExistingOpportunity: existing);
    private static OpportunityPipelineSnapshot Snapshot(OpportunityStatus status=OpportunityStatus.Open,int probability=25) => new(OpportunityId,"Acme",1000,"USD",probability,PipelineId,Stage1,status);
}
