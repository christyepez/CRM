namespace CRM.Application.Reporting;

public sealed record CrmKpiContract(
    string Name,
    string DisplayName,
    string Source,
    string AnalyticsMode,
    string RuntimeMode,
    bool Connected,
    string Warning);

public sealed record CrmDashboardContract(
    string Name,
    string Source,
    string AnalyticsMode,
    string RuntimeMode,
    bool Connected,
    string Warning);

public sealed record CrmAnalyticsReadModelContract(
    string Name,
    string Source,
    string AnalyticsMode,
    string RuntimeMode,
    bool Connected,
    string Warning);

public sealed record CrmReportingMetricDefinitionContract(
    string Name,
    string Description,
    string Source,
    string AnalyticsMode,
    string RuntimeMode,
    bool Connected,
    string Warning);

public sealed record CrmReportingIntegrationStatusResponse(
    string Module,
    string Status,
    string AnalyticsMode,
    string RuntimeMode,
    string Source,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    IReadOnlyCollection<string> BlockedCapabilities,
    bool FoundationMode);
