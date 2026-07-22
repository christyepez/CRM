using CRM.Application.Financial;

namespace CRM.Application.Ports.Financial;

/// <summary>
/// FutureFinancialAdapter port for invoice awareness without creating invoices in CRM.
/// </summary>
public interface IFinancialInvoiceAwarenessPort
{
    Task<FinancialInvoiceSummaryContract> GetInvoiceAwarenessAsync(CancellationToken cancellationToken = default);
}
