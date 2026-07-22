namespace CRM.Application.Portal;

public sealed class CrmPortalIntegrationStatusService
{
    public const string WarningText = "Portal integration contracts only; no runtime calls configured";

    private static readonly string[] Capabilities =
    [
        "Security/Auth",
        "Menu",
        "Permissions",
        "Audit",
        "Notification",
        "Configuration",
        "Gateway",
        "CorrelationId"
    ];

    public CrmPortalIntegrationStatusResponse GetStatus() =>
        new(
            "CRM",
            "PortalIntegrationPlanned",
            "Planned",
            "NonProduction",
            "PortalCorporativo",
            false,
            WarningText,
            Capabilities,
            true);

    public object GetContracts() => new
    {
        foundationMode = true,
        module = "CRM",
        status = "PortalIntegrationPlanned",
        integrationMode = "Planned",
        runtimeMode = "NonProduction",
        capabilityOwner = "PortalCorporativo",
        connected = false,
        warning = WarningText,
        contracts = new[]
        {
            nameof(PortalUserContextContract),
            nameof(PortalTenantContextContract),
            nameof(PortalPermissionContract),
            nameof(PortalMenuItemContract),
            nameof(PortalAuditEventContract),
            nameof(PortalNotificationContract),
            nameof(PortalConfigurationContract),
            nameof(PortalCorrelationContract)
        },
        requiredCapabilities = Capabilities
    };

    public object GetRequiredCapabilities() => new
    {
        foundationMode = true,
        module = "CRM",
        status = "PortalIntegrationPlanned",
        integrationMode = "Planned",
        runtimeMode = "NonProduction",
        capabilityOwner = "PortalCorporativo",
        connected = false,
        warning = WarningText,
        requiredCapabilities = Capabilities.Select(capability => new
        {
            capability,
            owner = "PortalCorporativo",
            crmPolicy = "External"
        }).ToArray()
    };
}
