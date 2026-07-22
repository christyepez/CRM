using CRM.Application.Financial;

namespace CRM.Application.Ports.Financial;

/// <summary>
/// FutureFinancialAdapter port for customer reference lookup without direct CRM-to-Financiero coupling.
/// </summary>
public interface IFinancialCustomerLookupPort
{
    Task<FinancialCustomerReferenceContract> GetCustomerReferenceStatusAsync(CancellationToken cancellationToken = default);
}
