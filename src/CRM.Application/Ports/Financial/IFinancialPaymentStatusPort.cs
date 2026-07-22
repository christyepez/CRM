using CRM.Application.Financial;

namespace CRM.Application.Ports.Financial;

/// <summary>
/// FutureFinancialAdapter port for payment status awareness without direct Financiero runtime calls.
/// </summary>
public interface IFinancialPaymentStatusPort
{
    Task<FinancialPaymentStatusContract> GetPaymentStatusAsync(CancellationToken cancellationToken = default);
}
