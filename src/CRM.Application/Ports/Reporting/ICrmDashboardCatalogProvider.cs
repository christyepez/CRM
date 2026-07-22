using CRM.Application.Reporting;

namespace CRM.Application.Ports.Reporting;

/// <summary>
/// FutureReportingAdapter port for dashboard catalog metadata without Power BI embedding.
/// </summary>
public interface ICrmDashboardCatalogProvider
{
    Task<IReadOnlyCollection<CrmDashboardContract>> GetDashboardsAsync(CancellationToken cancellationToken = default);
}
