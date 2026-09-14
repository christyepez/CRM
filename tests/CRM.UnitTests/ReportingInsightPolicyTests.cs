using CRM.Domain.ReportingInsights;using Xunit;
namespace CRM.UnitTests;
public sealed class ReportingInsightPolicyTests
{
 [Fact]public void ValidInsight_IsNormalized(){var r=ReportingInsightPolicy.Validate(new("LEADS-FUNNEL"," Leads Funnel ",[new("LeadConversionRate",24.5m,"Percent"),new("QualifiedLeads",38,"Count")],"FoundationMock"));Assert.True(r.Valid);Assert.Equal("leads-funnel",r.Snapshot!.Key);}
 [Fact]public void NegativeMetric_IsRejected(){var r=ReportingInsightPolicy.Validate(new("leads-funnel","Leads Funnel",[new("QualifiedLeads",-1,"Count")],"FoundationMock"));Assert.Equal(ReportingInsightValidationErrorCode.NegativeMetricValue,r.Error);}
 [Fact]public void RealSource_IsRejected(){var r=ReportingInsightPolicy.Validate(new("leads-funnel","Leads Funnel",[new("QualifiedLeads",1,"Count")],"RealData"));Assert.Equal(ReportingInsightValidationErrorCode.InvalidSourceMode,r.Error);}
}