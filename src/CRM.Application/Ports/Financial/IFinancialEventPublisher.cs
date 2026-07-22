using CRM.Application.Financial;

namespace CRM.Application.Ports.Financial;

/// <summary>
/// FutureFinancialAdapter port for conceptual CRM-Financiero events without a broker or outbox runtime.
/// </summary>
public interface IFinancialEventPublisher
{
    Task<FinancialIntegrationEventContract> PublishPlannedEventAsync(FinancialIntegrationEventContract integrationEvent, CancellationToken cancellationToken = default);
}
