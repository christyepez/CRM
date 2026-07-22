namespace CRM.Application.Reporting;

public sealed class CrmReportingIntegrationStatusService
{
    public const string WarningText = "Reporting contracts only; no analytics runtime configured";

    private static readonly string[] RequiredCapabilities =
    [
        "KpiCatalog",
        "DashboardCatalog",
        "AnalyticsReadModels",
        "PortalAuthorizationContext",
        "FuturePowerBiEmbedding"
    ];

    private static readonly string[] BlockedCapabilities =
    [
        "RealDataset",
        "EmbedToken",
        "ProductionDashboard",
        "RuntimePowerBiConnection"
    ];

    public CrmReportingIntegrationStatusResponse GetStatus() =>
        new(
            "CRM",
            "ReportingIntegrationPlanned",
            "Planned",
            "NonProduction",
            "FoundationMock",
            false,
            WarningText,
            RequiredCapabilities,
            BlockedCapabilities,
            true);

    public IReadOnlyCollection<CrmKpiContract> GetKpis() =>
    [
        Kpi("LeadConversionRate", "Lead Conversion Rate"),
        Kpi("QualifiedLeads", "Qualified Leads"),
        Kpi("OpportunitiesWon", "Opportunities Won"),
        Kpi("OpportunitiesLost", "Opportunities Lost"),
        Kpi("PipelineValue", "Pipeline Value"),
        Kpi("AverageSalesCycleDays", "Average Sales Cycle Days"),
        Kpi("ActivitiesCompleted", "Activities Completed"),
        Kpi("FollowUpsPending", "Follow Ups Pending"),
        Kpi("AccountsActive", "Accounts Active"),
        Kpi("ContactsActive", "Contacts Active")
    ];

    public IReadOnlyCollection<CrmDashboardContract> GetDashboards() =>
    [
        Dashboard("CRM Executive Overview"),
        Dashboard("Leads Funnel"),
        Dashboard("Opportunities Pipeline"),
        Dashboard("Sales Activities"),
        Dashboard("Accounts and Contacts"),
        Dashboard("Financial Signals Preview")
    ];

    public IReadOnlyCollection<CrmAnalyticsReadModelContract> GetAnalyticsReadModels() =>
    [
        ReadModel("LeadAnalyticsReadModel"),
        ReadModel("OpportunityAnalyticsReadModel"),
        ReadModel("ActivityAnalyticsReadModel"),
        ReadModel("AccountContactAnalyticsReadModel"),
        ReadModel("FinancialSignalPreviewReadModel")
    ];

    public IReadOnlyCollection<CrmReportingMetricDefinitionContract> GetMetricDefinitions() =>
        GetKpis()
            .Select(kpi => new CrmReportingMetricDefinitionContract(
                kpi.Name,
                $"Conceptual definition for {kpi.DisplayName}.",
                "FoundationMock",
                "Planned",
                "NonProduction",
                false,
                WarningText))
            .ToArray();

    private static CrmKpiContract Kpi(string name, string displayName) =>
        new(name, displayName, "FoundationMock", "Planned", "NonProduction", false, WarningText);

    private static CrmDashboardContract Dashboard(string name) =>
        new(name, "FoundationMock", "Planned", "NonProduction", false, WarningText);

    private static CrmAnalyticsReadModelContract ReadModel(string name) =>
        new(name, "FoundationMock", "Planned", "NonProduction", false, WarningText);
}
