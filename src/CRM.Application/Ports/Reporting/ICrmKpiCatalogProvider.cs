using CRM.Application.Reporting;

namespace CRM.Application.Ports.Reporting;

/// <summary>
/// FutureReportingAdapter port for CRM KPI metadata without activating analytics runtime.
/// </summary>
public interface ICrmKpiCatalogProvider
{
    Task<IReadOnlyCollection<CrmKpiContract>> GetKpisAsync(CancellationToken cancellationToken = default);
}
