using CRM.Domain.Foundation;

namespace CRM.Application.Foundation;

public sealed record CrmReadinessResponse(
    string Module,
    string Status,
    string PortalIntegration,
    string FinancialIntegration,
    string RuntimeMode);

public sealed class CrmReadinessService
{
    public CrmReadinessResponse GetReadiness() =>
        new(
            CrmFoundationStatus.Module,
            CrmFoundationStatus.Status,
            CrmFoundationStatus.PortalIntegration,
            CrmFoundationStatus.FinancialIntegration,
            CrmFoundationStatus.RuntimeMode);
}
