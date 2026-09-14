using CRM.Application.Ports.ReadModels;using CRM.Domain.ReportingInsights;
namespace CRM.Infrastructure.ReportingInsights;
public sealed class InMemoryReportingInsightFoundationProvider:IReportingInsightFoundationProvider
{
 private static readonly ReportingInsightSnapshot[] Items=[
  new(ReportingInsightKeys.LeadsFunnel,"Leads Funnel",[new("LeadConversionRate",24.5m,"Percent"),new("QualifiedLeads",38m,"Count")],"FoundationMock")
  // INSIGHT_ITEMS
 ];
 public Task<IReadOnlyCollection<ReportingInsightSnapshot>> GetAllAsync(CancellationToken ct=default){ct.ThrowIfCancellationRequested();return Task.FromResult<IReadOnlyCollection<ReportingInsightSnapshot>>(Items);}
 public Task<ReportingInsightSnapshot?> GetByKeyAsync(string key,CancellationToken ct=default){ct.ThrowIfCancellationRequested();return Task.FromResult(Items.FirstOrDefault(x=>string.Equals(x.Key,key,StringComparison.OrdinalIgnoreCase)));}
}