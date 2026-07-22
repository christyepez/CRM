using CRM.Application.Financial;

namespace CRM.Application.Ports.Financial;

/// <summary>
/// FutureFinancialAdapter port for collection-risk signals without implementing cobranzas in CRM.
/// </summary>
public interface IFinancialCollectionsSignalPort
{
    Task<FinancialCollectionsSignalContract> GetCollectionsSignalAsync(CancellationToken cancellationToken = default);
}
