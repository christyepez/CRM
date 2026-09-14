using CRM.Application.Ports.ReadModels;using CRM.Domain.ReportingInsights;
namespace CRM.Application.ReportingInsights;
public sealed record ReportingInsightItem(string Key,string Title,IReadOnlyCollection<InsightMetric> Metrics,string SourceMode,bool ProductiveRuntimeEnabled,bool PortalRuntimeEnabled,bool CommonDbRuntimeEnabled);
public interface IReportingInsightReadService { Task<IReadOnlyCollection<ReportingInsightItem>> GetAllAsync(CancellationToken ct=default); Task<ReportingInsightItem?> GetByKeyAsync(string key,CancellationToken ct=default); }
public sealed class ReportingInsightReadService(IReportingInsightFoundationProvider provider):IReportingInsightReadService
{
 public async Task<IReadOnlyCollection<ReportingInsightItem>> GetAllAsync(CancellationToken ct=default)=>(await provider.GetAllAsync(ct)).Select(Map).ToArray();
 public async Task<ReportingInsightItem?> GetByKeyAsync(string key,CancellationToken ct=default){var x=await provider.GetByKeyAsync((key??string.Empty).Trim().ToLowerInvariant(),ct);return x is null?null:Map(x);}
 static ReportingInsightItem Map(ReportingInsightSnapshot x){var v=ReportingInsightPolicy.Validate(x);if(!v.Valid||v.Snapshot is null)throw new InvalidOperationException(v.Message);var n=v.Snapshot;return new(n.Key,n.Title,n.Metrics,n.SourceMode,false,false,false);}
}