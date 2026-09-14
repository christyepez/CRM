using CRM.Domain.PipelineCatalog;
using Xunit;

namespace CRM.UnitTests;

public sealed class PipelineCatalogPolicyTests
{
    private static readonly string PipelineId = Guid.NewGuid().ToString("D");
    private static readonly string Stage1 = Guid.NewGuid().ToString("D");
    private static readonly string Stage2 = Guid.NewGuid().ToString("D");

    [Fact] public void ValidCatalog_IsNormalizedAndSorted(){var r=PipelineCatalogPolicy.Validate(Catalog("  Sales  ",[new(Stage2," Won ",2),new(Stage1," Open ",1)]));Assert.True(r.Valid);Assert.Equal("Sales",r.Catalog!.Name);Assert.Equal(new[]{1,2},r.Catalog.Stages.Select(x=>x.Order));Assert.Equal("Open",r.Catalog.Stages.First().Name);}
    [Fact] public void InvalidPipelineId_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.InvalidPipelineId,PipelineCatalogPolicy.Validate(Catalog(id:"bad")).ErrorCode);
    [Fact] public void EmptyPipelineName_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.PipelineNameRequired,PipelineCatalogPolicy.Validate(Catalog(name:" ")).ErrorCode);
    [Fact] public void LongPipelineName_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.PipelineNameTooLong,PipelineCatalogPolicy.Validate(Catalog(name:new string('x',121))).ErrorCode);
    [Fact] public void EmptyStages_AreRejected()=>Assert.Equal(PipelineCatalogErrorCode.StagesRequired,PipelineCatalogPolicy.Validate(Catalog(stages:Array.Empty<PipelineStageCatalogRecord>())).ErrorCode);
    [Fact] public void InvalidStageId_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.InvalidStageId,PipelineCatalogPolicy.Validate(Catalog(stages:[new("bad","Open",1)])).ErrorCode);
    [Fact] public void EmptyStageName_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.StageNameRequired,PipelineCatalogPolicy.Validate(Catalog(stages:[new(Stage1," ",1)])).ErrorCode);
    [Fact] public void LongStageName_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.StageNameTooLong,PipelineCatalogPolicy.Validate(Catalog(stages:[new(Stage1,new string('x',121),1)])).ErrorCode);
    [Theory][InlineData(0)][InlineData(-1)] public void NonPositiveOrder_IsRejected(int order)=>Assert.Equal(PipelineCatalogErrorCode.InvalidStageOrder,PipelineCatalogPolicy.Validate(Catalog(stages:[new(Stage1,"Open",order)])).ErrorCode);
    [Fact] public void DuplicateOrder_IsRejected()=>Assert.Equal(PipelineCatalogErrorCode.DuplicateStageOrder,PipelineCatalogPolicy.Validate(Catalog(stages:[new(Stage1,"Open",1),new(Stage2,"Won",1)])).ErrorCode);
    [Fact] public void ExactNameBoundary_IsAccepted()=>Assert.True(PipelineCatalogPolicy.Validate(Catalog(name:new string('x',120),stages:[new(Stage1,new string('y',120),1)])).Valid);

    private static PipelineCatalogRecord Catalog(string name="Sales",IReadOnlyCollection<PipelineStageCatalogRecord>? stages=null,string? id=null)=>
        new(id??PipelineId,name,stages??[new(Stage1,"Open",1),new(Stage2,"Won",2)]);
}
