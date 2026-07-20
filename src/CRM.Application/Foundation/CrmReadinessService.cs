using CRM.Application.Contracts;
using CRM.Domain.Foundation;

namespace CRM.Application.Foundation;

public sealed class CrmReadinessService
{
    public CrmReadinessResponse GetReadiness() =>
        new(
            CrmFoundationStatus.Module,
            CrmFoundationStatus.Status,
            CrmFoundationStatus.PortalIntegration,
            CrmFoundationStatus.FinancialIntegration,
            CrmFoundationStatus.RuntimeMode,
            "Draft");
}
