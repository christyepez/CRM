using CRM.Application.Portal;

namespace CRM.Infrastructure.Portal;

public sealed class PortalIntegrationPlaceholder
{
    public CrmPortalIntegrationStatusResponse GetPlannedStatus() =>
        new(
            "CRM",
            "NotConfigured",
            "Planned",
            "NonProduction",
            "PortalCorporativo",
            false,
            "NonProductionPlaceholder: Portal integration contracts only; no runtime calls configured",
            [
                "Security/Auth",
                "Menu",
                "Permissions",
                "Audit",
                "Notification",
                "Configuration",
                "Gateway",
                "CorrelationId"
            ],
            true);

    public void ThrowIfRuntimeUseIsAttempted() => throw new PortalAdapterNotConfiguredException();
}
