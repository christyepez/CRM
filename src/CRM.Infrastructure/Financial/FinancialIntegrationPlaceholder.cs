using CRM.Application.Financial;

namespace CRM.Infrastructure.Financial;

public sealed class FinancialIntegrationPlaceholder
{
    public CrmFinancialIntegrationStatusResponse GetPlannedStatus() =>
        new(
            "CRM",
            "NotConfigured",
            "Planned",
            "NonProduction",
            "Financiero",
            false,
            "NonProductionPlaceholder: Financial integration contracts only; no runtime calls configured",
            [
                "CustomerReference",
                "AccountStatus",
                "InvoiceSummary",
                "PaymentStatus",
                "CollectionsSignal",
                "FinancialEvents"
            ],
            [
                "API",
                "Events",
                "NoSharedDatabase"
            ],
            true);

    public void ThrowIfRuntimeUseIsAttempted() => throw new FinancialAdapterNotConfiguredException();
}
