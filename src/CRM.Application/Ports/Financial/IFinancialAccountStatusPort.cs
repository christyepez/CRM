using CRM.Application.Financial;

namespace CRM.Application.Ports.Financial;

/// <summary>
/// FutureFinancialAdapter port for account status awareness without shared database access.
/// </summary>
public interface IFinancialAccountStatusPort
{
    Task<FinancialAccountStatusContract> GetAccountStatusAsync(CancellationToken cancellationToken = default);
}
