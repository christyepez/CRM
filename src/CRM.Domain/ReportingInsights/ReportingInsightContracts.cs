namespace CRM.Domain.ReportingInsights;
public sealed record InsightMetric(string Name, decimal Value, string Unit);
public sealed record ReportingInsightSnapshot(string Key,string Title,IReadOnlyCollection<InsightMetric> Metrics,string SourceMode);
public enum ReportingInsightValidationErrorCode { None=0,KeyRequired,InvalidKey,TitleRequired,TitleTooLong,MetricsRequired,DuplicateMetricName,InvalidMetricName,NegativeMetricValue,InvalidSourceMode }
public static class ReportingInsightKeys { public const string LeadsFunnel="leads-funnel";  public const string OpportunitiesPipeline="opportunities-pipeline";  public const string SalesActivities="sales-activities"; }