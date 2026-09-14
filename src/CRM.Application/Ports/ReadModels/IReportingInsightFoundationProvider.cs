using CRM.Domain.ReportingInsights;
namespace CRM.Application.Ports.ReadModels;
public interface IReportingInsightFoundationProvider { Task<IReadOnlyCollection<ReportingInsightSnapshot>> GetAllAsync(CancellationToken ct=default); Task<ReportingInsightSnapshot?> GetByKeyAsync(string key,CancellationToken ct=default); }