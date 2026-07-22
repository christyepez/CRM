namespace CRM.Infrastructure.Reporting;

public sealed class ReportingAdapterNotConfiguredException : InvalidOperationException
{
    public ReportingAdapterNotConfiguredException()
        : base("NonProductionPlaceholder: Reporting integration is Planned and no analytics runtime is configured.")
    {
    }
}
