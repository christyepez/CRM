namespace CRM.Infrastructure.Financial;

public sealed class FinancialAdapterNotConfiguredException : InvalidOperationException
{
    public FinancialAdapterNotConfiguredException()
        : base("NonProductionPlaceholder: Financial integration is Planned and no runtime adapter is configured.")
    {
    }
}
