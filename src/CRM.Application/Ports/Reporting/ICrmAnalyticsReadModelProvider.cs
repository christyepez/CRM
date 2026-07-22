using CRM.Application.Reporting;

namespace CRM.Application.Ports.Reporting;

/// <summary>
/// FutureReportingAdapter port for analytics read model metadata without datasets or DB queries.
/// </summary>
public interface ICrmAnalyticsReadModelProvider
{
    Task<IReadOnlyCollection<CrmAnalyticsReadModelContract>> GetReadModelsAsync(CancellationToken cancellationToken = default);
}
