using CRM.Application.Reporting;

namespace CRM.Infrastructure.Reporting;

public sealed class ReportingIntegrationPlaceholder
{
    public CrmReportingIntegrationStatusResponse GetPlannedStatus() =>
        new(
            "CRM",
            "NotConfigured",
            "Planned",
            "NonProduction",
            "FoundationMock",
            false,
            "NonProductionPlaceholder: Reporting contracts only; no analytics runtime configured",
            [
                "KpiCatalog",
                "DashboardCatalog",
                "AnalyticsReadModels",
                "PortalAuthorizationContext",
                "FuturePowerBiEmbedding"
            ],
            [
                "RealDataset",
                "EmbedToken",
                "ProductionDashboard",
                "RuntimePowerBiConnection"
            ],
            true);

    public void ThrowIfRuntimeUseIsAttempted() => throw new ReportingAdapterNotConfiguredException();
}
