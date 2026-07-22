namespace CRM.Infrastructure.Portal;

public sealed class PortalAdapterNotConfiguredException : InvalidOperationException
{
    public PortalAdapterNotConfiguredException()
        : base("NonProductionPlaceholder: Portal integration is Planned and no runtime adapter is configured.")
    {
    }
}
